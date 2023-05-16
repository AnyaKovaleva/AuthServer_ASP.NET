FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebAPI_Test/WebAPI_Test.csproj", "WebAPI_Test/"]
RUN dotnet restore "WebAPI_Test/WebAPI_Test.csproj"
COPY . .
WORKDIR "/src/WebAPI_Test"
RUN dotnet build "WebAPI_Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI_Test.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI_Test.dll"]
