FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ../../../../Shared ./Shared
COPY ../PaymentService.Application ./PaymentService.Application
COPY ../PaymentService.Infrastructure ./PaymentService.Infrastructure
COPY . ./PaymentService.API


RUN dotnet restore "./PaymentService.API/PaymentService.API.csproj"


WORKDIR "/src/PaymentService.API"
RUN dotnet build "PaymentService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
RUN dotnet publish "PaymentService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.API.dll"]
