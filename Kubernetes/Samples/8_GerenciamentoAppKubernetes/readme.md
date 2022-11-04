# Gerenciando Aplicações no Kubernetes


## Atualizar Deployments
Se o arquivo de deploy mudou (uma atualização de versão, serviço, framework, app, etc) basta aplicar o deploy novamente usando `kubectl apply -f <NomDoArquivo>`

##  Debugar Pods
Use `kubectl describe <pod|svc|etc> <NomeDoObjeto>`

Para verificar logs utilize `kubectl logs <nomeDoPod> -c <NomeDoContainer>` 

Para executar um comando dentro de um POD use `kubectl exec -it <NomeDoPod> -- comando`, por exemplo `kubectl exec -it pizzafrontend -- sh` abre um shell do container que esta rodando a aplicação pizzafrontend


## KubeConfig
No windows fica na pasta `C:\Users\<MeuUsuario>\.kube`

Você pode manioular o config através do `kubectl`. Use o comando `kubectl config --help` para ver as opções disponiveis

## Minikube Dashboard
Caso não queira usar linha de comando vc pode fazer tudo pelo dashboard

Use `minikube dashboard` para abrir o dashboard no navegador







