# Helm

O Helm é um gerenciador de pacotes para Kubernetes. Ele permite que você instale e atualize aplicativos em seu cluster Kubernetes de forma fácil e rápida.

O Helm usa um formato de pacote chamado Chart. Um Chart é um pacote de arquivos Kubernetes que descreve um aplicativo Kubernetes. Ele contém todos os recursos necessários para instalar o aplicativo, como objetos de Deployment, Services, ConfigMaps, etc.


## Instalação
Para instalar o Helm, siga as instruções do site oficial: https://helm.sh/docs/intro/install/

## Conceitos do Helm
- Chart: Um pacote de arquivos Kubernetes que descreve um aplicativo Kubernetes. Ele contém todos os recursos necessários para instalar o aplicativo, como objetos de Deployment, Services, ConfigMaps, etc.
- Repository: Um repositório é um local onde os Charts são armazenados. O Helm vem com um repositório chamado stable, que contém Charts de aplicativos populares. Você também pode criar seu próprio repositório.
- release: Uma instância de um Chart executando em um cluster Kubernetes. Cada instalação de um Chart é uma release. E cada release tem um nome exclusivo, como "happy-panda".

## Comandos do Helm
- **helm search hub**: Pesquisa o Artifact Hub que lista charts helm de dezenas de repositorios diferentes. Exemplo `helm search hub wordpress`
- **helm search repo**: Pesquisa os repositórios que eu adicionei ao meu cliente helm (usando `helm repo add`). Exemplo de uso `helm search repo <nome>`
- **helm install NomeDoPacote**: Instala um pacote helm. Exemplo de uso `helm install wordpress` 
- **helm uninstall NomeDoPacote**: Desinstala um pacote helm. Exemplo de uso `helm uninstall wordpress`
- **help upgrade**: Utilizado quando uma nova versão de um chart é liberada ou quando desejo alterar a versão do chart que tenho atualmente
- **helm rollback**: Utilizado para voltar para uma versão anterior de um chart 
- **helm create NomeDoChart**: Cria um novo chart com o nome especificado. Ele cria uma pasta NomeDoChart e dentro desta pasta cria a seguinte estrutura:
    - Chart.yaml: Arquivo de configuração do chart
    - values.yaml: Arquivo de configuração do chart
    - templates: Pasta onde ficam os arquivos de configuração do chart
    - charts: Pasta onde ficam os charts dependentes do chart principal 
- **helm lint NomeDoChart:** Verifica se o arquivo está incorreto ou mal-formado


# Testando o Helm
Para testar o Helm, vamos instalar uma aplicação que está do Docker Hub em https://hub.docker.com/r/macoratti/mvcprodutos/tags 

- Crie o chart usando o comando `helm create mvc`
- Ajuste os arquivos `Chart.yaml` e `values.yaml` definido os valores para o nosso caso:
  - No arquivo `Chart.yaml` altere o nome do chart para `mvc` e a versão para `0.1.0` e altere a versão do appVersion para `1.16.0` entre aspas
  - No values.yaml altere o valor da propriedade `image.repository` para `macoratti/mvcprodutos` e altere o valor da propriedade `image.tag` para `1.0`. Altere tbm o type do service para `NodePort` para permitir que este app seja exposto para fora do cluster e escolha uma porta, 8080, por exemplo
- Instale o chart usando o comando `helm install mvcprodutos .` Note o (.) no final do comando. Vc deve estar dentro da pasta do chart para executar este comando.
- Verifique a instalação usando `kubectl get all|pod|service|etc` ou então `helm list`
- Acesse o app usando o comando `minikube service mvcprodutos` ou então acesse o endereço `http://<ip>:<porta>` onde o ip é o ip do seu cluster e a porta é a porta que você definiu no arquivo values.yaml

# Upgrading and Rollbacking Apps
## Installing the App
- Usaremos a imagem macoratti/pizzaria:latest para testar o upgrade e rollback
- Vamos criar o Chart.yaml usando o comando `helm create pizzaria-chart`
- Vamos validar o chart usando o comando `helm lint pizzaria-chart`
- Ajustaremos os arquivos `Chart.yaml` e `values.yaml` para o nosso caso:
  - No arquivo `Chart.yaml` altere o nome do chart para `pizzaria-chart` e a versão para `0.1.0` e altere a versão do appVersion para `1.0` entre aspas
  - No values.yaml altere o valor da propriedade `image.repository` para `macoratti/pizzaria` e altere o valor da propriedade `image.tag` para `latest`. Altere tbm o type do service para `NodePort` para permitir que este app seja exposto para fora do cluster e escolha uma porta, 8080, por exemplo.
- Instale o chart usando o comando `helm install pizzaria-chart .`. Note o (.) no final do comando. Vc deve estar dentro da pasta do chart para executar este comando.
- verifique a instalação com o comando `helm list` ou `kubectl get all|pod|service|etc`
- Execute a Aplicação usando o comando `minikube service pizzaria-chart` ou então acesse o endereço `http://<ip>:<porta>` onde o ip é o ip do seu cluster e a porta é a porta que você definiu no arquivo values.yaml

## Upgrading the App
- Altere o número de réplicas para 3. Faça isso criando um novo arquivo chamado `values-prod.yaml` e coloque somente o seguinte conteúdo:
```
replicaCount: 3
```	
- Faça o upgrade usando o comando `helm upgrade pizzaria-chart -f values-prod.yaml .` Note o (.) no final do comando. Vc deve estar dentro da pasta do chart para executar este comando.
- Verifique o ambiente com `helm list` e veja que a REVISION agora está 2 e `helm history pizzaria-chart` mostra o histórico de revisões e `kubectl get all` agora deve mostrar 3 Pods para o App de Pizzaria.

## Rolling Back the App
- Verifique o release atual com `helm ls`
- Encontre o número da revisão atual com `helm history pizzaria-chart`
- Retorne para a revisão anterior com `helm rollback pizzaria-chart [release][revisao][flag]` onde:
  - release: nome do release que no nosso caso é `pizzaria-chart`
  - revisao: nome da revisão para onde queremos retornar que no nosso caso é `1`
  - flag: --force para forçar o rollback mesmo que o release atual esteja em um estado diferente do release que vc quer voltar
- Execute `kubectl get all` e veja que agora temos 1 Pod para o App de Pizzaria
- Execute `helm history pizzaria-chart` e veja que temos 3 revisões e a revisão 2 foi desfeita
- Desinstale o chart com `helm uninstall pizzaria-chart`