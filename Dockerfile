FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/ToDoApp.csproj", "src/"]
RUN dotnet restore "./src/ToDoApp.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "./ToDoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./ToDoApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["./src/Data/ToDoAppDb.db", "./Data/ToDoAppDb.db"]
# RUN chmod 666 "./Data/ToDoAppDb.db"
ENTRYPOINT ["dotnet", "ToDoApp.dll"]