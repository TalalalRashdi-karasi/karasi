docker context use remote_ticket    # remote as context
docker ps   # list running containers
scp -r Deployment 
:/root   # copy new docker-compose

docker compose -f Deployment/docker-compose.yml pull
docker compose -f Deployment/docker-compose.yml up -d   # running docker

