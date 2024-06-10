#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web.MyShop/Web.MyShop.csproj", "Web.MyShop/"]
COPY ["Data.MyShop/Data.MyShop.csproj", "Data.MyShop/"]
COPY ["Infrastructure.MyShop/Infrastructure.MyShop.csproj", "Infrastructure.MyShop/"]
RUN dotnet restore "Web.MyShop/Web.MyShop.csproj"
COPY . .
WORKDIR "/src/Web.MyShop"
RUN dotnet build "Web.MyShop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.MyShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.MyShop.dll"]