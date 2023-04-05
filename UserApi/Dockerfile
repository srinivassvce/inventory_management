#Stage 1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY /UserApi/*.csproj ./
RUN dotnet restore 
COPY /UserApi/ ./
RUN dotnet build 
RUN dotnet publish -c Release -o out

#Stage 2
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /src/out ./
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ConnectionStrings:IdentityConnection="Data Source=172.17.0.4;Initial Catalog=IdentityDb;User ID=sa;Password=Password@123"

ENTRYPOINT [ "dotnet", "UserApi.dll" ]
