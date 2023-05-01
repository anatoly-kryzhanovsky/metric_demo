FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Metrics.App/Metrics.App.csproj", "Metrics.App/"]
RUN dotnet restore "Metrics.App/Metrics.App.csproj"
COPY . .
WORKDIR "/src/Metrics.App"
RUN dotnet build "Metrics.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metrics.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metrics.App.dll"]
