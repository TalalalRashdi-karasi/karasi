# Access Server

```sh
chmod 400 LightsailDefaultKey-ap-southeast-1.pem
ssh -i 'LightsailDefaultKey-ap-southeast-1.pem'  ubuntu@18.141.208.65 



ssh -i /Users/talal/ticketApp/aws/LightsailDefaultKey-ap-southeast-1.pem ubuntu@18.141.208.65

chmod 600 /Users/talal/ticketApp/aws/LightsailDefaultKey-ap-southeast-1.pem
```

- show nginx log
```sh
sudo tail -f /var/log/nginx/access.log
```

# Access db

- password
8gt0Q3SvWHcjEOT&hT}FkR+dfa6*ij^V



```ssh
mysql -h ls-7dd31fb5e5b2e74745fca8faf53866343877db0b.clooeoq42tuj.ap-southeast-1.rds.amazonaws.com -P 3306 -u dbmasteruser -p
```

```sql
create database eventom;

-- connect to db
use eventom;

-- select from events table
select * from events;
```

```sql

```

```bash
mysql -h ls-7dd31fb5e5b2e74745fca8faf53866343877db0b.clooeoq42tuj.ap-southeast-1.rds.amazonaws.com -P 3306 -u dbmasteruser -p eventom < Backup/eventom.dump
```

# Publish

```sh
bash deploy.sh
```


# SSL Lets Encrypt

sudo apt install certbot python3-certbot-nginx
sudo certbot --nginx -d karasi.om -d www.karasi.om

# verify
sudo systemctl status certbot.timer

# renewal
sudo certbot renew --dry-run

# Nginx

sudo systemctl status nginx
sudo systemctl restart nginx
sudo systemctl reload nginx
sudo systemctl stop nginx
sudo systemctl start nginx

