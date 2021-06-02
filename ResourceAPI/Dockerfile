FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["ResourceAPI/ResourceAPI.csproj", "ResourceAPI/"]
RUN dotnet restore "ResourceAPI/ResourceAPI.csproj"
COPY . .

WORKDIR "/src/ResourceAPI"

RUN dotnet build "ResourceAPI.csproj" -c Release -o /app/build

# FROM build as publish
RUN dotnet publish "ResourceAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runner

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS="http://+:80"
# ENV ASPNETCORE_ENVIRONMENT="Development"
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "ResourceAPI.dll"]
