# DESAFIO TÉCNICO – DESENVOLVIMENTO DE API DE CONSULTA DE CRÉDITOS

## Instruções de instalação e execução

### Prerequisitos
Verifique se você possui [Docker](https://www.docker.com/get-started) instalado.

### Configurações

As configurações de connection string, usuário e senhas estão no arquivo .env

``` bash
CONFIG_PATH=".\\Config.json"
ACCEPT_EULA="Y"
MSSQL_SA_PASSWORD: "!Pwd12345678@"

POSTGRES_USER: "postgres"
POSTGRES_PASSWORD: "postgres"
POSTGRES_DBNAME: "ConsultaCreditoService"

DB_CONNECTION_STRING: "Host=postgres;Port=5432;Database=ConsultaCreditoService;Username=postgres;Password=postgres;Include Error Detail=true"
SERVICE_BUS_CONNECTION_STRING: "Endpoint=sb://servicebus/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;"
```

### Execução

1. Execute o comando
``` bash
# Clonar repositório
git clone https://github.com/viniciusehonda/DesafioAPIConsultaDeCreditos.git	

# Navegar para o diretório do projeto
cd ConsultaCreditoService

# posicionar cmd na pasta raiz do projeto
# executar docker compose up
docker compose up

# a api por padrão estará rodando no endereço: http://localhost:5025
```
- Endereço padrão api
[api](http://localhost:5025)

- Self Health check
[self](http://localhost:5025/self)

- Ready health check
[ready](http://localhost:5025/ready)

### ▶️ Testando a API com o arquivo .http

``` bash
/src/ConsultaCreditoService.Api/ConsultaCreditoService.Api.http
```

#### ▶️ Como usar no Visual Studio

Abra o arquivo api.http.
Sobre cada requisição aparecerá o botão "Send Request".
Clique para executar.
A resposta aparecerá ao lado com status, headers e body.