FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.config .
COPY *.csproj .
RUN dotnet restore

# copy and build app
COPY . .
RUN dotnet publish -c release -o out

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-buster-slim AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "led-blink.dll"]
