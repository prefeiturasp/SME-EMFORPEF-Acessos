# Estágio Base
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV TZ=America/Sao_Paulo
ENV LANG=pt_BR.UTF-8
ENV LANGUAGE=pt_BR:pt
ENV LC_ALL=pt_BR.UTF-8

# Instalação de dependências e configuração de SSL Legado (se necessário)
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       tzdata \
       locales \
    && sed -i -e 's/# pt_BR.UTF-8 UTF-8/pt_BR.UTF-8 UTF-8/' /etc/locale.gen \
    && locale-gen \
    && dpkg-reconfigure --frontend noninteractive tzdata \
    # Configuração OpenSSL Legado (Mesma lógica do seu script, mas otimizada)
    && sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=0/g' /etc/ssl/openssl.cnf \
    && sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1.0/g' /etc/ssl/openssl.cnf \
    && rm -rf /var/lib/apt/lists/*

USER app

# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# --- CORREÇÃO PRINCIPAL AQUI ---
# O destino deve incluir 'src/' para casar com o comando de restore
COPY ["src/SME.Acessos.Api/SME.Acessos.Api.csproj", "src/SME.Acessos.Api/"]

# O restore agora encontrará o caminho correto: /src/src/SME.Acessos.Api/...
RUN dotnet restore "src/SME.Acessos.Api/SME.Acessos.Api.csproj"

COPY . .
WORKDIR "/src/src/SME.Acessos.Api"
RUN dotnet build "SME.Acessos.Api.csproj" -c Release -o /app/build

# Estágio de Publish
FROM build AS publish
RUN dotnet publish "SME.Acessos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SME.Acessos.Api.dll"]