# Bắt đầu từ đây - Lab 9 và Lab 10

## 1. Mở project

1. Giải nén `SELab9_10.zip`.
2. Mở file `SELab9_10.sln` bằng Visual Studio 2022.
3. Cài .NET 8 SDK nếu Visual Studio báo thiếu target framework.
4. Chờ Visual Studio Restore NuGet, sau đó chọn **Build > Build Solution**.

## 2. Chạy Lab 9

Trong Visual Studio, mở **Test > Test Explorer** rồi chọn **Run All**. Có thể chạy từng nhóm bằng Terminal tại thư mục gốc:

```powershell
dotnet test .\Lab9\BasicMath.Tests\BasicMath.Tests.csproj
dotnet test .\Lab9\BankAccount.Tests\BankAccount.Tests.csproj
dotnet test .\Lab9\LoginAutomation\LoginAutomation.csproj
```

- BasicMath: Exercise 1 và Exercise 2.
- BankAccount: Exercise 3.
- LoginAutomation: Exercise 4. Máy cần cài Google Chrome; Selenium Manager có thể cần Internet lần đầu để tải driver phù hợp.

## 3. Chạy Lab 10 trên máy trước

Chạy `Lab10/DeploymentDemo/DeploymentDemo.csproj` bằng Visual Studio (F5), hoặc:

```powershell
dotnet run --project .\Lab10\DeploymentDemo\DeploymentDemo.csproj --urls http://localhost:5080
```

Mở `http://localhost:5080`, rồi kiểm tra thêm `/health` và `/api/info`.

## 4. Chạy Docker trên Windows

Cài và mở Docker Desktop. Tại thư mục gốc project, chạy:

```powershell
docker build -t selab-deployment-demo:latest -f .\Lab10\DeploymentDemo\Dockerfile .\Lab10\DeploymentDemo
docker run -d -p 8080:8080 --name selab10-deployment-demo selab-deployment-demo:latest
```

Mở `http://localhost:8080`. Khi xong, dọn container:

```powershell
docker stop selab10-deployment-demo
docker rm selab10-deployment-demo
```

## 5. Các bước cần tài khoản thật

- Docker Hub: đăng nhập, tag và push image theo `Lab10/DeploymentDemo/DOCKER_HUB.md`.
- GitHub Actions: tạo repository rồi thêm hai secrets `DOCKER_USERNAME`, `DOCKER_PASSWORD` (nên dùng Docker Hub access token).
- Ubuntu: làm theo `DEPLOY_UBUNTU.md`, cần SSH vào server và mở port 8080.
- Azure / AWS: làm theo `DEPLOY_AZURE.md` hoặc `DEPLOY_AWS.md`; kiểm tra chi phí trước khi tạo tài nguyên cloud.

## 6. Hoàn thiện hai báo cáo

Mở hai file Word trong `Reports`. Điền Student ID / Name / Class, chạy thực tế các bài, thay các khung **INSERT REAL SCREENSHOT HERE** bằng ảnh của chính m, điền kết quả test và URL website thật. Sau đó xuất lại PDF. PDF hiện tại chỉ là bản nháp có bookmark, không phải bằng chứng đã test/deploy.

**Lưu ý:** Ở môi trường tạo file này không có .NET SDK và Docker, vì vậy mã nguồn được dựng theo nội dung hai handout nhưng chưa thể được compile, chạy test hoặc deploy thực tế tại đây. Hãy chạy lần đầu trên Visual Studio / Docker Desktop của m và gửi lỗi cụ thể nếu có để sửa tiếp.
