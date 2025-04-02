# Docker Compose Basics: Nginx, Networking, and Volumes
This guide walks you through using Docker Compose to:

- Run an NGINX container (```my-nginx```)

- Run an Alpine container (```ping-server```)

- Connect them on a custom Docker network

- Share a Docker-managed volume between them

## 🔧 Prerequisites
- Docker installed

- Docker Compose (included with Docker Desktop)

## 📂 Project Structure
```plaintext
.
├── docker-compose.yml
└── README.md
```
## 🚀 Getting Started
### 1. Clone the project (if applicable)
   Or just make sure ```docker-compose.yml``` is in your current directory.

### 2. Start the containers
```bash
   docker-compose up -d
```
   📝 ```-d``` = detached mode, runs containers in the background.

This will:

- Launch ```my-nginx``` and ```ping-server```

- Attach both to a custom network ```my-network```

- Mount a shared volume ```my-volume``` on ```/data``` inside ```ping-server```

## 🧪 Test It
### 1. Connect to my-nginx
```bash
   docker exec -it my-nginx bash
```
   Then, install ping tools (already handled in the Compose ```command```):

```bash
ping ping-server
```
You’ll see responses proving the two containers are connected over the custom Docker network.

### 2. Test Volume Sharing
   From within ```my-nginx```:

```bash
echo "Hello from nginx" > /data/hello.txt
```
Now check the file from a new container using the same volume:

```bash
docker run --rm -v my-volume:/data alpine cat /data/hello.txt
```
Output:

```csharp
Hello from nginx
```

### ✅ Key Concepts

- ```services```: Defines each container

- ```networks```: Allows containers to talk to each other

- ```volumes```: Persistent, shared data between containers

- ```command```: Override default behavior so the containers stay alive and interactive

## 🧹 Clean Up
```bash
docker-compose down -v
```
Stops and removes containers, network, and the volume.