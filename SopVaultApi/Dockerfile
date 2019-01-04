FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SopVaultApi/SopVaultApi.csproj", "SopVaultApi/"]
RUN dotnet restore "SopVaultApi/SopVaultApi.csproj"
COPY . .
WORKDIR "/src/SopVaultApi"
RUN dotnet build "SopVaultApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SopVaultApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SopVaultApi.dll"]