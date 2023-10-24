FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/ECMS.Web.Host/ECMS.Web.Host.csproj", "src/ECMS.Web.Host/"]
COPY ["src/ECMS.Web.Core/ECMS.Web.Core.csproj", "src/ECMS.Web.Core/"]
COPY ["src/ECMS.Application/ECMS.Application.csproj", "src/ECMS.Application/"]
COPY ["src/ECMS.Core/ECMS.Core.csproj", "src/ECMS.Core/"]
COPY ["src/ECMS.EntityFrameworkCore/ECMS.EntityFrameworkCore.csproj", "src/ECMS.EntityFrameworkCore/"]
WORKDIR "/src/src/ECMS.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ECMS.Web.Host", "src/ECMS.Web.Host"]
COPY ["src/ECMS.Web.Core", "src/ECMS.Web.Core"]
COPY ["src/ECMS.Application", "src/ECMS.Application"]
COPY ["src/ECMS.Core", "src/ECMS.Core"]
COPY ["src/ECMS.EntityFrameworkCore", "src/ECMS.EntityFrameworkCore"]
WORKDIR "/src/src/ECMS.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ECMS.Web.Host.dll"]
