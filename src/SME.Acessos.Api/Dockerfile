# ==========================================
# Estágio Base: Runtime e Configurações de OS
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Definições de Localização e Timezone
ENV TZ=America/Sao_Paulo
ENV LANG=pt_BR.UTF-8
ENV LANGUAGE=pt_BR:pt
ENV LC_ALL=pt_BR.UTF-8

# Instalação de pacotes e Correção Crítica do OpenSSL (SQL Server Legacy)
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        tzdata \
        locales \
    # Configuração de Locale
    && sed -i -e 's/# pt_BR.UTF-8 UTF-8/pt_BR.UTF-8 UTF-8/' /etc/locale.gen \
    && locale-gen \
    && dpkg-reconfigure --frontend noninteractive tzdata \
    # -------------------------------------------------------------------------
    # 1. Altera a referência de configuração padrão
    && sed -i 's/ssl_conf = ssl_sect/ssl_conf = ssl_configuration/g' /etc/ssl/openssl.cnf \
    # 2. Injeta as configurações permissivas (SECLEVEL=0 e UnsafeLegacyRenegotiation)
    && { \
        echo ''; \
        echo '[ssl_configuration]'; \
        echo 'system_default = tls_system_default'; \
        echo ''; \
        echo '[tls_system_default]'; \
        echo 'CipherString = DEFAULT@SECLEVEL=0'; \
        echo 'Options = UnsafeLegacyRenegotiation'; \
    } >> /etc/ssl/openssl.cnf \
    # -------------------------------------------------------------------------
    && rm -rf /var/lib/apt/lists/*

# Retorna para o usuário não-privilegiado (Segurança)
USER app

# ==========================================
# Estágio de Build: SDK
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia apenas o csproj respeitando a estrutura de pastas "src/"
COPY ["src/SME.Acessos.Api/SME.Acessos.Api.csproj", "src/SME.Acessos.Api/"]

# Restaura as dependências
RUN dotnet restore "src/SME.Acessos.Api/SME.Acessos.Api.csproj"

# Copia todo o restante do código fonte
COPY . .

# Compilação
WORKDIR "/src/src/SME.Acessos.Api"
RUN dotnet build "SME.Acessos.Api.csproj" -c Release -o /app/build

# ==========================================
# Estágio de Publicação
# ==========================================
FROM build AS publish
RUN dotnet publish "SME.Acessos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# Estágio Final: Imagem de Produção
# ==========================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SME.Acessos.Api.dll"]