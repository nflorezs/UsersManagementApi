# UserManagementApi

## Requeriments

#### Net Core runtime 6.0
#### Visual Studio 2022
#### SQL Server 2018


## Installation

Clone the repository to any source in your computer

Download Database Image from [here](https://drive.google.com/file/d/18VG9k5llfQnP0WDAvmVzUDVhgCVUs7VI/view?usp=sharing)

Install database image in SQLServer 2018

![image](https://user-images.githubusercontent.com/100897465/159998903-f21b908f-bc4b-428b-912a-2e69208ad6c5.png)




```bash
dotnet build
dotnet run
```

or In VStudio run the project

![image](https://user-images.githubusercontent.com/100897465/159999164-b48775cf-3b52-443d-81a4-7ec41ebbe132.png)



## Usage

When the project is running SwaggerUI will open and let you make request to the API

![image](https://user-images.githubusercontent.com/100897465/159999309-64726d52-7104-4767-8609-bf412cbee77f.png)



Background process will run and bring data from external API


You can see metrics HangFire job in /Dashboard

![image](https://user-images.githubusercontent.com/100897465/160000024-3a395d1c-817a-4ef1-bd8d-62b0fb0308a3.png)

