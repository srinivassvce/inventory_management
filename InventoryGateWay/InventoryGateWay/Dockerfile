FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /InventoryGateWay
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "InventoryGateWay.csproj"
# Build and publish a release
RUN dotnet publish "InventoryGateWay.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /InventoryGateWay/out .
ENTRYPOINT ["dotnet", "InventoryGateWay.dll"]
EXPOSE 80
