docker ps
docker cp Deployment/eventom_bk.sql b7d3b9858fff:eventom_bk.sql
docker exec -i b7d3b9858fff /usr/bin/mysql -u root --password=12345678 eventom < eventom_bk.sql
