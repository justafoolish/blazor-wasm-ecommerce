#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BlazorAppEC/Server/BlazorAppEC.Server.csproj", "BlazorAppEC/Server/"]
COPY ["BlazorAppEC/Client/BlazorAppEC.Client.csproj", "BlazorAppEC/Client/"]
COPY ["BlazorAppEC/Shared/BlazorAppEC.Shared.csproj", "BlazorAppEC/Shared/"]
RUN dotnet restore "BlazorAppEC/Server/BlazorAppEC.Server.csproj"
COPY . .
WORKDIR "/src/BlazorAppEC/Server"
RUN dotnet build "BlazorAppEC.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorAppEC.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorAppEC.Server.dll"]
