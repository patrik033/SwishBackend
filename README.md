# SwishBackend

![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
 
## Table Of Contents
* [General Info](#general-info)
* [Setup](#setup)


## General Info
This Project is...


## Setup
#### Sql Server:

Make sure you've Sql server installed on your platform. It can be found here: [Microsoft Sql Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
A nice addition would be SQL Server Management Studio(SSMS), it can be found here: [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)


#### Visual Studio:

To run the project you need an IDE, like Visual Studio Community: [Visual Studio](https://visualstudio.microsoft.com/vs/community/)

#### Update The Database:

To update the database simply use the Package Manager Console that comes integrated with visual studio community and simply write "Update-Database"
Make sure you are in the correct project when doing so. As of now only Identity and StoreItems offers this.

#### Make sure RabbitMQ is running:
Either: run this command in the terminal to start rabbitMQ:  docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
Or: navigate to the docker-compose.yml file in terminal and write "docker compose up" to start rabbit mq while docker is running.


