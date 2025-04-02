#!/bin/bash
kubectl port-forward svc/customer-service 5288:5288 &
kubectl port-forward svc/registration-service 3000:3000 &
kubectl port-forward svc/notification-service 5000:5000 &
kubectl port-forward svc/kafka-ui 8081:8080 &
kubectl port-forward svc/grafana 3001:3000 &
kubectl port-forward svc/prometheus 9090:9090 &