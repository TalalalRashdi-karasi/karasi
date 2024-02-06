#!/bin/bash

# Variables
DOCKER_CONTEXT="remote_ticket"
COMPOSE_FILE_PATH="Deployment/docker-compose.yml"
DOCKER_HOST="ssh://root@45.76.36.193"
SERVICE_NAME="dotnetapp"

# Create Docker context if it does not exist (checks for existence first)
if ! docker context inspect $DOCKER_CONTEXT > /dev/null 2>&1; then
  docker context create $DOCKER_CONTEXT --docker host=$DOCKER_HOST
fi
docker context use $DOCKER_CONTEXT 


# remove unused images
docker system prune -af

# Build the dotnetapp Docker image
docker compose -f $COMPOSE_FILE_PATH build $SERVICE_NAME

# Uncomment the next line if you want to stop and remove all services
# docker compose -f $COMPOSE_FILE_PATH down

# Stop and remove the specific service container for dotnetapp
docker compose -f $COMPOSE_FILE_PATH stop $SERVICE_NAME
docker compose -f $COMPOSE_FILE_PATH rm $SERVICE_NAME -f

# Start the containers again
docker compose -f $COMPOSE_FILE_PATH up -d

echo "Rebuild and redeployment completed."

# Switch Docker context back to default
docker context use default
