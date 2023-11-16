#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["JobTest/JobTest.csproj", "JobTest/"]
RUN dotnet restore "JobTest/JobTest.csproj"
COPY . .
WORKDIR "/src/JobTest"
RUN dotnet build "JobTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobTest.dll"]