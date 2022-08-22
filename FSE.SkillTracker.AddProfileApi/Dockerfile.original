#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FSE.SkillTracker.AddProfileApi/FSE.SkillTracker.AddProfileApi.csproj", "FSE.SkillTracker.AddProfileApi/"]
COPY ["FSE.SkillTracker.AddProfileApi.Infrastructure/FSE.SkillTracker.AddProfileApi.Infrastructure.csproj", "FSE.SkillTracker.AddProfileApi.Infrastructure/"]
COPY ["FSE.SkillTracker.AddProfileApi.Application/FSE.SkillTracker.AddProfileApi.Application.csproj", "FSE.SkillTracker.AddProfileApi.Application/"]
COPY ["FSE.SkillTracker.AddProfileApi.Domain/FSE.SkillTracker.AddProfileApi.Domain.csproj", "FSE.SkillTracker.AddProfileApi.Domain/"]
RUN dotnet restore "FSE.SkillTracker.AddProfileApi/FSE.SkillTracker.AddProfileApi.csproj"
COPY . .
WORKDIR "/src/FSE.SkillTracker.AddProfileApi"
RUN dotnet build "FSE.SkillTracker.AddProfileApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FSE.SkillTracker.AddProfileApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FSE.SkillTracker.AddProfileApi.dll"]