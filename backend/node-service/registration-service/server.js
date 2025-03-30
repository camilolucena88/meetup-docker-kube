const express = require("express");
const { Pool } = require("pg");
const kafka = require("kafkajs");
require("dotenv").config();
const prometheus = require("express-prometheus-middleware");
const app = express();
app.use(express.json());

const pool = new Pool({
    connectionString: process.env.DATABASE_URL,
});

const kafkaClient = new kafka.Kafka({
    brokers: [process.env.KAFKA_BROKER],
});
const producer = kafkaClient.producer();

app.get("/health", (req, res) => {
    res.json({ status: "Registration Service is running" });
});

app.post("/register", async (req, res) => {
    const { name, email, password } = req.body;
    try {
        const result = await pool.query(
            "INSERT INTO users (name, email, password) VALUES ($1, $2, $3) RETURNING *",
            [name, email, password]
        );

        await producer.connect();
        await producer.send({
            topic: "user-registered",
            messages: [{ value: JSON.stringify({ email }) }],
        });

        res.status(201).json({ message: "User registered successfully", user: result.rows[0] });
    } catch (error) {
        console.error(error);
        res.status(500).json({ error: "Error registering user" });
    }
});

app.use(prometheus({
    metricsPath: '/metrics',
    collectDefaultMetrics: true,
    requestDurationBuckets: [0.1, 0.5, 1, 1.5]
}));

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
    console.log(`Registration Service running on port ${PORT}`);
});