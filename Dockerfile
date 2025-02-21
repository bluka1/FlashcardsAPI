FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./Flashcards.Api/Flashcards.Api.csproj", "./"]
RUN dotnet restore "./Flashcards.Api.csproj"
COPY . .
RUN dotnet publish "./Flashcards.Api/Flashcards.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Flashcards.Api.dll"]