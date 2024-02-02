#!/bin/bash

# create context if not exists
docker context create remote_ticket --docker host=ssh://root@45.76.36.193
docker context use remote_ticket 

docker ps   # list running containers
scp -r conf root@45.76.36.193:/root   # copy new docker-compose

# docker compose -f Deployment/docker-compose.yml pull
docker compose -f Deployment/docker-compose.yml up -d   # running docker

docker ps

# docker context to local
docker context use default