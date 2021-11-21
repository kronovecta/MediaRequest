#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MediaRequest/MediaRequest.WebUI.csproj", "MediaRequest/"]
COPY ["MediaRequest.Data/MediaRequest.Data.csproj", "MediaRequest.Data/"]
COPY ["MediaRequest.Application/MediaRequest.Application.csproj", "MediaRequest.Application/"]
COPY ["MediaRequest.Application/MediaRequest.Application.Tests.csproj", "MediaRequest.Application.Tests/"]
COPY ["MediaRequest.Domain/MediaRequest.Domain.csproj", "MediaRequest.Domain/"]
RUN dotnet restore "MediaRequest/MediaRequest.WebUI.csproj"
COPY . .
WORKDIR "/src/MediaRequest"
RUN dotnet build "MediaRequest.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaRequest.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaRequest.WebUI.dll"]