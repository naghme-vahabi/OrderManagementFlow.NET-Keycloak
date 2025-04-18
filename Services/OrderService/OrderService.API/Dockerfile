FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ../../../../Shared ./Shared
COPY ../OrderService.Application ./OrderService.Application
COPY ../OrderService.Infrastructure ./OrderService.Infrastructure
COPY ../OrderService.Domain ./OrderService.Domain
COPY ../../CustomerService/CustomerService.Domain ./CustomerService.Domain
COPY . ./OrderService.API


RUN dotnet restore "./OrderService.API/OrderService.API.csproj"


WORKDIR "/src/OrderService.API"
RUN dotnet build "OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
RUN dotnet publish "OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.API.dll"]
