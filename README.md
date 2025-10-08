# 🧾 Payments Orchestration System

A **production-grade payments orchestration platform** built with **.NET 9** and designed to demonstrate senior-level backend architecture skills.  
It provides unified APIs for managing payments, retries, refunds, reconciliation, and integrations with multiple payment providers (Stripe, Adyen, Braintree).

---

## 📁 Project Structure

```
src/
├─ Payments.Api/           # ASP.NET Core Web API entrypoint
├─ Payments.Domain/        # Domain entities, value objects, and domain events
├─ Payments.Application/   # Use cases, CQRS handlers, and interfaces (ports)
├─ Payments.Infrastructure/# Persistence, messaging, and outbox/inbox
├─ Payments.Providers/     # Payment gateway adapters (Stripe, Adyen, Braintree)
└─ Payments.Worker/        # Background worker for webhooks, retries, and reconciliation

deploy/
├─ docker/                 # Dockerfiles, docker-compose for local dev
├─ k8s/                    # Kubernetes manifests
└─ terraform/              # Infrastructure as code (optional cloud provisioning)
```

---

## ⚙️ Features

- Unified payment API (create, authorize, capture, refund, cancel)
- Idempotent request handling
- Outbox / inbox pattern for **exactly-once delivery**
- Background workers for webhook and retry processing
- Provider abstraction (Stripe, Adyen, Braintree)
- Domain-driven design with clean architecture
- Comprehensive observability (Serilog + OpenTelemetry)
- CI/CD ready with Docker and Kubernetes manifests

---

## 🧠 Tech Stack

| Layer | Technology |
|-------|-------------|
| **Language** | C# (.NET 9) |
| **Framework** | ASP.NET Core Minimal APIs / Worker Service |
| **Database** | PostgreSQL |
| **Messaging** | RabbitMQ |
| **Observability** | Serilog, OpenTelemetry, Prometheus |
| **Infrastructure** | Docker, Kubernetes, Terraform |
| **Testing** | xUnit, Testcontainers, FluentAssertions |

---

## 🧩 Domain Model Overview

The domain follows **DDD** principles:

- `Payment` – Aggregate root for transaction lifecycle  
- `Refund` – Value object representing reversal of funds  
- `Money`, `Currency` – Value objects for correct monetary operations  
- `PaymentAuthorized`, `PaymentCaptured`, `PaymentRefunded` – Domain events  

---

## 🚀 Getting Started

### 1️⃣ Prerequisites

- .NET 9 SDK  
- Docker & Docker Compose  
- PostgreSQL and RabbitMQ (local or via Docker)

### 2️⃣ Clone and Build

```bash
git clone https://github.com/yourusername/payments-orchestration.git
cd payments-orchestration
dotnet build
```

### 3️⃣ Run Locally

```bash
docker-compose -f deploy/docker/docker-compose.yml up --build
```

### 4️⃣ Access the API

Visit Swagger UI at:  
👉 `http://localhost:5000/swagger`

---

## 🧪 Testing

```bash
dotnet test
```

---

## 🧱 Clean Architecture Layers

```
┌────────────────────────────┐
│        Payments.Api        │   → Presentation Layer (HTTP endpoints)
├────────────────────────────┤
│    Payments.Application    │   → Application logic (CQRS, services)
├────────────────────────────┤
│      Payments.Domain       │   → Core domain models, events, rules
├────────────────────────────┤
│   Payments.Infrastructure  │   → Data, messaging, outbox, providers
└────────────────────────────┘
```

---

## 🧰 Useful Commands

| Command | Description |
|----------|-------------|
| `dotnet build` | Builds all projects |
| `dotnet test` | Runs all test projects |
| `dotnet run --project src/Payments.Api` | Runs the main API |
| `dotnet run --project src/Payments.Worker` | Runs the background worker |
| `docker-compose up` | Spins up the full local environment |

---

## 📈 Future Enhancements

- Provider health scoring and smart routing engine  
- Multi-currency ledger and reconciliation engine  
- Support for SEPA / ACH flows  
- Rate-limiting, circuit breakers, and advanced retries  
- UI Dashboard for provider metrics and routing rules  

---

## 👤 Author

**Adedamola Ajayi** – Backend Engineer (C# / Python / FastAPI)  
📍 Based in Nigeria | Available for remote roles and relocation opportunities in Europe or North America  
💼 [LinkedIn](https://www.linkedin.com/in/adedamolaajayi/) • [GitHub](https://github.com/damolaajayi)

---

## 🪪 License

This project is licensed under the **MIT License** – see the [LICENSE](LICENSE) file for details.
