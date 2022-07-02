#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SocialEventManager.API/SocialEventManager.API.csproj", "src/SocialEventManager.API/"]
COPY ["src/SocialEventManager.Infrastructure/SocialEventManager.Infrastructure.csproj", "src/SocialEventManager.Infrastructure/"]
COPY ["src/SocialEventManager.BLL/SocialEventManager.BLL.csproj", "src/SocialEventManager.BLL/"]
COPY ["src/SocialEventManager.DAL/SocialEventManager.DAL.csproj", "src/SocialEventManager.DAL/"]
RUN dotnet restore "src/SocialEventManager.API/SocialEventManager.API.csproj"
COPY . .
WORKDIR "/src/src/SocialEventManager.API"
RUN dotnet build "SocialEventManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialEventManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialEventManager.API.dll"]