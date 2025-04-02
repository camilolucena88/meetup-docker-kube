# README.md: Docker Basics for Presentation

This README walks through basic Docker commands and examples for the talk *"Introduction to Docker & Kubernetes: A Hands-On Guide for Developers"*. We'll start with a single container, then connect two containers using networking.

## Prerequisites
- Docker installed on your machine ([Install Docker](https://docs.docker.com/get-docker/))
- A terminal ready to go!

---

## Part 1: Basic Container Commands

Let’s start with a simple container using the `nginx` image (a lightweight web server).

### 1. Run a Container
Start a container in the background (detached mode):
```bash
docker run -d --name my-nginx nginx
```

-d: Detached mode.

--name my-nginx: Names the container.

### 2. List Running Containers
```bash
docker ps
```
### 3. Connect to the Container
```bash
docker exec -it my-nginx bash
```
Exit with exit.

### 4. Stop the Container
```bash
docker stop my-nginx
```
### 5. Start the Container
```bash
docker start my-nginx
```
### 6. Remove the Container
```bash

docker rm -f my-nginx
```
Bonus: Hello World
```bash

docker run hello-world
```
Check stopped containers: docker ps -a.

## Connecting Two Containers
Let’s use alpine to demonstrate networking and volumes.
### 1. Create a Network
```bash

docker network create my-network
```

Check the network:
```bash
docker network ls --filter name=my-network
```

Inspect the network:
```bash
docker network inspect my-network
```

Connect services to network:
```bash
docker network connect my-network my-nginx
```````

Check network connections:
```bash
docker inspect my-nginx
```````

### 2. Run Ping Server
```bash

docker run -d --name ping-server --network my-network alpine tail -f /dev/null
```
### 3. Connect to my Nginx Client
```bash

docker exec -it my-nginx bash
```

Inside, install ping:
```bash
apt update && apt install iputils-ping
```

Inside, ping:
```sh

ping ping-server
```
Ctrl+C to stop, exit to leave.

### 4. Add a Volume
```bash

docker volume create my-volume
```
Run server with volume:
```bash

docker run -d --name ping-server --network my-network -v my-volume:/data alpine tail -f /dev/null
```
Run client:
```bash
docker run -it --rm --name volume-server --network my-network -v my-volume:/data alpine sh
echo "Hello from volume" > /data/hello.txt
exit
```
Second server:
```sh
docker run -it --rm -v my-volume:/data alpine cat /data/hello.txt
```
### 5. Clean Up

```bash

docker stop ping-server my-nginx
docker rm ping-server my-nginx
docker network rm my-network
docker volume rm my-volume
```