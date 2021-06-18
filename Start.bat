start "API" cmd /c "cd ..\PersonalFinance.API\src\PersonalFinance.API && dotnet lambda-test-tool-3.1"

start "Frontend" cmd /c "cd ..\PersonalFinance.Frontend\src\PersonalFinance.Frontend && dotnet run"