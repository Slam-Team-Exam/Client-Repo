# ------------------ Build Stage ------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file(s) and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all source files and publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ------------------ Runtime Stage ------------------
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Set the entrypoint to run the console app
ENTRYPOINT ["dotnet", "MicroClient.dll"]
