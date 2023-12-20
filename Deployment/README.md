# Deployment


## Copy config to remote server
    
    ```ssh
    # copy config to remote server
    scp -r Deployment root@45.76.36.193:/root
    ```


## Run docker-compose remotely

```ssh 
# adding context to docker (after you added ssh key to remote server)
docker context create remote_ticket --docker host=ssh://root@45.76.36.193


# change docker context to remote_ticket
docker context use remote_ticket

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


docker exec -i fb9f3ef4926a /usr/bin/mysql -u root --password=12345678 eventom < eventom_bk.sql

# check connection
docker exec -it fb9f3ef4926a /bin/bash

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