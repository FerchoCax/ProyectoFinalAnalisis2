ARG REPO=mcr.microsoft.com/dotnet/runtime
# Installer image
FROM amd64/buildpack-deps:bullseye-curl AS installer

RUN aspnetcore_version=6.0.10 \
    && curl -fSL --output aspnetcore.tar.gz https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/$aspnetcore_version/aspnetcore-runtime-$aspnetcore_version-linux-x64.tar.gz \
    && aspnetcore_sha512='85fd0e7e8bcf371fe3234b46089382fae7418930daec8c5232347c08ebd44542924eaf628264335473467066512df36c3213949e56f0d0dae4cf387f2c5c6d0c' \
    && echo "$aspnetcore_sha512  aspnetcore.tar.gz" | sha512sum -c - \
    && tar -oxzf aspnetcore.tar.gz ./shared/Microsoft.AspNetCore.App \
    && rm aspnetcore.tar.gz


# ASP.NET Core image


FROM $REPO:6.0.10-bullseye-slim-amd64

ENV ASPNET_VERSION=6.0.10

WORKDIR /app
COPY --from=installer ["/shared/Microsoft.AspNetCore.App", "/usr/share/dotnet/shared/Microsoft.AspNetCore.App"]

WORKDIR /app
COPY /compilados/publish /app/



ENTRYPOINT [ "dotnet","Api.dll" ]