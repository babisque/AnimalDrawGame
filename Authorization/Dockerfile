# Utiliza a imagem oficial do .NET SDK para compilar e construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY . ./
RUN dotnet restore

# Compila a aplicação
RUN dotnet publish -c Release -o out

# Instala as ferramentas do EF Core CLI
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Utiliza a imagem oficial do .NET Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exponha a porta que a aplicação irá rodar
EXPOSE 80
EXPOSE 443

# Define a entrada padrão para rodar a aplicação
ENTRYPOINT ["dotnet", "Authorization.API.dll"]