# 🧭 Microservice Architecture – Docker Compose Setup

This project demonstrates a **polyglot microservice architecture** with services written in **Node.js**, **Python**, and **.NET**, communicating via **Kafka**, persisting data with **PostgreSQL**, and exposing telemetry through **Prometheus-compatible metrics**.

All services are containerized and orchestrated using **Docker Compose**, making local development and testing seamless.

---

## 🧱 Services Overview

| Service Name        | Language    | Purpose                                        |
|---------------------|-------------|------------------------------------------------|
| `registration-service` | Node.js     | Handles user registrations and publishes events to Kafka. |
| `notification-service` | Python (FastAPI) | Consumes Kafka events and sends email notifications. |
| `customer-service`     | .NET Core  | Listens to Kafka events and persists user data in a secondary DB. |

Each service exposes a `/health` endpoint and a `/metrics` endpoint compatible with Prometheus.

---

## 🔭 Observability

| Tool       | Purpose                                | Access URL             |
|------------|----------------------------------------|------------------------|
| **Prometheus** | Scrapes `/metrics` from services for monitoring | `http://localhost:9090` |
| **Grafana**    | Visualizes Prometheus data          | `http://localhost:3001` |

- Dashboards are preconfigured to monitor service health and request metrics.
- Each microservice exposes a `/metrics` endpoint (e.g., `http://localhost:<port>/metrics`).

---

## 🧪 Tech Stack Summary

- 🟦 **Node.js (Express)** – RESTful API and Kafka producer
- 🐍 **Python (FastAPI)** – Kafka consumer and email sender
- 🟪 **.NET Core (EF)** – Kafka consumer and PostgreSQL persistence
- 🐘 **PostgreSQL** – Primary and secondary data stores
- 🧵 **Kafka** – Event-driven communication
- 📊 **Prometheus + Grafana** – Metrics scraping and visualization
- 🐳 **Docker Compose** – Local orchestration

---

## 🚀 Getting Started

### 1. Prerequisites
- Docker & Docker Compose installed
- Ports `3000`, `5000`, `6000`, `9090`, `3001` free (or configurable)

## 📦 Future Enhancements
- Add Zipkin or OpenTelemetry for tracing

- API Gateway or service mesh

- Kubernetes manifest (for production parity)

- AuthN/AuthZ integration

## 🧩 Architecture Summary & Real-World Use
- This project implements a polyglot microservice architecture using:

- Event-driven communication (Kafka)

- Relational data persistence (PostgreSQL)

- High-performance caching (Redis)

- REST APIs for frontend integration

- Full observability (Prometheus + Grafana)

## ✅ This setup reflects real-world use cases such as:
- User onboarding and engagement platforms

- Event-sourced transaction or activity tracking systems

- Notification infrastructure (email/SMS/push pipelines)

- Multi-service SaaS backends with async workflows

- Real-time fraud detection or anomaly monitoring

- This architecture is designed for modular growth, language-agnostic development, and production-grade observability, making it a strong foundation for scalable, distributed systems in modern applications.

## 🙌 Credits
Built for the talk:
"Introduction to Docker & Kubernetes: A Hands-On Guide for Developers"

