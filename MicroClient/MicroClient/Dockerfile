# Base image til at køre app'en
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopiér kun csproj først (for bedre caching)
COPY ["MicroClient.csproj", "./"]
RUN dotnet restore "MicroClient.csproj"

# Kopiér resten af koden
COPY . .

# Byg projektet
RUN dotnet build "MicroClient.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MicroClient.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroClient.dll"]