from fastapi import FastAPI, Response
import asyncio
import threading
import redis
from kafka_consumer import consume_messages
from prometheus_client import start_http_server, Counter, generate_latest, CONTENT_TYPE_LATEST
import os

app = FastAPI()

# Prometheus counters
kafka_events_received = Counter('kafka_user_registered_received', 'Number of Kafka events received')
emails_sent = Counter('emails_sent_total', 'Number of emails successfully sent')

threading.Thread(target=lambda: start_http_server(9100)).start()

redis_client = redis.Redis(host=os.getenv("REDIS_HOST", "redis-cache"), port=6379, decode_responses=True)

@app.get("/health")
def health_check():
    return {"status": "Notification Service is running"}

@app.get("/emails/sent")
def get_sent_emails():
    emails = redis_client.lrange("emails_sent", 0, -1)
    return {"emails": emails}

@app.on_event("startup")
async def startup_event():
    asyncio.create_task(consume_messages(redis_client, kafka_events_received, emails_sent))

@app.get("/metrics")
def metrics():
    return Response(generate_latest(), media_type=CONTENT_TYPE_LATEST)