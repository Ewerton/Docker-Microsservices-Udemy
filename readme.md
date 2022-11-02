# Projeto de Microserviços do curso de Docker do Macoratti na UDEMY

Link para o [Curso na UDEMY](https://www.udemy.com/course/docker-essencial-para-a-plataforma-net).


A arquitetura desenvolvida no curso foi a seguinte:
![Arquitetura](Arquitetura.png)

A parte de OCELOT.NET e RabbitMQ não foi mostrada no curso pois estava fora do escopo

Catalog.API -> http://localhost:8000/swagger/index.html


Basket.API -> http://localhost:8001/swagger/index.html

Discount.API -> http://localhost:8002/swagger/index.html

Discount.Grpc -> http://localhost:8003/swagger/index.html

## Para interagir com o banco Postgres (discountdb)
PgAdmin -> http://localhost:5050/
	User: admin@ewerton.com
	Pass: admin1234

##Para interagir com o Container do Mongo (catalogdb)
No terminal: `docker exec -it catalogdb bash`
use `mongosh` para acessar o CLI do mongo
use `show dbs` para verificar os DBs existentes
use `db` para verificar os DBs selecionado
use `use CatalogDB` para selecionar o DB criado
use `show collections` para mostrar as coleções criadas
use `help` para mais comandos


##Para Interagir com o container do Redis (basketdb)
No terminal: `docker exec -it basketdb sh` 
use `redis-cli` para ter acesso ao CLI do redis

use `help` para mais comandos
