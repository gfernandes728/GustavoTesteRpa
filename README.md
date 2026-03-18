# GustavoTesteRpa

Este projeto consiste em uma **API RPA** para coleta de dados em background e um **RpaWorker** que realiza o scraping de forma contínua.  
O RpaWorker popula dados que podem ser consultados via API HTTP, permitindo monitoramento e integração com outros sistemas.

Os dados pegos são os ultimos dados pegos a cada 5 minutos, pelo RpaWorker da ultima cotação, da moeda USD para BRL.

---

## 📁 Estrutura do Projeto

```
GustavoTesteRpa/
├── RpaApi/                            # API de coleta de dados
│   ├── Controllers/
│   │   └── ScrappingController.cs     # Controller da API para obter os dados
│   ├── Program.cs                     # Inicializador da API
│   └── Dockerfile                     # Dockerfile da API
│
├── RpaWorker/                         # Serviço em background para scraping
│   ├── Models/
│   │   └── Quote.cs                   # Modelo de dados para cotação
│   ├── Services/
│   │   ├── IScrapingService.cs        # Interface do serviço de coleta de dados
│   │   └── ScrapingService.cs         # Serviço de coleta de dados
│   ├── ScrapingWorker.cs              # Serviço em background
│   ├── Program.cs                     # Inicializador do Worker
│   └── Dockerfile                     # Dockerfile do Worker
│
├── docker-compose.yml                  # Docker Compose para rodar o projeto
├── README.md                           # Documentação do projeto
└── .gitignore                          # Arquivos e pastas ignoradas pelo Git
```

---

## 1. Como obter pelo Git

```bash
git clone https://github.com/gfernandes728/GustavoTesteRpa.git
cd GustavoTesteRpa
```

### 2. Como iniciar localmente

1. Abra o projeto no Visual Studio ou VS Code.

2. Certifique-se de que a porta 7034 esteja disponível.

3. Restaure os pacotes NuGet:
```bash
dotnet restore
```

4. Compile e rode a API + Worker:
```bash
dotnet run --project RpaApi
dotnet run --project RpaWorker
```

O Worker será executado em background e a API estará disponível em http://localhost:7034/api/scrapping.

### 3. Como rodar pelo Docker

1. Build dos containers:
```bash
docker-compose build
```

2. Iniciar containers:
```bash
docker-compose up
```

A API será exposta em http://localhost:7034/api/scrapping.

O Worker irá rodar no container em background, preenchendo os dados que a API retorna.

### 4. Acesso à API para verificação

Endpoint: GET http://localhost:7034/api/scrapping
Retorno esperado: lista de dados coletados pelo RpaWorker.

Exemplo de teste usando curl:
```bash
curl http://localhost:7034/api/scrapping
```

Ou abra no navegador: http://localhost:7034/api/scrapping

---

### Observações:

**Certifique-se de que o Docker e .NET 8 SDK estejam instalados para rodar o projeto via container.**
**A API e o Worker compartilham dados usando um serviço singleton (ScrapingService) com IHttpClientFactory.**

---

### Tecnologias Utilizadas

**.NET 8 / ASP.NET Core** – desenvolvimento da **RpaApi** e do **RpaWorker**.
**C#** – linguagem principal do projeto.
**HttpClientFactory** – para requisições HTTP dentro do **ScrapingService**.
**BackgroundService / HostedService** – para execução contínua do **RpaWorker**.
**Docker & Docker Compose** – para containerização da **RpaApi** e **RpaWorker**.
**Swagger / OpenAPI** – documentação e teste da **RpaApi**.
**Visual Studio / VS Code** – IDEs para desenvolvimento.
**Git** – versionamento de código.

---

## 👤 Autor

**Gustavo Fernandes**  
📧 guga.728@gmail.com  
