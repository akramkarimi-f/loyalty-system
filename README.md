# loyalty-system

# Run the Project
1. Clone the repository
2. Open the Solution file in Visual Studio
3. Change the profile to https
4. Run Redis
5. Run the Solution
6. Change the URL address to https://localhost:7180/swagger
7. Authorize in the Swagger by the email: testuser@loyalty.com and password: Test@123
8. Run the API by ID: 1


# Build docker-compose
1. Switch the ConnectionStrings section in appsettings.json file.
2. Run the command in the terminal:
   docker-compose up --build
