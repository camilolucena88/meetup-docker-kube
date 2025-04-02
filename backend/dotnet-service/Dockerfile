FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 5288

# Copy csproj and restore first
COPY customer-service/customer-service/customer-service.csproj ./customer-service/
RUN dotnet restore ./customer-service/customer-service.csproj

COPY customer-service/ ./customer-service/
WORKDIR /src/customer-service
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "customer-service.dll"]
