#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SIO.Front.Server/SIO.Front.Server.csproj", "src/SIO.Front.Server/"]
COPY ["src/SIO.Front.Shared/SIO.Front.Shared.csproj", "src/SIO.Front.Shared/"]
COPY ["src/SIO.Front.Client/SIO.Front.Client.csproj", "src/SIO.Front.Client/"]
RUN dotnet restore "src/SIO.Front.Server/SIO.Front.Server.csproj"
COPY . .
WORKDIR "/src/src/SIO.Front.Server"
RUN dotnet build "SIO.Front.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SIO.Front.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SIO.Front.Server.dll"]