# Exercise 6 - Deploy to Amazon Web Services

The workflow follows the lab handout: publish the image to Amazon ECR and deploy it with Elastic Beanstalk. AWS resources may incur charges.

1. Create/select an AWS account and choose a Region.
2. Create an ECR repository named `selab-deployment-demo`.
3. Configure AWS CLI credentials securely (prefer an IAM user/role with least privilege):

```powershell
aws configure
```

4. Build, authenticate, tag and push the image. Replace the account ID and region:

```powershell
docker build -t selab-deployment-demo:latest -f .\Lab10\DeploymentDemo\Dockerfile .\Lab10\DeploymentDemo
aws ecr get-login-password --region YOUR_REGION | docker login --username AWS --password-stdin YOUR_AWS_ACCOUNT_ID.dkr.ecr.YOUR_REGION.amazonaws.com
docker tag selab-deployment-demo:latest YOUR_AWS_ACCOUNT_ID.dkr.ecr.YOUR_REGION.amazonaws.com/selab-deployment-demo:latest
docker push YOUR_AWS_ACCOUNT_ID.dkr.ecr.YOUR_REGION.amazonaws.com/selab-deployment-demo:latest
```

5. Update `Dockerrun.aws.json` with the exact ECR image URI. Create a ZIP containing `Dockerrun.aws.json` and upload it to a new Elastic Beanstalk Web server environment using the Docker platform.
6. Configure the environment's instance role so it can pull from ECR. Allow inbound application traffic through the environment security group / load balancer.
7. Wait until the environment is healthy, open its public URL, and verify `/health` and `/api/info`. Capture ECR repository, Elastic Beanstalk environment health, and the live page for the report.
