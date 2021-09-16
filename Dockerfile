#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Imagem de execucoo
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443


#Copia tudo para pasta do workdir, faz o restore e copia pacotes
COPY . ./
RUN dotnet publish GestaoServicoLogistica.sln -c Release -o /out


# Copy everything else and build
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=build-env /out .


EXPOSE 44365
ENTRYPOINT ["dotnet", "GSL.Cadastro.Api.dll"]

