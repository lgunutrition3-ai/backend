FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

# Set environment variables for configuration
ENV ConnectionStrings__DefaultConnection="Server=mysql.railway.internal;Port=3306;Database=railway;User=root;Password=GJLdlVaGKkcXqIgDUHubwYEwEQhvKcKj"
ENV Jwt__Key="fd652ac609768d0a119b7ec9383e8212202271393e254fa6a89ad9c11bb8917568d004d1b519ab497cdf46e599d9ce0eedfa770a2927cd610a6257bc6090af74"
ENV Jwt__Issuer="NutritionAPI"
ENV Jwt__Audience="NutritionClient"

ENTRYPOINT ["dotnet", "Nutrition_backend.dll"]