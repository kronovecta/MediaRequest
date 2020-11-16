FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
COPY *.sln ./
COPY MediaRequest/*.csproj ./MediaRequest/
COPY MediaRequest.Application/*.csproj ./MediaRequest.Application/
COPY MediaRequest.Data/*.csproj ./MediaRequest.Data/
COPY MediaRequest.Domain/*.csproj ./MediaRequest.Domain/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY MediaRequest/. ./MediaRequest/
COPY MediaRequest.Application/. ./MediaRequest.Application/
COPY MediaRequest.Data/. ./MediaRequest.Data/
COPY MediaRequest.Domain/. ./MediaRequest.Domain/
#
WORKDIR /app/MediaRequest
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/MediaRequest/out ./
ENTRYPOINT ["dotnet", "MediaRequest.WebUI.dll"]

# FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS build
# WORKDIR /app
# EXPOSE 80
# 
# COPY *.sln ./
# COPY MediaRequest/*.csproj ./MediaRequest/
# COPY MediaRequest.Application/*.csproj ./MediaRequest.Application/
# COPY MediaRequest.Data/*.csproj ./MediaRequest.Data/
# COPY MediaRequest.Domain/*.csproj ./MediaRequest.Domain/
# # RUN dotnet restore
# 
# COPY . ./mediarequest
# WORKDIR /app/mediarequest
# RUN dotnet publish -c Release -o out
# 
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
# WORKDIR /app
# COPY --from=build /app/mediarequest/out ./
# ENTRYPOINT ["dotnet", "MediaRequest.WebUI.dll"]
