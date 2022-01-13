#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs

COPY ["src", "src"]
WORKDIR "/src/src/SIO.Front.Client/wwwroot"
RUN npm install
WORKDIR "/src"
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