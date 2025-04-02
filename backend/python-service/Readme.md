# Notification Service

## Overview
This microservice listens to **Kafka topic `user-registered`**, processes messages, and sends email notifications.

## Endpoints

### Health Check
**GET /health**  
_Checks if the service is running._
#### Response:
```json
{
  "status": "Notification Service is running"
}
```

## Running the Service Locally
1. Ensure **Docker & Kafka are running**.
2. Clone the repository and navigate to the **notification-service** folder.
3. Run the following command:
   ```sh
   docker-compose -f docker-compose.override.yml up
   ```
4. The service will be available at `http://localhost:5000`

## Technologies Used
- **FastAPI** - Python Backend
- **Kafka Consumer (aiokafka)** - Async event processing
- **Redis** - Caching and rate limiting
- **SMTP** - Email notifications
- **Docker & Kubernetes** - Deployment

---
