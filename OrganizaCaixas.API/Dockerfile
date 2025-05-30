# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia csproj e restaura dependências
COPY ["OrganizaCaixas.API.csproj", "./"]
RUN dotnet restore "./OrganizaCaixas.API.csproj"

# Copia todo o restante e builda a aplicação
COPY . .
RUN dotnet publish "OrganizaCaixas.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrganizaCaixas.API.dll"]
