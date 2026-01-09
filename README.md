# Microsserviço Restaurant Identification
Esse microsserviço é responsável por identificar os clientes no momento do pedido, registrando informações como nome, CPF e e-mail. O cliente também tem a opção de iniciar o pedido sem se identificar.

## Infraestrutura
- AspNet core 10.0
- MongoDB
- Redis
- Docker

## Ambiente

### Compilação
`dotnet build Restaurant.Identification.WebApi/Restaurant.Identification.WebApi.csproj`

### Testes
`dotnet test Restaurant.Identification.Model.Test/Restaurant.Identification.Model.Test.csproj`

`dotnet test Restaurant.Identification.Application.Test/Restaurant.Identification.Application.Test.csproj`

### Execução
`dotnet Restaurant.Identification.WebApi.dll`

### Publicação
`dotnet publish Restaurant.Identification.WebApi/Restaurant.Identification.WebApi.csproj -c Release -o dist`

### Docker
`docker build -t restaurant-identification:{{version}} .`

### Kubernetes

#### Redis
`redis-id-service.yaml` ClusterIP para comunicação interna com o App

`redis-id-secrets.yaml` Senha do Redis

`redis-id.yaml` Deployment do Redis

#### MongoDB
`mongo-id-service.yaml` ClusterIP para comunicação interna com o App

`mongo-id-secrets.yaml` Usuário e senha do MongoDB

`mongo-id-configmap.yaml` Inicialização do banco de dados e coleção

`mongo-id.yaml` StatefulSet do MongoDB

#### App
`app-id-service.yaml`  ClusterIP para comunicação interna com o App

`app-id-ingress.yaml` Ingress para comunicação externa com o App

`app-id-secrets.yaml` Chaves pública e privada para autenticação e validação do issuer

`app-id.yaml` Deployment do App

### Arquitetura
```
app-id (AspNet Core)
│
├── mongo-id (MongoDB - clientes, serviços) (get/set)
|
├── redis-id (Redis - Cache de clientes, serviços) (get/set)
|
├── app-order (AspNet Core - gerenciamento de pedidos) (in)
|
├── app-prep (AspNet Core - gerenciamento de preparo) (in)
|
└── app-pay (AspNet Core - gerenciamento de pagamentos) (in)
```

## Endpoints

### Main
- `GET /` retorna `204 No Content` para o teste de vida da API.

### Client
- `GET /client` Retorna um cliente padrão "Não identificado" para efetuar os pedidos quando o cliente não deseja se identificar.
- `GET /client/list?id={id}&id={id}` Retorna os clientes de acordo com seus respectivos Ids.
- `GET /client/cpf/{cpf}` Retorna o cliente com o CPF informado. `404 Not Found` quando não houver cliente cadastrado com o CPF.
- `GET /client/id/{id}` Recupera um cliente pelo id. `404 Not Found` quando não houver cliente cadastrado com o id.
- `POST /client` Registra um novo cliente retornando o id do recurso criado.

### Service
- `POST /service/login` Autentica um serviço através de client_credentials para ter permissão aos demais serviços.