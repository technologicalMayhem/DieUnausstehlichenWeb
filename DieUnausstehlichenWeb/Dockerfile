FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DieUnausstehlichenWeb/DieUnausstehlichenWeb.csproj", "DieUnausstehlichenWeb/"]
RUN dotnet restore "DieUnausstehlichenWeb/DieUnausstehlichenWeb.csproj"
COPY . .
WORKDIR "/src/DieUnausstehlichenWeb"
RUN dotnet build "DieUnausstehlichenWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DieUnausstehlichenWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DieUnausstehlichenWeb.dll"]
