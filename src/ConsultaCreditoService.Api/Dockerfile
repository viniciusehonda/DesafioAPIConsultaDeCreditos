FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY [".editorconfig", "."]
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/ConsultaCreditoService.Api/ConsultaCreditoService.Api.csproj", "src/ConsultaCreditoService.Api/"]
COPY ["src/ConsultaCreditoService.Application/ConsultaCreditoService.Application.csproj", "src/ConsultaCreditoService.Application/"]
COPY ["src/ConsultaCreditoService.Domain/ConsultaCreditoService.Domain.csproj", "src/ConsultaCreditoService.Domain/"]
COPY ["src/ConsultaCreditoService.Infrastructure/ConsultaCreditoService.Infrastructure.csproj", "src/ConsultaCreditoService.Infrastructure/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
RUN dotnet restore "./src/ConsultaCreditoService.Api/ConsultaCreditoService.Api.csproj"
COPY . .
WORKDIR "/src/src/ConsultaCreditoService.Api"
RUN dotnet build "./ConsultaCreditoService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConsultaCreditoService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsultaCreditoService.Api.dll"]