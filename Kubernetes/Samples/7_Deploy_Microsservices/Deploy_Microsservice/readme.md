# Deploy de uma solu��o de Microsservi�os no Kubernetes

A Soclu��o ter� a seguinte Arquitetura:
![FluxoDeTrabalho.png]

## Criando as Imagens do Backend e do Frontend da Solu��o

Use o comando `docker build -f backend\Dockerfile -t backend .` Note o ponto ( . ) no final do comando

Use o comando `docker build -f frontend\Dockerfile -t frontend .` Note o ponto ( . ) no final do comando

## Envie a imagem para o DockerHub

Use `docker login` para se logar na sua conta do DockerHub

Use `docker tag backend <seu_usuario>/pizzabackend` para criar uma tag para o backend

Use `docker tag frontend <seu_usuario>/pizzafrontend` para criar uma tag para o frontend

Use `docker push <seu_usuario>/pizzabackend` para enviar a imagem do backend para o DockerHub

Use `docker push <seu_usuario>/pizzafrontend` para enviar a imagem do frontend para o DockerHub

## Deploy dos Servi�os no Kubernetes
O arquivo `backend-deploy.yml` e o arquivo `frontend-deploy.yml` cont�m a defini��o de um `Deployment` e de um `Service` do Kubernetes, tudo no mesmo arquivo.

O arquivo `backend-deploy.yml` o Service do backend est� definido com o tipo `ClusterIP`, ou seja, ele s� pode ser acessado dentro do cluster, isso porque o backend n�o precisa expor nada para o mundo exterior e o frontend est� rodando dentro do mesmo cluster.

J� o arquivo `frontend-deploy.yml` tem seu Service definido como `LoadBalancer` para exp�r a porta para que ela fique acess�vel pelo browser

Use `kubectl apply -f <NomeDoArquivo.yml>` para fazer o deploy no Kubernetes.

## Testando o Deploy

Use `minikube service pizzafrontend` Isso vai criar o tunnel para o servi�o e abrir o browser com a aplica��o rodando.

