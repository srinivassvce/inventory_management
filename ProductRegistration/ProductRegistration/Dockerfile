FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ProductRegistration
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "ProductRegistration.csproj"
# Build and publish a release
RUN dotnet publish "ProductRegistration.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /ProductRegistration/out .
ENTRYPOINT ["dotnet", "ProductRegistration.dll"]
EXPOSE 80