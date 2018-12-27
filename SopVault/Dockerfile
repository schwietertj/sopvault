FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SopVault/SopVault.csproj", "SopVault/"]
RUN dotnet restore "SopVault/SopVault.csproj"
COPY . .
WORKDIR "/src/SopVault"
RUN dotnet build "SopVault.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SopVault.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SopVault.dll"]