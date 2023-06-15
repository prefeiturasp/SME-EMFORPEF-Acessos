FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SME.Acessos.Api/SME.Acessos.Api.csproj", "SME.Acessos.Api/"]
RUN dotnet restore "SME.Acessos.Api/SME.Acessos.Api.csproj"

COPY . .
WORKDIR "/src/SME.Acessos.Api"
RUN dotnet build "SME.Acessos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SME.Acessos.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV TZ=America/Sao_Paulo
RUN apt-get update \
    && apt-get install -yq tzdata locales -y \
    && dpkg-reconfigure --frontend noninteractive tzdata \
	  && locale-gen en_US.UTF-8
COPY --from=publish /app/publish .

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf

ENTRYPOINT ["dotnet", "SME.Acessos.Api.dll"]
