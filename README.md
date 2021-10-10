# Hospital Center
Educational project with 2 services, communicating via RabbitMQ; Docker; Kubernetes

## Used technologies
- .Net Core 5 API
- InMemory database
- Docker
- Kubernetes
- RabbitMQ

![topology](https://i.ibb.co/nz6dRjx/topology.png)

## Run the project
```
CD K8S
kubectl apply -f ingress-srv.yaml
kubectl apply -f patient-depl.yaml
kubectl apply -f patient-p-srv.yaml
kubectl apply -f rabbitmq-depl.yaml
kubectl apply -f vaccine-depl.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.3/deploy/static/provider/cloud/deploy.yaml
```
- Add zhivkopetkov.com in the windows host file

## End points
- [GET] /api/people => returns all people with their vaccination status

- [GET] /api/people/1001 => returns info for person with ID 10001

- [GET] /api/vaccine => returns all vaccines

- [GET] /api/patient/10001 => returns info for patient with ID 10001 and vaccines

- [POST] /api/people => create person in the patient service and via RabbitMQ is transffered to vaccine service
```
{
    "firstName": "Test",
    "lastName": "TestLast",
    "city": "Sofia",
    "nan": "123123123"
}
```
- [POST] /api/vaccine/injekt => adding vaccine info to the patient in the vaccine service, via RabbitMQ, the status is changed in patient service
```
{
    "patientid": 10001,
    "vaccineid": 1
}
```
