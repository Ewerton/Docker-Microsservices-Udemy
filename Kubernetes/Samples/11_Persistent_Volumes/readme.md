# Volumes

### Persistent Volume (PV)
É uma abstração do dispositivo de armazenamento físico conectado ao kubernetes

### Persistent VOlume Claim (PVC)
Quando o usuário precisa de um local de armazenamento ele deve criar um PVC, que é uma espécie de "pedido de espaço de armazenamento", se no kubernetes houver algum PV que corresponda aos requisitos deste pedido (tamanho, modo de acesso, etc), todos os PV que correspondam ao pedido serão associados ao POD

### StorageClass
Quando nenhum PV é encontrado para os requisitos de uma PVC, um StorageClass, que usa provisionadores e parametros pre-definidos, é criado para disponibilizar o armazenamento solicitado.


## Provisionamento Estático 
É quando um administrador deixa os PV's todos criados e prontos para uso. Usuário e aplicações que precisarem de armazenamento devem solicitar através de uma PVC.

Quando o usuário/aplicativo terminar seu trabalho e não precisar mais do armazenamento (quando um POD morre, por exemplo), o armazenamento associado ao POD é reciclado e fica disponivel novamente para atender aos proximos pedidos

## Provisionamento Dinámico (Recomendado)
Existe um PVC que está associado à uma StorageClasse, quando o PVC é executado para solicitar armazenamento, um novo PV é criado e associado ao POD requisitante.

## Ciclos de Vida de PV e PVC
- provisioning: quando um PV é criado, mas ainda não está disponivel para uso
- binding: quando um PV é associado a um PVC
- Using: quando um POD está usando o armazenamento através do PVC
- Reclaiming: quando um POD não está mais usando o armazenamento e o PV está sendo reciclado

## Estados do PV
- Available: quando um PV está disponivel para uso
- Bound: quando um PV está associado a um PVC
- Released: quando um PVC foi deletado, mas o PV ainda não foi reciclado
- Failed: quando um PV não pode ser reciclado (ocorreu um erro no PV)


# Testes
Para criar um PV aplique o arquivo `persistentvolume.yaml` no cluster `kubectl apply -f persistentvolume.yml`

Use `kubectl get pv` para verificar se o PV foi criado

Para criar um PVC aplique o arquivo `persistentvolumeclaim.yaml` no cluster `kubectl apply -f persistentvolumeclaim.yml`

Use `kubectl get pvc` para verificar se o PVC foi criado

Para criar um POD que usa o PVC, aplique o arquivo `pod1.yaml` no cluster `kubectl apply -f pod1.yml`

Use `kubectl get all` para verificar se o POD foi criado

Use `kubectl describe pod aspnet-pod` para verificar os detalhes do POD (inclusive os detalhes de storage)

Diferente dos volumes efêmeros, persistent volumes não são perdidos quando o POD encerra, para testar isso:

Use `kubectl exec -it pod/aspnet-pod -- /bin/bash` para entrar no container
entre na pasta `meuvolume` e crie um arquivo lá `echo "Teste de Persistent Volume" > TestePV.txt`

Delete o POD `kubectl delete pod aspnet-pod`

Crie um novo POD `kubectl apply -f pod1.yml`

Entre no container do novo POD `kubectl exec -it pod/aspnet-pod -- /bin/bash`

Entre na pasta `meuvolume` e verifique se o arquivo `TestePV.txt` está lá


