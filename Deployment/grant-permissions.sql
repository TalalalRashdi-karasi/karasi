# docker exec -it mysql_container_name mysql -u root -p12345678

GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' IDENTIFIED BY '12345678' WITH GRANT OPTION;
FLUSH PRIVILEGES;
