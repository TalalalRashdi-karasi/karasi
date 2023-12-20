dotnet publish -c Release -r linux-x64 --self-contained false
tar -cvf shubak.web.v1.tar -C bin/Release/net6.0/linux-x64/publish .
scp shubak.web.v1.tar root@95.179.135.254:/var/www_tmp
rm shubak.web.v1.tar

ssh root@95.179.135.254 << EOF
    sudo systemctl stop kestrel-site-shubak-com.service
    rm -r /var/www/shubak/*
    tar -xf /var/www_tmp/shubak.web.v1.tar -C /var/www/shubak/
    sudo systemctl start kestrel-site-shubak-com.service
    sudo systemctl status kestrel-site-shubak-com.service
    systemctl reload nginx
EOF