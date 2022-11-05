# Exemplo de Aplicação ASP.NET com SQLServer usando Persistent Storage no Kubernetes

Será feito o seguinte:
- Criar um Persistent Volume Claim (PVC) para armazenar os dados do SQLServer
- Criar um Deployment para o SQLServer com Persistent Volume (PV)
- Criar um Service para expôr o SQLServer
- Criar uma aplicação ASP.NET Core MVC que usa o container do SQL Server em execução no POD para gerenciar informações sobre filmes
  - Criar a APlicação ASP.NET Core MVC
  - Criar um Dockerfile para o container da aplicação
  - Criar a imagem da aplicação
- Criar um Deployment da aplicação MVC usando a imagem (mvcfilmes) criada
- Criar um Service para expor o POD da aplicação ASP.NET Core MVC
- Aplicar as Migrations e criar o banco de dados FilmesDB no SQLServer
- Deletar o POD do SQLServer e verificar se os dados estão preservados


# Testando

Use `kubectl apply -f mssql-pvc.yml` para criar o PVC

Use `kubectl apply -f sql-deploy.yml` para criar o Deployment do SQLServer

> **Atenção:** Usando o Docker como driver poderá ocorrer um [erro de Timeout ImagePullBackOff](https://github.com/kubernetes/minikube/issues/14806) então, se este erro ocorrer, execute `minikube ssh docker pull mcr.microsoft.com/mssql/server:2019-latest` para primeiro baixar a imagem do SQLServer no Minikube e depois execute `kubectl apply -f sql-deploy.yml` para criar o POD.

Use `kubectl apply -f sql-service.yml` para criar o Service do SQLServer

A aplicação possui uma Action chamada RunMigrate na HomeController, esta action é que executa a migration no banco.

A string de conexão da aplicação foi definida da sequinte forma:
``` json
 "ConnectionStrings": {
    "DefaultConnection": "Data Source=mssql-service,1433;Initial Catalog=FilmesDB;Persist Security Info=True;User ID=SA;Password=Numsey#2022"
  },
```

Note que o nome do Data Source é o nome dado ao Service do Kubernetes assim como a porta definida lá no arquivo `sql-service.yml`

Crie a imagem da aplicação usando o comando `docker build -t mvcfilmes .`

Crie uma tag para a imagem usando o comando `docker tag mvcfilmes ewernet/mvcfilmes`

Suba a imagem para o DockerHub usando o comando `docker push ewernet/mvcfilmes`

Use `kubectl apply -f app-deploy.yml` para criar o Deployment da aplicação MVC

Use `kubectl apply -f app-service.yml` para criar o Service da aplicação MVC no Kubernetes

Use `minikube service crudapp-service` para abrir o navegador com a aplicação MVC

Teste a aplicação. No primeiro acesso a migration não foi rodada então você deverá acessar /home/runmigrate para rodar a migration e criar o banco de dados.

Delete o POD que contem o SQLServer usando `kubectl delete pod mssql-deployment-xxxxxx`

Como existe um ReplicaSet, em alguns instantes o POD será recriado

Acesse novamente a aplicação `minikube service crudapp-service` e veja que os dados não foram perdidos

Delete o deployment do SQLServer usando `kubectl delete deployment mssql-deployment`

Refaça o processo de criação do POD do SQLServer usando `kubectl apply -f sql-deploy.yml`

Access a aplicação `minikube service crudapp-service` e veja que os dados não foram perdidos