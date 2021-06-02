FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["EventAPI/EventAPI.csproj", "EventAPI/"]
RUN dotnet restore "EventAPI/EventAPI.csproj"
COPY . .

WORKDIR "/src/EventAPI"

RUN dotnet build "EventAPI.csproj" -c Release -o /app/build

# FROM build as publish
RUN dotnet publish "EventAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runner

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS="http://+:80"
# ENV ASPNETCORE_ENVIRONMENT="Development"
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "EventAPI.dll"]
