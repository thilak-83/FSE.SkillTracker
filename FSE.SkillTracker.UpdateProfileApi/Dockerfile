#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FSE.SkillTracker.UpdateProfileApi/FSE.SkillTracker.UpdateProfileApi.csproj", "FSE.SkillTracker.UpdateProfileApi/"]
COPY ["FSE.SkillTracker.Infrastructure/FSE.SkillTracker.Infrastructure.csproj", "FSE.SkillTracker.Infrastructure/"]
COPY ["FSE.SkillTracker.Domain/FSE.SkillTracker.Domain.csproj", "FSE.SkillTracker.Domain/"]
COPY ["FSE.SkillTracker.Application/FSE.SkillTracker.Application.csproj", "FSE.SkillTracker.Application/"]
RUN dotnet restore "FSE.SkillTracker.UpdateProfileApi/FSE.SkillTracker.UpdateProfileApi.csproj"
COPY . .
WORKDIR "/src/FSE.SkillTracker.UpdateProfileApi"
RUN dotnet build "FSE.SkillTracker.UpdateProfileApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FSE.SkillTracker.UpdateProfileApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FSE.SkillTracker.UpdateProfileApi.dll"]