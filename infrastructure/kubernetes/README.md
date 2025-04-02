# Kubernetes Setup with k3d

This guide helps you run the local Kubernetes cluster using k3d, deploy services like Kafka, Redis, Postgres, Grafana, and your microservices.

---

## 🧱 Prerequisites

- [Docker](https://www.docker.com/)
- [k3d](https://k3d.io/)
- [kubectl](https://kubernetes.io/docs/tasks/tools/)
- [Helm](https://helm.sh/) (optional for future improvement)

---

## 🚀 Cluster Setup

Identify which Context is currently active:

```bash
kubectl config get-contexts
```
```bash
kubectl config current-context
```

### 🔸 What is a Context?
A context is a set of access parameters:

Which cluster you're talking to

Which user/credentials you're using

Which namespace you're working in by default

### Using k3d:

Create a new cluster (if required)
```bash
k3d cluster create mycluster
```

List all clusters:
```bash
k3d cluster list
```

List all clusters:
```bash
k3d cluster stop mycluster
```

### 🔹 What is a Cluster?
A cluster is a Kubernetes environment — it includes:

One or more nodes (VMs or containers running Kubernetes components)

A control plane (API server, scheduler, etc.)

The networking and resources to run your workloads

Think of a cluster as a full runtime for your containers, services, volumes, secrets, etc.


### 🧱 What is a Namespace in Kubernetes?
A namespace is a way to logically isolate resources within the same Kubernetes cluster.

Think of it like folders in a shared drive — same server, but different spaces to organize, isolate, and manage things.

#### 🔹 Why Namespaces Exist
Imagine you're running:

A dev environment

A staging environment

A production environment
...all on the same cluster.

You don’t want your dev app messing with your prod services, right?
→ That's where namespaces step in.


Typical Error:
Error: ImagePullBackOff
```bash
kubectl describe pod <pod_id>
```
```bash
kubectl get pods
```
```bash
kubectl get pods
```

```bash
kubectl rollout status deployment <deployment-name>
```

```bash
kubectl rollout restart deployment <deployment-name>
```
Build a new image:
```bash
docker build -t <image-name>:latest .
```

Import the image into the cluster:

```bash
k3d image import <image-name>:latest -c <name-cluster>
```

Access to pod

```bash
kubectl exec -it <pod-name> -- sh
```

### 📦 Future Enhancements

- CI/CD pipeline for automated deployments
- Monitoring and alerting setup
- Horizontal Pod Autoscaler (HPA) for scaling
- Ingress Controller for routing
- Persistent Volumes for data storage
- Helm charts for service deployment
- Secrets management with Vault or Kubernetes Secrets
- Multi-cluster setup for high availability
