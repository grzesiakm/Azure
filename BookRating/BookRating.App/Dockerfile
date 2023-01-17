FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookRating.App/BookRating.App.csproj", "BookRating.App/"]
RUN dotnet restore "BookRating.App/BookRating.App.csproj"
COPY . .
WORKDIR "/src/BookRating.App"
RUN dotnet build "BookRating.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookRating.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookRating.App.dll"]
