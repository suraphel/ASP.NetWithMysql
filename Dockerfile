#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["mysqltest/mysqltest.csproj", "mysqltest/"]
RUN dotnet restore "mysqltest/mysqltest.csproj"
COPY . .
WORKDIR "/src/mysqltest"
RUN dotnet build "mysqltest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mysqltest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mysqltest.dll"]