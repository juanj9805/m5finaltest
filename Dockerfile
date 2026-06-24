# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy csproj files and restore — layer cache optimization
COPY Final.Domain/Final.Domain.csproj Final.Domain/
COPY Final.Web/Final.Web.csproj Final.Web/
RUN dotnet restore Final.Web/Final.Web.csproj

# Copy source and publish
COPY . .
RUN dotnet publish Final.Web/Final.Web.csproj -c Release -o /out

# Stage 2: Runtime only
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /out .

# Listen on 8080 (HTTP) inside the container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Final.Web.dll"]
