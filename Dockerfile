# Runtime with .NET 10 ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine

USER root

ARG APP_NAME="identity-service"
ENV APP_NAME=${APP_NAME}

WORKDIR /app

# Copy published output from build stage
COPY ./publish .

# Expose default ASP.NET Core port
EXPOSE 5000

# Run the .NET application
ENTRYPOINT ["./entrypoint.sh"]