FROM mcr.microsoft.com/dotnet/core/runtime:2.1
WORKDIR /app
COPY ./*.csproj ./
RUN dotnet restore dockerapi.csproj
COPY . ./
RUN dotnet publish dockerapi.csproj -c Release -o out
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY — from=build /app/out .
ENTRYPOINT [“dotnet”, “dockerapi.dll”]`
