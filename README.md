# SwishBackend

![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
 
## Table Of Contents
* [General Info](#general-info)
* [Setup](#setup)


## General Info
This Project is my implementation of an e-commerce platform built with C#/.NET as backend with a microservice perspective taken in mind. 
It's not something that in it's current state is ready for production, but is more my learning platform for setting up a microservice application.
The project will include Authentication,Email service for receiving registration confirm, order confirmation etc, signalR for live data updates.
For payment both Stripe and swedish Swish system will be introduced, though no reall money will be involved for the payments.

The project consist of two project, first this as the backend then the frontend part. See [Frontend](https://github.com/patrik033/SwishFront)

## Todo


     
 - Authentication &#x2610;
   - Add basic authentication &#x2611;
   - Change to http only cookie &#x2610;
 - Basic Email &#x2610;
   - Add more endpoints except when user registers and request a new activation email &#x2610;
 - Setting up the Gateway &#x2610;
 - Ordering Service &#x2610;
   - Added basic stripe interragation(~will be moved to payment API later~) &#x2611;  
 - Adding Carrier Options &#x2610;
   - Add postnord as carrier option &#x2610;
   - Add DHL as carrier option(optional, might change)  &#x2610;
 - Payment Service &#x2610;
   - Basic interaction with stripes embedded form &#x2611;
   - Add swish option as payment alternative payment method  &#x2610;



## Setup
#### Sql Server:

Make sure you've Sql server installed on your platform. It can be found here: [Microsoft Sql Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
A nice addition would be SQL Server Management Studio(SSMS), it can be found here: [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

#### EmailService:
For the currenct state of the project I'm using Azure Key Vault to store my SendGrid and from where the email is sent. If you want to use that service you need to change that. It includes those classes in the project: 

- EmailRegistrationSuccessfullConsumer
- AzureKeyVault
- ServiceExtensions
- SendGridEmailRegisterService


#### Visual Studio:

To run the project you need an IDE, like Visual Studio Community: [Visual Studio](https://visualstudio.microsoft.com/vs/community/)

#### Update The Database:

To update the database simply use the Package Manager Console that comes integrated with visual studio community and simply write "Update-Database"
Make sure you are in the correct project when doing so. As of now only Identity, StoreItems and Orders projects offers this.

#### Make sure RabbitMQ is running:
Either: run this command in the terminal to start rabbitMQ:  docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
Or: navigate to the docker-compose.yml file in terminal and write "docker compose up" to start rabbit mq while docker is running.

## Checkout

- For payment success: use card number 4242 4242 4242 4242
- For cards with unnificient founds: use card number 4000 0000 0000 9995
- For declined payment: use card number 4000 0000 0000 0002

  For date use a valid future date as 12/34<br></br>
  And csv code 3 digits such as 123 &nbsp; (4 digits for american express cards)
  <br/><br/>For more information how to use stripe, please use:  &nbsp; [Stripe testing documentation](https://stripe.com/docs/testing?numbers-or-method-or-token=card-numbers#use-test-cards)
