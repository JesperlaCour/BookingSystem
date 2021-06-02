FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["DelPinBookingV2/DelPinBookingV2.csproj", "DelPinBookingV2/"]
RUN dotnet restore "DelPinBookingV2/DelPinBookingV2.csproj"
COPY . .

WORKDIR "/src/DelPinBookingV2"

RUN dotnet build "DelPinBookingV2.csproj" -c Release -o /app/build

# FROM build as publish
RUN dotnet publish "DelPinBookingV2.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runner

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS="http://+:80"
# ENV ASPNETCORE_ENVIRONMENT="Development"
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "DelPinBookingV2.dll"]
