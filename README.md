# 🧾 Order Management System (Microservices-based)

This project is a sample **Order Management System** built with modern microservice architecture principles using `.NET`, `RabbitMQ`, `Keycloak`, and `PostgreSQL`.

---

## 🏗 Architecture Overview

- Microservices (Clean Architecture)
- CQRS + DDD
- Asynchronous communication with **MassTransit + RabbitMQ**
- Identity & Access Management with **Keycloak**
- Data persistence with **EF Core + PostgreSQL**
- Containerized using **Docker Compose**

---

## 🧩 Services

| Service           | Port  | Responsibilities                             |
|-------------------|-------|----------------------------------------------|
| OrderService      | 5001  | Handle orders and statuses                   |
| PaymentService    | 5002  | Process payments via async message bus       |
| CustomerService   | 5003  | Manage customers                             |
| RabbitMQ          | 15672 | Message broker + management UI               |
| PostgreSQL        | 5432  | Shared DB for all services                   |
| Keycloak          | 8080  | Authentication and Authorization             |

---

## 🔐 Authentication

> Auth is handled via **Keycloak** (OAuth2 / OpenID Connect)

- Realm: `ordersystem`
- Client: `orders-api`
- User: `testuser / 1234`
- Token Endpoint:
    POST http://localhost:8080/realms/ordersystem/protocol/openid-connect/token

---

## 🧪 How the System Works

### Order Flow:

1. Client registers a customer (`/api/customers`)
2. Creates an order for that customer (`/api/orders`)
3. `OrderService` publishes `OrderCreatedEvent`
4. `PaymentService` consumes event and "processes" payment
5. After success, it publishes `OrderPaidEvent`
6. `OrderService` consumes it and updates the order status to `Paid`

---

## 🚀 Run the Project

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Docker & Docker Compose](https://www.docker.com/)

### Run All with Docker

```bash
cd Yaml
docker-compose up --build

```

## 🧰 Technologies Used
C# / .NET 8

- ASP.NET Core Web API

- MassTransit + RabbitMQ

- EF Core + PostgreSQL

- Clean Architecture

- CQRS + MediatR

- Keycloak (OpenID Connect)

- Docker

## 🧑‍💻 Developed by
With ❤️ and architecture in mind.
❤️ Pull requests & feedbacks are welcome!
