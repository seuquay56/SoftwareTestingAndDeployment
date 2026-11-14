# Exercise 3 - Deploy the Docker container to Ubuntu

Replace the placeholders with your own username, server IP and image name.

## Option A - Copy an image archive (the method shown in the lab handout)

On the Windows development machine:

```powershell
docker build -t selab-deployment-demo:latest -f .\Lab10\DeploymentDemo\Dockerfile .\Lab10\DeploymentDemo
docker save -o selab-deployment-demo.tar selab-deployment-demo:latest
scp .\selab-deployment-demo.tar username@YOUR_UBUNTU_SERVER_IP:/home/username/
```

On Ubuntu, connect through SSH and run:

```bash
sudo apt-get update
sudo apt-get install -y docker.io
sudo systemctl enable --now docker
docker --version
sudo docker load -i /home/username/selab-deployment-demo.tar
sudo docker run -d --restart unless-stopped -p 8080:8080 --name selab10-deployment-demo selab-deployment-demo:latest
sudo docker ps
```

Open `http://YOUR_UBUNTU_SERVER_IP:8080` and `http://YOUR_UBUNTU_SERVER_IP:8080/health`. Make sure the server firewall / cloud security group allows inbound TCP port 8080.

## Option B - Pull from Docker Hub

After pushing the image to Docker Hub, on Ubuntu run:

```bash
sudo docker login
sudo docker pull YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
sudo docker run -d --restart unless-stopped -p 8080:8080 --name selab10-deployment-demo YOUR_DOCKERHUB_USERNAME/selab-deployment-demo:latest
```

Manage it with `sudo docker ps`, `sudo docker logs selab10-deployment-demo`, `sudo docker stop selab10-deployment-demo`, and `sudo docker rm selab10-deployment-demo`.

## Docker Compose

Copy the project folder to the server, create `.env` from `.env.example`, set `DOCKERHUB_USERNAME`, then run:

```bash
sudo docker compose up -d
sudo docker compose ps
```
