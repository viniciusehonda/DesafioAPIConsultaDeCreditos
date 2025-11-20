FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/ConsultaCreditoService.Worker/ConsultaCreditoService.Worker.csproj", "src/ConsultaCreditoService.Worker/"]
COPY ["src/ConsultaCreditoService.Application/ConsultaCreditoService.Application.csproj", "src/ConsultaCreditoService.Application/"]
COPY ["src/ConsultaCreditoService.Domain/ConsultaCreditoService.Domain.csproj", "src/ConsultaCreditoService.Domain/"]
COPY ["src/ConsultaCreditoService.Infrastructure/ConsultaCreditoService.Infrastructure.csproj", "src/ConsultaCreditoService.Infrastructure/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
RUN dotnet restore "./src/ConsultaCreditoService.Worker/ConsultaCreditoService.Worker.csproj"
COPY . .
WORKDIR "/src/src/ConsultaCreditoService.Worker"
RUN dotnet build "./ConsultaCreditoService.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConsultaCreditoService.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsultaCreditoService.Worker.dll"]