# ARG REPO=mcr.microsoft.com/dotnet/runtime
# # Installer image
# FROM amd64/buildpack-deps:bullseye-curl AS installer

# RUN aspnetcore_version=6.0.10 \
#     && curl -fSL --output aspnetcore.tar.gz https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/$aspnetcore_version/aspnetcore-runtime-$aspnetcore_version-linux-x64.tar.gz \
#     && aspnetcore_sha512='85fd0e7e8bcf371fe3234b46089382fae7418930daec8c5232347c08ebd44542924eaf628264335473467066512df36c3213949e56f0d0dae4cf387f2c5c6d0c' \
#     && echo "$aspnetcore_sha512  aspnetcore.tar.gz" | sha512sum -c - \
#     && tar -oxzf aspnetcore.tar.gz ./shared/Microsoft.AspNetCore.App \
#     && rm aspnetcore.tar.gz

# # ASP.NET Core image

# FROM $REPO:6.0.10-bullseye-slim-amd64

# ENV ASPNET_VERSION=6.0.10

# WORKDIR /app
# COPY --from=installer ["/shared/Microsoft.AspNetCore.App", "/usr/share/dotnet/shared/Microsoft.AspNetCore.App"]

# WORKDIR /app
# COPY /compilados/publish /app/



# ENTRYPOINT [ "dotnet","Api.dll" ]

##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
##EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["Api.csproj", "."]
#RUN dotnet restore "./Api.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Api.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Api.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/Api/Api.csproj", "/prox/"]
COPY ["/AcccesoDatos/AccesoDatos.csproj", "/prox/"]
COPY ["/Servicios/Servicios.csproj", "/prox/"]
COPY ["Google.Api.Gax.dll","/prox/"]
COPY ["Google.Api.Gax.Rest.dll","/prox/"]
COPY ["Google.Apis.Auth.dll","/prox/"]
COPY ["Google.Apis.Auth.PlatformServices.dll","/prox/"]
COPY ["Google.Apis.Core.dll","/prox/"]
COPY ["Google.Apis.Storage.v1.dll","/prox/"]
COPY ["Google.Cloud.Storage.V1.dll","/prox/"]
COPY ["Google.Protobuf.dll","/prox/"]
RUN dotnet restore "/prox/Api.csproj"
COPY . .
WORKDIR "/src/prox"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]