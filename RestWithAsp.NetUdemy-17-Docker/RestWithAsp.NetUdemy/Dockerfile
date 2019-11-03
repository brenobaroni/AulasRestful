FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["RestWithAsp.NetUdemy/RestWithAspNetUdemy.csproj", "RestWithAsp.NetUdemy/"]
RUN dotnet restore "RestWithAsp.NetUdemy/RestWithAspNetUdemy.csproj"
COPY . .
WORKDIR "/src/RestWithAsp.NetUdemy"
RUN dotnet build "RestWithAspNetUdemy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestWithAspNetUdemy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWithAspNetUdemy.dll"]