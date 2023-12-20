# Use the official .NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . ./

# Publish the application in a release configuration
RUN dotnet publish -c Release -o out

# Use the official .NET Core runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port the app runs on
EXPOSE 80

# Define the entry point for the application
ENTRYPOINT ["dotnet", "YourAppName.dll"]
