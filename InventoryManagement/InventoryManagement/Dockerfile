FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /InventoryManagement
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "InventoryManagement.csproj"
# Build and publish a release
RUN dotnet publish "InventoryManagement.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /InventoryManagement/out .
ENTRYPOINT ["dotnet", "InventoryManagement.dll"]
EXPOSE 80