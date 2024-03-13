# Deployment


## Copy config to remote server
    
    ```ssh
    # copy config to remote server
    scp -r Deployment root@45.76.36.193:/root
    ```

ssh-copy-id -i ~/.ssh/id_rsa.pub root@45.76.36.193
password: 
q6T@smq_qJr!At2n

## Run docker-compose remotely

```ssh 
# adding context to docker (after you added ssh key to remote server)
docker context create remote_ticket --docker host=ssh://root@45.76.36.193


# change docker context to remote_ticket
docker context use remote_ticket

docker ps

# run docker-compose
docker-compose -f Deployment/docker-compose.yml up -d

# change docker context to default
docker context use default
```


## Copy db backup to container

```ssh
# copy db backup to container
docker ps
docker cp Deployment/eventom_bk.sql fb9f3ef4926a:eventom_bk.sql

# create mysql db
CREATE DATABASE eventom;

docker exec -i 1167dbcb1555 mysql -u root --password=12345678 -e "CREATE DATABASE eventom;"


docker exec -i 1167dbcb1555 /usr/bin/mysql -u root --password=12345678 eventom < eventom_bk.sql

# check connection
docker exec -it 1167dbcb1555 /bin/bash

# connect to db
/usr/bin/mysql -u root --password=12345678 eventom

# list all tables in db
use eventom;
show tables;
```

# list all tables
show tables;
```



```ssh


## rebuild images and run docker-compose

```ssh
# rebuild images and run docker-compose
docker compose -f Deployment/docker-compose.yml up -d --build
```

## monitor logs

```ssh
# monitor logs
docker compose -f Deployment/docker-compose.yml logs -f
```

## connect to mysql

To connect to a MySQL server running inside a Docker container on a remote server using SSH, you can follow these steps:

1. **SSH Tunneling**: 
   - First, create an SSH tunnel to forward a local port to the port on which your MySQL server is running inside the Docker container. This allows you to use MySQL's standard connection tools as if the MySQL server were running on your local machine.
   - Use the command:

     ```bash
     ssh -L local_port:docker_container_ip:container_port user@remote_server -p ssh_port
     ```
   
   - `local_port`: This is the port on your local machine where you want to access MySQL.
   - `docker_container_ip`: The IP address of the Docker container running MySQL. This is often `127.0.0.1` (localhost) if you haven't set up a custom network in Docker.
   - `container_port`: The port on which MySQL is running inside the Docker container.
   - `user`: Your SSH username on the remote server.
   - `remote_server`: The IP address or hostname of the remote server.
   - `ssh_port`: The port for SSH on the remote server (default is 22).

2. **Connect to MySQL**:
   - After setting up the SSH tunnel, you can connect to the MySQL server using your favorite MySQL client or command-line tool, just as if it were running on your local machine.
   - Use the MySQL connection command:

     ```bash
     mysql -h 127.0.0.1 -P local_port -u mysql_user -p
     ```
   
   - `127.0.0.1`: This is the local host IP because you are connecting through the SSH tunnel.
   - `local_port`: The local port you forwarded in the SSH tunnel.
   - `mysql_user`: Your MySQL username.

3. **Additional Considerations**:
   - Ensure that the MySQL server inside the Docker container is configured to accept connections from the host where the SSH tunnel terminates. This may require adjusting the `bind-address` in the MySQL configuration.
   - Make sure that your user in MySQL has the necessary privileges to connect from the SSH tunnel’s endpoint.
   - If you’re using Docker Compose, ensure that the ports for the MySQL service are properly exposed or accessible to the host where the SSH tunnel is set up.

4. **Security**:
   - Always use secure passwords and consider additional security measures, such as using SSH keys for authentication.
   - Keep your software (SSH, Docker, MySQL) up to date to ensure you have the latest security patches.

By following these steps, you should be able to securely connect to a MySQL server running inside a Docker container on a remote server using only SSH access.


## Connect to MySQL

```bash
# Test in server

mysql -h 127.0.0.1 -P 3306 -u root -p12345678

# Open ssh tunnel (local port is 3206 to avoid confusion with local server)
ssh -L 3206:127.0.0.1:3306 root@karasi.om -p 22

## LetsEncrypt

```bash
certonly --webroot --webroot-path=/var/www/certbot/ --email admin@karasi.om --agree-tos --no-eff-email -d karasi.om -v
```


 mysqldump -u root -p12345678 eventom --routines > eventom.dump