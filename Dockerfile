# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Sao chép file .sln và tất cả các file .csproj để tận dụng layer caching
COPY *.sln .
COPY AIMathProject.API/*.csproj ./AIMathProject.API/
COPY AIMathProject.Application/*.csproj ./AIMathProject.Application/
COPY AIMathProject.Domain/*.csproj ./AIMathProject.Domain/
COPY AIMathProject.Infrastructure/*.csproj ./AIMathProject.Infrastructure/

# Phục hồi các gói NuGet
RUN dotnet restore

# Sao chép toàn bộ mã nguồn (đã bỏ qua bin và obj thông qua .dockerignore)
COPY AIMathProject.API/. ./AIMathProject.API/
COPY AIMathProject.Application/. ./AIMathProject.Application/
COPY AIMathProject.Domain/. ./AIMathProject.Domain/
COPY AIMathProject.Infrastructure/. ./AIMathProject.Infrastructure/

# Build và publish dự án API
WORKDIR /source/AIMathProject.API
RUN dotnet publish -c Release -o /app --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Cấu hình port
EXPOSE 80

# Khởi chạy ứng dụng
ENTRYPOINT ["dotnet", "AIMathProject.API.dll"]