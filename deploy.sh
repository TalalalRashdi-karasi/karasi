#!/bin/bash

# Variables
APP_NAME="karasi.om"
PROJECT_PATH="src/Shubak_Website.csproj"
BUILD_DIR="src/bin/Release/net7.0/linux-x64/publish"
EC2_USER="ubuntu"
EC2_HOST="18.141.208.65"
EC2_KEY_PATH="aws/LightsailDefaultKey-ap-southeast-1.pem"
REMOTE_APP_DIR="/var/www/$APP_NAME"
REMOTE_NGINX_CONFIG="/etc/nginx/sites-available/$APP_NAME"
REMOTE_NGINX_CONFIG_LINK="/etc/nginx/sites-enabled/$APP_NAME"
SERVICE_NAME="karasi-om.service"
REMOTE_SERVICE_PATH="/etc/systemd/system/$SERVICE_NAME"
DOTNET_VERSION="7.0"

# Build the .NET Core application
echo "Building the .NET Core application..."
dotnet publish -c Release -r linux-x64 --self-contained

# Create a temporary directory for the build output on the EC2 instance
echo "Creating temporary directory on the EC2 instance..."
# clear the temp_deploy directory
ssh -i $EC2_KEY_PATH $EC2_USER@$EC2_HOST "rm -rf ~/temp_deploy"
ssh -i $EC2_KEY_PATH $EC2_USER@$EC2_HOST "mkdir -p ~/temp_deploy"

# Copy the build output to the temporary directory on the EC2 instance
echo "Copying build output to the EC2 instance..."
rsync -avz -e "ssh -i $EC2_KEY_PATH" $BUILD_DIR/* $EC2_USER@$EC2_HOST:~/temp_deploy

# Create directories, install .NET SDK, Nginx, and configure them on the EC2 instance
# Configure the EC2 instance
echo "Configuring the EC2 instance..."
ssh -i $EC2_KEY_PATH $EC2_USER@$EC2_HOST << EOF
# Determine Ubuntu version
UBUNTU_VERSION=$(. /etc/os-release; echo $VERSION_ID)

# Update package list and install .NET SDK if it's not installed
if ! command -v dotnet &> /dev/null
then
    echo ".NET SDK not found. Installing .NET SDK..."
    wget https://packages.microsoft.com/config/ubuntu/\$UBUNTU_VERSION/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    sudo apt-get update
    sudo apt-get install -y dotnet-sdk-$DOTNET_VERSION
else
    echo ".NET SDK is already installed."
fi

# Update package list and install Nginx if it's not installed
if ! command -v nginx &> /dev/null
then
    echo "Nginx not found. Installing Nginx..."
    sudo apt update
    sudo apt install -y nginx
else
    echo "Nginx is already installed."
fi

# Create the application directory if it doesn't exist
sudo mkdir -p $REMOTE_APP_DIR
sudo rsync -av ~/temp_deploy/ $REMOTE_APP_DIR

# Create the Nginx configuration
sudo tee $REMOTE_NGINX_CONFIG > /dev/null <<EOL
server {
    listen 80;
    server_name karasi.om;

    location / {
        proxy_pass http://localhost:5001;
        proxy_http_version 1.1;
        proxy_set_header Upgrade \$http_upgrade;
        proxy_set_header Connection "keep-alive";
        proxy_set_header Host \$host;
        proxy_cache_bypass \$http_upgrade;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
EOL

# Enable the Nginx configuration
sudo ln -sf $REMOTE_NGINX_CONFIG $REMOTE_NGINX_CONFIG_LINK
sudo systemctl restart nginx

# Create the systemd service file
sudo tee $REMOTE_SERVICE_PATH > /dev/null <<EOL
[Unit]
Description=.NET Web API Application
After=network.target

[Service]
WorkingDirectory=$REMOTE_APP_DIR
ExecStart=dotnet $REMOTE_APP_DIR/Shubak_Website.dll --urls=http://localhost:5001
Restart=always
RestartSec=10
SyslogIdentifier=dotnet-$APP_NAME
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
EOL

# Enable and start the systemd service
sudo systemctl enable $SERVICE_NAME
sudo systemctl start $SERVICE_NAME

echo "Configuration completed successfully."
EOF

echo "Deployment completed successfully."
