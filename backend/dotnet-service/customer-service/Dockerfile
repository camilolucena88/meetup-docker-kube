FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /src
EXPOSE 5288

# Copy csproj and restore first
COPY customer-service/customer-service.csproj ./customer-service/
RUN dotnet restore ./customer-service/customer-service.csproj

# Copy everything else
COPY customer-service/ ./customer-service/
WORKDIR /src/customer-service
RUN dotnet publish -c Release -o /app/publish

# Use ASP.NET base image to run
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "customer-service.dll"]
