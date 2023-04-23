# Employee Vacation Manager Web API
This is a simple .NET Core Web API project that allows employees to manage their vacation balance and make vacation requests. The project is built using Entity Framework and PostgreSQL.

## Features
- CRUD operations for employees and their vacation balance.
- Vacation request creation, approval, and rejection.

## Getting Started
To get started with this project, follow these steps:

## Prerequisites
- .NET Core 5.0
- PostgreSQL 13.4

## Installing
1. Clone the repository to your local machine.
```
git clone https://github.com/seifhassan89/employee-vacation-manager.git
```
2. Navigate to the project directory.
```
cd employee-vacation-manager
```
3. Restore the project dependencies.
```
dotnet restore
```
4. Update the database schema.
```
dotnet ef database update
```
5. Run the application.
```
dotnet run
```
## Usage
The API endpoints can be tested using a tool such as Postman. The following endpoints are available:

![image](https://user-images.githubusercontent.com/64795421/225893643-df7ff5b1-feb0-428c-806e-e6a5c34fd126.png)
![image](https://user-images.githubusercontent.com/64795421/225893674-79013b2e-00b9-4e21-9a26-34e2622e58e9.png)
![image](https://user-images.githubusercontent.com/64795421/225893745-da1f56c6-d6f3-4a96-8d8d-c2a142ebedf2.png)
![image](https://user-images.githubusercontent.com/64795421/225893816-d81ed2b1-60fb-4912-8b91-6f0110da54fe.png)

## Contributing
Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new feature branch.
3. Make your changes.
4. Create a pull request.

## License
This project is licensed under the MIT License - see the LICENSE file for details.
