#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
FROM mcr.microsoft.com/dotnet/core/sdk:latest AS base
RUN mkdir -p /WebApiSenseWord/src
COPY . /WebApiSenseWord/src/

#ÐÞ¸ÄÊ±Çø
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /WebApiSenseWord/src/
EXPOSE 8084
ENV ASPNETCORE_URLS http://*:8084

#Build
RUN dotnet restore
RUN dotnet test --filter TestCategory=UnitTest
#RUN dotnet build "WebApiSenseWord.sln" -c Release -o /WebApiSenseWord/src/build
RUN dotnet publish "WebApiSenseWord.sln" -c Release -o /WebApiSenseWord/src/publish

#FROM base AS final
WORKDIR /WebApiSenseWord/src/publish
ENTRYPOINT ["dotnet", "WebApiSenseWord.dll"]