# PasswordApi
The solution contains two main projects that are required to run the application.

## 1. Password.web
An simple MVC application that has two pages for password creation and validation.
### /password/create
Create the password based on the user id.
### /password/validate
Validate the password and user id.

The MVC project will be communicating to the back-end throught he web api application.

## 2. Password.Api
A console application that hosts the web api using owin. Make sure this is running before requesting or validating the Api.
