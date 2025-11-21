# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files for each project and restore
COPY ["src/ECMS.Application/ECMS.Application.csproj", "ECMS.Application/"]
COPY ["src/ECMS.Core/ECMS.Core.csproj", "ECMS.Core/"]
COPY ["src/ECMS.EntityFrameworkCore/ECMS.EntityFrameworkCore.csproj", "ECMS.EntityFrameworkCore/"]
COPY ["src/ECMS.Migrator/ECMS.Migrator.csproj", "ECMS.Migrator/"]
COPY ["src/ECMS.Web.Core/ECMS.Web.Core.csproj", "ECMS.Web.Core/"]
COPY ["src/ECMS.Web.Host/ECMS.Web.Host.csproj", "ECMS.Web.Host/"]

RUN dotnet restore "ECMS.Web.Host/ECMS.Web.Host.csproj"

# Copy the rest of the code and publish
COPY . .
WORKDIR "/src/ECMS.Web.Host"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECMS.Web.Host.dll"]