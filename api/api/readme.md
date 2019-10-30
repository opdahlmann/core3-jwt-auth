- steps:

startup.cs

 services.AddCors();

 - Add a AppSettings.cs file under Helpers.

  var appSettingsSection = Configuration.GetSection("AppSettings");
  services.Configure<AppSettings>(appSettingsSection);

  in configurations:

   app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());



- Add models:

AuthenticateModel
User

- Add UserService

Add Nugets

Microsoft.AspNetCore.Authentication.jwtBearer
System.IdentityModel.Tokens.Jwt


- TokenValidationParameters in startup


Test:
Postman: http://localhost:4000/user/authenticate

body: with json selector

{
    "username": "test@test.com",
    "password": "test"
}





