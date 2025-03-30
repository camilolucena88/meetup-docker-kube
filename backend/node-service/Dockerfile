### Dockerfile for Registration Service (Node.js + Express)

# Use official Node.js runtime as a parent image
FROM node:18-alpine

# Set working directory
WORKDIR /app

# Copy the rest of the application files
COPY ./registration-service/ .

# Install dependencies
RUN npm install

# Expose the service port
EXPOSE 3000

# Command to run the application
CMD ["node", "server.js"]