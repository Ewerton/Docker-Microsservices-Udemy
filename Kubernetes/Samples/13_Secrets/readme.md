# Secrets

Como o kubernetes usa arquivos yaml declarativos para definir os recursos, é comum que esses arquivos contenham informações sensíveis, como senhas, tokens, etc. Essas informações não devem ser armazenadas no repositório git, pois qualquer pessoa que tenha acesso ao repositório terá acesso a essas informações.

Para resolver esse problema, o kubernetes disponibiliza o recurso de secrets, que permite armazenar informações sensíveis de forma segura.
  

## Criando um secret
Secrets podem ser criados usando:
- kubectl
- Arquivos yaml ou json
- a ferramenta kustomize

### Criando um secret usando kubectl
Cria um Secret com o nome `db-user-pass` que contém nome de usuário e senha:
```bash
kubectl create secret generic db-user-pass --from-literal=username=sa --from-literal=password=sa
```
O parametro `--from-literal` permite passar os valores diretamente na linha de comando. Para passar valores de arquivos, use o parametro `--from-file`.

Se o valor do secret conter caracteres especiais, como `@`, `$`, etc, é necessário usar aspas simples para envolver o valor, como no exemplo abaixo:
```bash
kubectl create secret generic db-user-pass --from-literal=username=sa --from-literal=password='sa@123'
```


### Criando um secret usando um arquivo yaml
O arquivos [secret1.yml](secret1.yml) tem um exemplo de como criar um secret usando a tag `data`. Desta forma você deve informar o valor do secret em base64. Para converter um valor para base64, use o comando `echo -n 'sa' | base64`. 

O arquivo [secret2.yml](secret2.yml) tem um exemplo de como criar um secret usando a tag `stringData`. Desta forma você deve informar o valor do secret em texto puro. O kubernetes converte o valor para base64 automaticamente.

O arquivo [secret3.yml](secret3.yml) tem um exemplo 

Depois é só usar o comando `kubectl apply -f <NomeArquivo>` para criar o secret:


### Criando um secret usando a ferramenta kustomize
O kustomize é uma ferramenta utilizada para gerar secrets. O nome do arquivo de configuração do kustomize é `kustomization.yml`. O arquivo [kustomization.yaml](kustomization.yaml) tem um exemplo de como criar um secret usando a ferramenta kustomize.

Depois de criar o secrect usando o Kustomize você pode usar o comando `kubectl apply -k <DiretorioComArquivoKustomization>.` para criar o secret.


Depois disso, basta referenciar o secret no arquivo yaml do recurso. O arquivo [pod1.yml](pod1.yml) tem um exemplo de como referenciar um secret no arquivo yaml de um pod.


### Comandos Adicionais de Secrets
- `kubectl get secrets` - Lista os secrets
- `kubectl describe secrets <secret-name>` - Exibe detalhes de um secret
- `kubectl delete secrets <secret-name>` - Deleta um secret
- `kubectl edit secrets <secret-name>` - Edita um secret
- `kubectl get secrets <secret-name> -o yaml` - Exibe o yaml de um secret
- `kubectl get secrets <secret-name> -o jsonpath='{.data}'` - Exibe o json de um secret     

## Usando um secret em um arquivo yaml
Os secrets são associados aos pods através de Volumes, que são montados no pod. Para isso, é necessário criar um volume do tipo secret e associar o secret ao volume. O arquivo [pod1.yml](pod1.yml) tem um exemplo de como fazer isso.

- Crie os secrets usando o arquivo Kustomization.yaml `kubectl apply -k <DirComArquivoKustomization>.`
- Copie o nome do secret criado e coloque no arquivo [pod1.yml](pod1.yml)
- Aplique o arquivo [pod1.yml](pod1.yml) usando o comando `kubectl apply -f pod1.yml` para criar o pod.
- Acesse o pod usando o comando `kubectl exec -it <nome-do-pod> -- /bin/bash`
- Entre na pasta `/etc/mysecret` e verifique que lá existem os arquivos `username` e `password`
- Use o comando `cat /etc/mysecret/username` e `cat /etc/mysecret/password` para ver o conteúdo do arquivo (que estarão em base64) e que o pod vai usar.
