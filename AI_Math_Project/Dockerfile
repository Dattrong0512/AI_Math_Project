# Chọn image SDK của .NET để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Sao chép tệp .sln và .csproj vào container
COPY *.sln . 
COPY AI_Math_Project/*.csproj ./AI_Math_Project/

# Phục hồi các gói nuget
RUN dotnet restore

# Sao chép toàn bộ mã nguồn từ thư mục hiện tại vào container
COPY AI_Math_Project/. ./AI_Math_Project/

# Chuyển đến thư mục dự án và build ứng dụng
WORKDIR /source/AI_Math_Project
RUN dotnet publish -c Release -o /app --no-restore

# Chạy ứng dụng trong container (stage cuối cùng)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./ 
ENTRYPOINT ["dotnet", "AI_Math_Project.dll"]
