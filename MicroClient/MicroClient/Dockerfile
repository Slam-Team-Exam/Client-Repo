# Base image for running the app
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the csproj first (for better caching)
COPY ["MicroClient.csproj", "./"]
RUN dotnet restore "MicroClient.csproj"

# Copy the rest of the code
COPY . .

# Build the project
RUN dotnet build "MicroClient.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MicroClient.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app

# Copy published output from publish stage
COPY --from=publish /app/publish .

# Set the entrypoint to run the console app
ENTRYPOINT ["dotnet", "MicroClient.dll"]
