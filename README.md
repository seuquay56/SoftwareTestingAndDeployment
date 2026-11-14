# Software Engineering Labs 9 and 10

This bundle contains a worked sample solution for the uploaded Lab 9 (Software Testing) and Lab 10 (Software Deployment) instructions. It uses .NET 8 so that the projects can be opened together in Visual Studio 2022 with a recent .NET 8 SDK installed.

> Important: this is a standalone sample because no existing Lab 2 / Lab 7 / Lab 8 website or source project was attached. Replace the sample site with your own course website if your instructor expects the previous assignment project. Cloud deployment, screenshots, and test execution must be completed on your Windows machine/account; this environment does not have the .NET SDK or Docker installed, so no real build/deployment result is claimed here.

## Open in Visual Studio

1. Extract the ZIP file.
2. Open `SELab9_10.sln` in Visual Studio 2022.
3. Install the **.NET 8 SDK** if Visual Studio reports that the target framework is missing.
4. Allow NuGet package restore to finish.
5. Build the solution.

## Lab 9 - Software Testing

### Exercise 1 and 2: BasicMath and CSV data

- `Lab9/BasicMath/BasicMaths.cs` implements Add, Subtract, Multiply and Divide.
- `Lab9/BasicMath.Tests/BasicMathsTests.cs` tests equivalence partitions and boundary values.
- `Lab9/BasicMath.Tests/CsvMathTests.cs` reads four CSV files and runs data-driven tests.

Run:

```powershell
dotnet test .\Lab9\BasicMath.Tests\BasicMath.Tests.csproj
```

### Exercise 3: BankAccount

- `Lab9/BankAccountApp/BankAccount.cs` implements Debit, Credit and Withdraw.
- `Lab9/BankAccount.Tests` contains inline tests and CSV-driven tests, including the insufficient-funds case.

Run:

```powershell
dotnet test .\Lab9\BankAccount.Tests\BankAccount.Tests.csproj
```

### Exercise 4: Selenium login automation

- The local demo login page is `Lab9/LoginAutomation/login.html`.
- Credentials for the valid-login data-driven test are in `TestData/LoginTestData.csv`.
- The tests run Chrome in headless mode and use Selenium Manager to obtain a compatible driver when needed.
- Install Google Chrome and allow Selenium Manager to access the internet on its first run if it needs to download a driver.

Run:

```powershell
dotnet test .\Lab9\LoginAutomation\LoginAutomation.csproj
```

For an Allure report, install the Allure command-line tool, run the tests, then execute `allure serve` from the test output directory where `allure-results` is created. The test class includes the Allure NUnit integration attribute.

## Lab 10 - Software Deployment

### Run locally

```powershell
dotnet run --project .\Lab10\DeploymentDemo\DeploymentDemo.csproj --urls http://localhost:5080
```

Open `http://localhost:5080`. Verify `http://localhost:5080/health` and `http://localhost:5080/api/info`.

### Exercise 1 and 2: Docker on Windows

Install and start Docker Desktop, then from the repository root run:

```powershell
docker build -t selab-deployment-demo:latest -f .\Lab10\DeploymentDemo\Dockerfile .\Lab10\DeploymentDemo
docker run -d -p 8080:8080 --name selab10-deployment-demo selab-deployment-demo:latest
```

Open `http://localhost:8080`. Check the container with `docker ps`; stop/remove it with:

```powershell
docker stop selab10-deployment-demo
docker rm selab10-deployment-demo
```

Or use Docker Compose:

```powershell
cd .\Lab10\DeploymentDemo
Copy-Item .env.example .env
# Edit .env and set your Docker Hub username, if you intend to push.
docker compose up --build -d
docker compose ps
docker compose down
```

### Exercise 1 / 2: Docker Hub

```powershell
docker login
docker tag selab-deployment-demo:latest YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
docker push YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
```

To pull and run on another computer:

```powershell
docker pull YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
docker run -d -p 8080:8080 --name selab10-deployment-demo YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
```

### Exercise 3: Ubuntu server

See `Lab10/DeploymentDemo/DEPLOY_UBUNTU.md`. It includes both `docker save` + `scp` + `docker load` from the handout, and the recommended Docker Hub pull path.

### Exercise 4: GitHub Actions CI/CD

The workflow is `.github/workflows/docker-image.yml`. Push the extracted project to a GitHub repository, then add repository secrets:

- `DOCKER_USERNAME`
- `DOCKER_PASSWORD` (prefer a Docker Hub access token)

Every push / pull request to `main` runs the BasicMath and BankAccount tests and builds the Docker image. A push to `main` also publishes `YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest`.

### Exercise 5 and 6: Azure and AWS

See `Lab10/DeploymentDemo/DEPLOY_AZURE.md` and `DEPLOY_AWS.md`. These are deployment runbooks; they require your own cloud account, registry, permissions, and potentially billable resources.

### Special production deployment

The sample app is container-ready, but the final live deployment URL cannot be generated without access to your hosting account. Deploy it using the Azure or AWS runbook, then add the real public URL and screenshots to the Lab 10 report.

## Submission preparation

- `Reports/Lab9_Report_Draft.docx` and `.pdf`
- `Reports/Lab10_Report_Draft.docx` and `.pdf`
- `Screenshots/Lab9` and `Screenshots/Lab10` are folders for your real screenshots.

The reports deliberately mark the locations where you must insert your own Visual Studio, Test Explorer, Docker Desktop, GitHub Actions and live-site screenshots. Do not submit the drafts until those placeholders are replaced and the actual results / live URL are entered.
