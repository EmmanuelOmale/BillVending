# Bill Vending System (Microservices Architecture)

This is a modular and scalable **microservices-based backend system** for a bill vending solution. It allows users to fund a wallet, make electricity bill payments, and supports robust **asynchronous event processing**, **failure handling**, and **concurrency control** using **CQRS**, **Entity Framework Core**, and **Clean Architecture** principles.

---

## Monorepo Structure

```
Services/
├── IdentityService/           # Handles user authentication & authorization
├── WalletService/             # Manages wallet creation, funding, and balance
├── BillPaymentService/        # Vends electricity & handles transaction failures
├── Common/                    # Common utilities, constants, response wrappers, etc.
└── SharedInfrastructure/      # Shared abstractions, base classes, middlewares
```

---

## WalletService Folder Structure

```
WalletService/
├── Application/               # Application layer (CQRS - commands, queries, DTOs)
│   ├── Commands/
│   │   └── CreateWallet/
│   │       ├── CreateWalletCommand.cs
│   │       └── CreateWalletCommandHandler.cs
│   └── Queries/
│       └── GetWalletByUserId/
│           ├── GetWalletByUserIdQuery.cs
│           └── GetWalletByUserIdQueryHandler.cs
│
├── Domain/                    # Core business entities & repository contracts
│   ├── Entities/
│   │   └── Wallet.cs
│   └── Repositories/
│       └── IWalletRepository.cs
│
├── Infrastructure/            # Database, persistence, EF Core context & implementations
│   ├── Persistence/
│   │   ├── WalletDbContext.cs
│   │   └── WalletRepository.cs
│   └── Shared/
│       └── IRepository.cs
│
└── Api/                       # HTTP endpoints and web entry point
    ├── Controllers/
    │   └── WalletController.cs
    └── Program.cs
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|------|------------|
| Language | C# (.NET 8) |
| API Framework | ASP.NET Core Web API |
| Architecture | CQRS, Clean Architecture |
| Persistence | Entity Framework Core, PostgreSQL |
| Messaging | In-memory queue (mocked), ready for SQS/RabbitMQ |
| Auth | Identity + JWT |
| Logging | Structured logging (ILogger), ready for Serilog |
| Tooling | VS Code + CLI (`dotnet` CLI) |
| Testing | xUnit + FluentAssertions (not included yet) |

---

## 📌 Features

- 🔐 **Authentication & Authorization** via IdentityService
- 👛 **Wallet Management** – Create, fund, check balance
- ⚡ **Electricity Bill Payment** via BillPaymentService
- 💥 **Robust Failure Handling** with rollback and event-based compensation
- 🧵 **Concurrency Control** using EF Core + transactions
- ⚙️ **CQRS Pattern** – Clean separation of reads/writes
- 🔁 **Asynchronous Processing** via message queues (mocked)
- 🧪 Ready for **Unit Testing**, **Logging**, **Tracing**

---

## 🚀 Running the Project

> This project is CLI-first. All actions can be performed from your terminal.

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)
- [Docker](https://www.docker.com/) (optional)
- VS Code (recommended)

---

### 🔧 Setup (Local)

```bash
# Navigate to the service
cd Services/WalletService

# Restore dependencies
dotnet restore

# Apply EF Core migrations (if any)
dotnet ef database update --project Infrastructure --startup-project Api

# Run the API
dotnet run --project Api
```

---

## 🧪 Example API Endpoints

| Action | Method | Endpoint |
|--------|--------|----------|
| Register Wallet | `POST` | `/api/wallet/create` |
| Fund Wallet | `POST` | `/api/wallet/fund` |
| Check Balance | `GET` | `/api/wallet/balance/{userId}` |
| Pay Bill | `POST` | `/api/wallet/paybill` |

> Swagger/OpenAPI support will be added for easier testing.

---

## 📦 Project Architecture (Clean + CQRS)

- **Application Layer**: Contains use case logic (commands/queries + handlers)
- **Domain Layer**: Holds core entities and business logic contracts
- **Infrastructure Layer**: Implementation of repositories, external API integrations, database
- **API Layer**: Controllers, request models, and dependency injection setup

Each microservice is **independently deployable** and communicates via **async messaging** (mocked for now).

---

## 🔄 Future Enhancements

- Integrate **RabbitMQ / Amazon SQS**
- Add **unit & integration tests**
- Implement **retry mechanisms**
- Add **Swagger/OpenAPI** documentation
- Introduce **Docker Compose** for orchestration
- Add **Distributed Tracing (OpenTelemetry)**

---

## 🧠 Project Highlights

- **Concurrency Handling**: Prevents race conditions with EF Core and transaction scopes.
- **Failure Compensation**: Uses a background worker to rollback failed transactions.
- **Scalability**: Modular services allow independent scaling and team ownership.
- **Simplicity**: Built with clarity and extensibility in mind.

---

## 🤝 Contributing

Open to collaborations! Feel free to fork, raise issues, and suggest improvements.

---

## 📄 License

MIT License

---

## 👨🏽‍💻 Author

**Omale Emmanuel]**  
Backend Engineer | .NET & Distributed Systems  
📍 Nigeria  
🌐 [GitHub](https://github.com/EmmanuelOmale) | 🐦 Twitter: [[@A1Omale]](https://x.com/A1Omale)
