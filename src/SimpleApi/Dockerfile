#compile
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app
COPY SimpleApi.csproj .
RUN dotnet restore
COPY . .
#pode ser ou release-para prod ou debug. o useapphost esta a false porque vamos correr o dll, se precisarmos do exe não colocavamos isto... acho eu
RUN dotnet publish "./SimpleApi.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore


#final
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SimpleApi.dll"]