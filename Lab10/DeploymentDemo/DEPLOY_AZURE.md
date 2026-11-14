# Exercise 5 - Deploy to Microsoft Azure

This project publishes its web application on container port **8080**. Use your own subscription and resource names; Azure resources may incur charges.

1. Sign in to the Azure portal and create/select a Resource Group.
2. Create an Azure Container Registry (ACR). Record its registry name and login server, for example `myregistry.azurecr.io`.
3. On your development machine, sign in with Azure CLI and authenticate Docker:

```powershell
az login
az acr login --name YOUR_ACR_NAME
```

4. Tag and push the locally built image:

```powershell
docker build -t selab-deployment-demo:latest -f .\Lab10\DeploymentDemo\Dockerfile .\Lab10\DeploymentDemo
docker tag selab-deployment-demo:latest YOUR_ACR_NAME.azurecr.io/selab-deployment-demo:latest
docker push YOUR_ACR_NAME.azurecr.io/selab-deployment-demo:latest
```

5. In Azure Portal, create an **App Service** using a Linux plan and a container-based deployment. Select the ACR image `selab-deployment-demo:latest` and configure registry authentication / managed identity.
6. Set the App Service application setting `WEBSITES_PORT` to `8080`, save, and restart the app.
7. Open the App Service default domain. Verify `/`, `/health` and `/api/info`; capture the portal deployment configuration and the live page for the report.

If an App Service plan or registry SKU is not free in your subscription, check the estimated cost before creating it. Do not publish passwords or registry credentials in GitHub source files.
