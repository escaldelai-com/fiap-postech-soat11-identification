@echo off

REM -- Redis
kubectl apply -f redis-id-service.yaml
kubectl apply -f redis-id-secrets.yaml
kubectl apply -f redis-id.yaml

REM -- MongoDB
kubectl apply -f mongo-id-service.yaml
kubectl apply -f mongo-id-secrets.yaml
kubectl apply -f mongo-id-configmap.yaml
kubectl apply -f mongo-id.yaml

REM -- App
kubectl apply -f app-id-service.yaml
kubectl apply -f app-id-secrets.yaml
kubectl apply -f app-id-ingress.yaml
kubectl apply -f app-id.yaml
