FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Olympo.Api/Olympo.Api.csproj", "src/Olympo.Api/"]
COPY ["src/Olympo.Application/Olympo.Application.csproj", "src/Olympo.Application/"]
COPY ["src/Olympo.Domain/Olympo.Domain.csproj", "src/Olympo.Domain/"]
COPY ["src/Olympo.Infrastructure/Olympo.Infrastructure.csproj", "src/Olympo.Infrastructure/"]
RUN dotnet restore "src/Olympo.Api/Olympo.Api.csproj"
COPY . .
WORKDIR "/src/src/Olympo.Api"
RUN dotnet build "Olympo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Olympo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Olympo.Api.dll"]
