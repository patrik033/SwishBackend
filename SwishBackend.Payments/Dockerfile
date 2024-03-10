#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80




#copy the solution file and restore dependencies
COPY SwishBackend.sln .
COPY SwishBackend.StoreItems/SwishBackend.StoreItems.csproj ./SwishBackend.StoreItems/
COPY SwishBackend.MassTransitCommons/SwishBackend.MassTransitCommons.csproj ./SwishBackend.MassTransitCommons/
COPY SwishBackend.SignalR/SwishBackend.SignalR.csproj ./SwishBackend.SignalR/
COPY SwishBackend.Payments/SwishBackend.Payments.csproj ./SwishBackend.Payments/
COPY SwishBackend.Orders/SwishBackend.Orders.csproj ./SwishBackend.Orders/
COPY SwishBackend.Identity/SwishBackend.Identity.csproj ./SwishBackend.Identity/
COPY SwishBackend.Gateway/SwishBackend.Gateway.csproj ./SwishBackend.Gateway/
COPY SwishBackend.Email/SwishBackend.Email.csproj ./SwishBackend.Email/
COPY SwishBackend.Carriers/SwishBackend.Carriers.csproj ./SwishBackend.Carriers/
RUN dotnet restore


# copy the rest of the application and publish
COPY . .
WORKDIR /app/SwishBackend.Payments
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SwishBackend.Payments.dll"]