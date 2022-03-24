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

# Endpoints

## [GET] GetUsers
```C#
[HttpGet("/api/Users",Name = "GetUsers")]
[ProducesResponseType(typeof(Response<IEnumerable<DatumDto>>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetUsers([FromQuery] PaginationFilter filter)
{
  var validFilter = new PaginationFilter(filter.PageNumber, 5);
  var response = await _userService.GetUsers(validFilter);
  return Ok(response);
}
```
Brings paging of 5 users you just have to send the page number to call

## [GET] GetUsersById
```C#
[Authorize]
[HttpGet("/api/Users/{id}", Name = "GetUsersById")]
[ProducesResponseType(typeof(Response<DatumDto>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetUsersById(int id)
{
  var response = await _userService.GetUserById(id);
  return Ok(response);
}
```
Brings the user identified by the Id that is sent (requires to be authenticated)

## [POST] CreateUser

```C#
[HttpPost("/api/Users", Name = "CreateUser")]
[ProducesResponseType(typeof(Response<DatumDto>), StatusCodes.Status200OK)]
public async Task<IActionResult> CreateUser(DatumDto UserData)
{
  if (ModelState.IsValid)
    {
      var response = await _userService.CreateUser(UserData);
      return Ok(response);
    }
    else
    {
      return BadRequest();
    }
}
```
Create the user indicated by the model

## [PATCH] UpdateUser
```C#
[HttpPatch("/api/Users/{id}", Name = "UpdateUser")]
[ProducesResponseType(typeof(Response<DatumDto>), StatusCodes.Status200OK)]
public async Task<IActionResult> UpdateUser(DatumDto UserData, int id)
{
  if (ModelState.IsValid)
  {
    UserData.id = id;
    var response = await _userService.UpdateUser(UserData);
    return Ok(response);
  }
  else
  {
    return BadRequest();
  }
}
```
Update the user by sending the ID and a body with the new features

## [GET] GenerateCredentials
```C#
[HttpGet("api/GenerateCredentials/{id}")]
[ProducesResponseType(typeof(Response<DatumLoginDto>), StatusCodes.Status200OK)]
public async Task<IActionResult> GenerateCredentials(int id)
{
  return Ok(await _userService.GenerateCredentials(id));
}
```
Generates credentials for the user (Id) that is sent to it, returns a username and a password


## [POST] Authenticate
```C#
[HttpPost("api/Authenticate")]
[ProducesResponseType(typeof(Response<AuthenticateResponseDto>), StatusCodes.Status200OK)]
public async Task<IActionResult> Authenticate(AuthenticateRequestDto model)
{
  var response = await _userService.Authenticate(model);
  if (response.Data == null)
    return BadRequest(new { message = "Username or password is incorrect" });
  return Ok(response);
        }
```
Generate a token by sending the previously generated credentials
