# ============================
# 1) Build
# ============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY mentor/*.csproj ./mentor/
COPY Mentor.Tests/*.csproj ./Mentor.Tests/

RUN dotnet restore mentor/mentor.csproj

# Copia tudo
COPY mentor/. ./mentor/

RUN dotnet publish mentor/mentor.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

# Copia artefatos publicados
COPY --from=build /app/publish .

# Porta da API
EXPOSE 8080

# URL de execução no container
ENV ASPNETCORE_URLS=http://+:8080

# Executa a aplicação
ENTRYPOINT ["dotnet", "mentor.dll"]
