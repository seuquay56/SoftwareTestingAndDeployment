# Docker Hub workflow

Build from repository root:

```powershell
docker build -t selab-deployment-demo:latest -f .\Lab10\DeploymentDemo\Dockerfile .\Lab10\DeploymentDemo
```

Log in and publish:

```powershell
docker login
docker tag selab-deployment-demo:latest YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
docker push YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
```

On a second computer:

```powershell
docker pull YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
docker run -d -p 8080:8080 --name selab10-deployment-demo YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
```

Open `http://localhost:8080`. Use `docker ps`, `docker stop` and `docker rm` to manage the container.
