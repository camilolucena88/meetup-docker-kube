# Meetup Docker-Kubernetes: Scalable Microservices Deployment

## 🚀 Project Overview
This repository demonstrates how to build, containerize, and deploy a **scalable microservices application** using **Docker, Kubernetes (DOKS), and Rancher**. It includes a backend with multiple services, a frontend, and DevOps infrastructure for CI/CD, monitoring, and security.

## 📁 Repository Structure
```
meetup-docker-kube/
│── backend/               # Backend services (Node.js, Python, .NET)
│   ├── node-service/      # Node.js (Express.js API Gateway)
│   ├── python-service/    # Python (FastAPI Microservice)
│   ├── dotnet-service/    # .NET Core API
│
│── frontend/              # Frontend app (React + Nginx)
│   ├── app/               # React app source code
│   ├── Dockerfile         # Dockerfile for frontend service
│
│── data/                  # Databases, migrations, test data
│   ├── postgres/          # PostgreSQL init scripts
│   ├── redis/             # Redis config
│   ├── kafka/             # Kafka setup
│
│── infrastructure/        # DevOps, Kubernetes, CI/CD
│   ├── docker/            # Docker Compose setup for local development
│   ├── kubernetes/        # K8s manifests and Helm charts
│   ├── ci-cd/             # GitHub Actions, ArgoCD config
│   ├── monitoring/        # Prometheus, Grafana setup
│
│── README.md              # Main documentation
│── .gitignore             # Ignore unnecessary files
│── LICENSE                # Open-source license (MIT, Apache, etc.)
```

## 📌 Tech Stack
- **Backend Services:**
    - [x] **Node.js (Express.js)** - API Gateway
    - [x] **Python (FastAPI)** - Microservice
    - [x] **.NET Core (Minimal API)** - Microservice
- **Frontend:** React + Nginx
- **Databases & Messaging:** PostgreSQL, Redis, Kafka
- **Orchestration:** Docker, Kubernetes (DOKS on DigitalOcean)
- **CI/CD:** GitHub Actions + ArgoCD
- **Monitoring:** Prometheus + Grafana + Loki (for logs)
- **Security:** RBAC, Secrets, Network Policies

## 🛠️ Local Development Setup
### 1️⃣ Install Dependencies
Ensure you have **Docker** and **Docker Compose** installed:
```sh
# Install Docker & Docker Compose
sudo apt update && sudo apt install -y docker docker-compose
```
### 2️⃣ Clone and Run Locally
```sh
git clone https://github.com/your-repo/meetup-docker-kube.git
cd meetup-docker-kube
```
Start the project using Docker Compose (for local dev):
```sh
docker-compose up --build
```

## 🚀 Deploying to Kubernetes (DOKS on DigitalOcean)
### 1️⃣ Install `doctl` (DigitalOcean CLI) & Authenticate
```sh
curl -sL https://github.com/digitalocean/doctl/releases/latest/download/doctl-linux-amd64.tar.gz | tar -xzv
doctl auth init
```
### 2️⃣ Create Kubernetes Cluster
```sh
doctl kubernetes cluster create meetup-cluster --region fra1 --node-pool "size=s-2vcpu-4gb,count=2"
```
### 3️⃣ Deploy Services
```sh
kubectl apply -f infrastructure/kubernetes/
```

## 📊 Monitoring & Logging
- **Prometheus & Grafana** for metrics
- **Loki + Fluentd** for logs
- **Kubernetes Dashboard** for cluster insights

## 🏗️ Roadmap
- [ ] Complete backend service implementation
- [ ] Add database migrations & test data
- [ ] Configure Rancher for cluster management
- [ ] Implement CI/CD workflows
- [ ] Optimize Kubernetes networking & security

## 🤝 Contributing
Contributions are welcome! Feel free to submit PRs or raise issues.

## 📄 License
This project is open-source under the **MIT License**.

