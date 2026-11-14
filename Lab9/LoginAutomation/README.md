# Selenium login automation

The demo uses a local HTML file so no web server or database is required for this lab exercise. The valid credential rows are in `TestData/LoginTestData.csv`; the HTML page checks the same three demo accounts. The test suite also contains an invalid-credential case.

Run with Chrome installed:

```powershell
dotnet test .\Lab9\LoginAutomation\LoginAutomation.csproj
```

The test class uses the `[AllureNUnit]` adapter and suite/feature/story labels. The `allureConfig.json` file sets the result folder to `allure-results`. After running tests, use `allure serve` from the test assembly's output directory (or set `allure.directory` to a path you prefer).
