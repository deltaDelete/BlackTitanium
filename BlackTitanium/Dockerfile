FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlackTitanium/BlackTitanium.csproj", "BlackTitanium/"]
RUN dotnet restore "BlackTitanium/BlackTitanium.csproj"
COPY . .
WORKDIR "/src/BlackTitanium"
RUN dotnet build "BlackTitanium.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlackTitanium.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlackTitanium.dll"]
