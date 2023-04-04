# Fridges

This is a ASP.NET Core Web-API project for managing fridges and products.

This is only a server part of a client-server application.

## Features of this project:
- CRUD operations with fridges, products and fridge-models
- Authorization based on jwt
- Access & refresh tokens
- Access to certain endpoints depending on the role
- Validation of all input data
- Unit tests
- Clean architecture

## Role-based authorization
- To work correctly in this application, there must be a user with the "Admin" role!
- Only users with the "Admin" role can assign roles to users.
- Only users with the "Product-maker" role can create/update/delete products.
- Only users with the "Fridge-maker" role can create/update/delete fridge models.

## Endpoints

<details><summary>Fridge</summary>
    
  `GET` **Get all fridges** - *{host}/api/fridges/*
  
  `GET` **Get products in fridge** - *{host}/api/fridges/{id}*
  
  `POST` **Create new fridge** - *{host}/api/fridges/*
  
  `PUT` **Update fridge** - *{host}/api/fridges/*
  
  `DELETE` **Delete fridge** - *{host}/api/fridges/{id}*
  
  `POST` **Add products** - *{host}/api/fridges/{fridge-id}/products/*
  
  `DELETE` **Remove products** - *{host}/api/fridges/{fridge-id}/products/{product-id}*
  
  `PATCH` **Update quantity** - *{host}/api/fridges/*
</details>

<details><summary>Product</summary>

  `GET` **Get all products** - *{host}/api/products/*
  
  `GET` **Get single product** - *{host}/api/products/{id}*
  
  `POST` **Create new product** - *{host}/api/products/*
  
  `PUT` **Update product** - *{host}/api/products/*
  
  `DELETE` **Delete product** - *{host}/api/products/{id}* 
</details>

<details><summary>Fridge model</summary>

  `GET` **Get all fridge models** - *{host}/api/fridge-models/*
  
  `GET` **Get single fridge model** - *{host}/api/fridge-models/{id}*
  
  `POST` **Create new fridge model** - *{host}/api/fridge-models/*
  
  `PUT` **Update fridge model** - *{host}/api/fridge-models/*
  
  `DELETE` **Delete fridge model** - *{host}/api/fridge-models/{id}*
</details>

<details><summary>Auth</summary>

  `GET` **Get all users** - *{host}/api/auth/users/*
  
  `GET` **Get all roles** - *{host}/api/auth/roles/*
  
  `POST` **Register** - *{host}/api/auth/register*
  
  `POST` **Login** - *{host}/api/auth/login*
  
  `GET` **Refresh token** - *{host}/api/auth/refresh-token*
  
  `POST` **Give role** - *{host}/api/auth/give-role*
</details>


## API requests examples
    
  <details>
  <summary>Get list of fridges</summary>
  
  ### Request

  `GET /api/fridges/`

  ### Response
  ```
  [
    {
        "id": "7f518f88-994f-4367-ab56-c76723c97c91",
        "name": "Iron Box",
        "ownerName": "o_o",
        "fridgeModel": {
            "id": "71880368-2643-4802-a9aa-5b953cc22fbd",
            "name": "ATLANT ХМ 4624-101",
            "year": 2021
        }
    },
    {
        "id": "afca670a-8cb5-4165-85fd-c7b62dfd8e62",
        "name": "Refridgerator-3000",
        "ownerName": "Mike",
        "fridgeModel": {
            "id": "a6d1ab78-9b6c-4c11-b602-d0793d0033b2",
            "name": "Indesit ITR 4200 W",
            "year": 2009
        }
    }
  ]
  ```
  </details>

  <details>
    <summary>Get a specific fridge with products</summary>

  ### Request

  `GET /api/fridges/7f518f88-994f-4367-ab56-c76723c97c91`

  ### Response
  ```
     {
      "fridge": {
          "id": "7f518f88-994f-4367-ab56-c76723c97c91",
          "name": "Iron Box",
          "ownerName": "o_o",
          "fridgeModel": {
              "id": "71880368-2643-4802-a9aa-5b953cc22fbd",
              "name": "ATLANT ХМ 4624-101",
              "year": 2021
          }
      },
      "products": [
          {
              "product": {
                  "id": "6b4a60a5-4c64-4530-b8e9-d4f63468a34a",
                  "name": "Ice cream",
                  "defaultQuantity": 1
              },
              "quantity": 5
          },
          {
              "product": {
                  "id": "925d4328-b60b-4a2a-90cc-f39229b9a279",
                  "name": "chicken",
                  "defaultQuantity": 2
              },
              "quantity": 2
          }
      ]
  }
  ```
  </details>

  <details>
    <summary>Get a non-existent fridge</summary>

  ### Request

  `GET /api/fridges/55afa767-a35b-4687-9317-919e481ccfd4`

  ### Response
  ```
  {
      "error": "Fridge not found."
  }
  ```
  </details>

  <details>
    <summary>Create a new fridge</summary>

  ### Request

  `POST /api/fridges/`
  #### Body
  ```
  {
      "name": "New fridge",
      "ownerName": "Pedro",
      "fridgeModelId": "a6d1ab78-9b6c-4c11-b602-d0793d0033b2"
  }
  ```

  ### Response
  ```
  {
      "id": "264d6499-7977-447e-8a7b-a4ac1c6c801c",
      "name": "New fridge",
      "ownerName": "Pedro",
      "fridgeModel": {
          "id": "a6d1ab78-9b6c-4c11-b602-d0793d0033b2",
          "name": "Indesit ITR 4200 W",
          "year": 2009
      }
  }
  ```
  </details>

  <details>
    <summary>Update fridge</summary>

  ### Request

  `PUT /api/fridges/`
  #### Body
  ```
  {
      "id": "264d6499-7977-447e-8a7b-a4ac1c6c801c",
      "name": "New fridge",
      "ownerName": "Elsa",
      "fridgeModelId": "a6d1ab78-9b6c-4c11-b602-d0793d0033b2"
  }
  ```

  ### Response
  ```
  {
      "id": "264d6499-7977-447e-8a7b-a4ac1c6c801c",
      "name": "New fridge",
      "ownerName": "Elsa",
      "fridgeModel": {
          "id": "a6d1ab78-9b6c-4c11-b602-d0793d0033b2",
          "name": "Indesit ITR 4200 W",
          "year": 2009
      }
  }
  ```
  </details>

  <details>
    <summary>Delete fridge</summary>

  ### Request

  `DELETE /api/fridges/264d6499-7977-447e-8a7b-a4ac1c6c801c`

  ### Response 
  <sup>(204 No Content)</sup>
  </details

