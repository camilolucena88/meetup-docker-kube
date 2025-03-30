# Registration Service

## Overview
This microservice handles user registrations, stores data in PostgreSQL, and publishes events to Kafka for further processing (e.g., email notifications).

## Endpoints

### Health Check
**GET /health**  
_Checks if the service is running._
#### Response:
```json
{
  "status": "Registration Service is running"
}
```

### Register a User
**POST /register**  
_Registers a new user and publishes an event to Kafka._

#### Request Body:
```json
{
  "name": "John Doe",
  "email": "johndoe@example.com",
  "password": "securepassword"
}
```

#### Response:
```json
{
  "message": "User registered successfully",
  "user": {
    "id": 1,
    "name": "John Doe",
    "email": "johndoe@example.com"
  }
}
```

#### Errors:
- `500 Internal Server Error`: If registration fails.

## Running the Service Locally
1. Ensure **Docker** and **Docker Compose** are installed.
2. Clone the repository and navigate to the project folder.
3. Run the following command:
   ```sh
   docker-compose up --build
   ```
4. The service will be available at `http://localhost:3000`

## Technologies Used
- **Node.js (Express.js)** - Backend API
- **PostgreSQL** - User data storage
- **Kafka** - Event-driven messaging
- **Docker & Kubernetes** - Deployment


