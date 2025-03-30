from aiokafka import AIOKafkaConsumer
import asyncio
import json
import os
from email_sender import send_email

async def consume_messages(redis_client, kafka_events_received, emails_sent):
    kafka_broker = os.getenv("KAFKA_BROKER", "kafka-broker:29092")
    consumer = AIOKafkaConsumer(
        "user-registered",
        bootstrap_servers=kafka_broker,
        group_id="notification-group",
        auto_offset_reset="earliest"
    )

    for attempt in range(10):
        try:
            await consumer.start()
            print("✅ Kafka consumer connected and listening...")
            break
        except Exception as e:
            print(f"❌ Kafka connection failed (attempt {attempt+1}/10): {e}")
            await asyncio.sleep(5)
    else:
        print("💥 Failed to connect to Kafka.")
        return

    try:
        async for msg in consumer:
            kafka_events_received.inc()
            data = json.loads(msg.value.decode("utf-8"))
            email = data.get("email")
            if email:
                await send_email(email)
                emails_sent.inc()
                redis_client.lpush("emails_sent", email)
    finally:
        await consumer.stop()