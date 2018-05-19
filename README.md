# SAMA

## SAMA.network RESTful API project

Available HTTP requests:

```
Sign up:

POST http://localhost:5000/sign-up
content-type: application/json

{
  "email": "user1@sama.network",
  "username": "user1",
  "password": "secret",
  "role": "ngo"
}

-------------------------------------

Sign in:

POST http://localhost:5000/sign-in
content-type: application/json

{
  "email": "user1@sama.network",
  "password": "secret"
}

-------------------------------------

Browse NGOs:

GET http://localhost:5000/ngos

-------------------------------------

NGO details:

GET http://localhost:5000/ngos/NGO_ID

-------------------------------------

Donate NGO:

POST http://localhost:5000/ngos/NGO_ID/donate
authorization: bearer secret_token
content-type: application/json

{
  "funds": 5000
}

-------------------------------------

Account details:

GET http://localhost:5000/me
authorization: bearer secret_token

-------------------------------------

Add funds:

POST http://localhost:5000/me/funds
authorization: bearer secret_token
content-type: application/json

{
  "funds": 5000
}

