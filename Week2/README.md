# Monday

## General Terms
- Waterfall - Agile\
- Tech-debt\
- Non-functional Requirements\
- DDD

## Implementation
-Base Request\
-Base Response\
-Dapper


# Tuesday

## ASP .NET Core
-Continue to learn ASP .NET Core basics\
-Started to basic API Project\
-Dependicies\
-Database connection\
-Migrations\
-MVC Architecture\


# Wednesday

## Theoretical Information and Implementation
-Signup Login\
-Jwt Token\
-Authenticate\
-Cache-in Memory\
-Logging


# Thursday

## Theoretical Information and Implementation
-Refresh token\
-Salting\
-Swagger and Postman


# Friday

-Code Review\
-Changes in the project according to code review

## Angular
-Small Angular Project\
-Component\
-Input Output\
-Event\
-Directives\
-Services\
-HTTP Request


# General Flow

First week starts with learning ASP .Net Core. This technology will be used as a Web API.I started with learning general syntax and ready files. 
After that I searched Dependency Injection which makes configured services available throughout an app. To understand easily, I implement logging,
database connection, memory cache and authendication. All of these services are be activating with DI. After that I searched Middleware. Middleware
is a request handling pipeline which is composed as a series of middleware components. It can be used with Use{Feature}. Then I looked at MVC Architecture
which is basically Model, View, Controller. Model is the shape of data. To use others components model is like a predefined template to control other 
features. View is the represantion of the user interface. To do that .cshtml extions filed are used. I skipped that because for frontend Angular will be used.
Controller actually is a center. It connects user, model and view. It takes request then handle it and give proper data as response. After understand the controller,
I learned routing. It is important for calling api. It is formed like domain/controller/action/id. After learning general info of project, I need external packages.
To reach them, I learned dependicies and NuGet Package manager. I learned how I can download and use them. After all I started to learn database, first I downloaded
SQL Server to use my localhost as database and SQL Server Management Studio to control my database. Then I downloaded some packages and do some initializations.
To transfer database I did migrations and implement to server all I did. After successfully performing operations on the database, I started by looking authenticate.
Basically to use authenticate, I start with Jwt Token. Also I learned using cache-in memory with my supervisor guidance. Then combine them with after logging store in
cache-in memory. I did small project with all I learned with. Then to follow and debug them I learned logging. After I completed first version of my small project. I
started to learn refresh token. To stay logged in long time refresh token is important and secure. All of these implementation, to make more secure hashed passwords,
I use salting. Then I completed my project to control APIs, I use 2 program. One of them is swagger which is integrated with project. When I launch project it
automatically shows functions and you can request them. The other one is Postman which is more complicated and have many features. I used them to check my requests and
their responses correct or not. Then I started to learn Angular basics. I tried to understand API connection and some terms specific to Angular. Then We did code review
with my supervisor in ASP .Net Core project. After that I made some changes to the code. I made changes to the database. I made a few changes for testing in Postman and 
completed the week.

# Project Flow

To do my project, First I started to connect database to my project. In my model, there were just username and password. I made SignUp function. To create account
successfully I checked same username exist or not. Because I used username as primary key. I stored password with hash. Then I made Login function. First I
checked username and password are correct or not. Then I check is there any token in cache with that username. If exist, I exist expire time. Else I created one.
Both case I give response as a token. In token there is only username info. After all, I searched refresh token and want to use it. To do that I changed database
structure. I added two column: Refresh token and expiry date. Then I changed login function. While creating jwt token also created refresh token and send both
of them as a response. To use refresh token I made refresh function. It gives jwt token and refresh token as a parameter. From token I took username and check
refresh token in database. Also in database refresh token stored in hashed version. If refresh token is not expired, it gives new jwt token and refresh token. After
all I learned salting and wanna make stronger security. So before hashing password and refresh token I used a saltkey and add this to them. In that way hashed ones
are more resistance to brute force. With ping function, checks connection. Finally I created check function, It takes jwt token as a parameter and gives token is expired
or not. Then we did code review with my supervisor. Some variable names changed. Signup takes more parameters like name, surname etc. Database expands according to them.
LoginRequest's response was just tokens. Now tokens + user info. Since I added user active information I did logout function. Then uptaded other functions according to
active check.

### Terms I Learned

-Pragmatic Programmer (Book):\
*Broken Window Theory\
*Stone Soup\
*Boil Frog

-ASP .Net Core:\
*Dependency Injection\
*Middleware\
*Routing\
*Dependicies\
*Models\
*Controllers\
*Database\
*Migrations\
*Logging\
*Swagger

-General:\
*API\
*MVC Architecture\
*Authenticate\
*Jwt Token\
*Cache-in Memory\
*Refresh Token\
*Salting\
*Swagger\
*Postman

-Angular:\
*Component\
*Input Output\
*Event\
*Directives\
*Services\
*HTTP request\

