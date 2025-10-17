FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG PUBLISH_MODE=Release
ARG TARGET_BUILD_DIR

WORKDIR /src
#RUN ls -R /src/
COPY ./SourceCode/negin.identityserver.api/*.csproj ./*.sln  /src
RUN dotnet restore ./IdentityServer.Api.csproj -v n

COPY ./SourceCode   /src
#WORKDIR /src
#RUN ls -l ./Negin.IdentityServer.Api/
#RUN dotnet restore ./negin.identityserver.api/IdentityServer.Api.csproj -v n



WORKDIR /src/negin.identityserver.api

RUN dotnet publish -c ${PUBLISH_MODE}  --output /publish 


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

ENV ASPNETCORE_URLS=http://+:9001
ENV TZ=Asia/Tehran

WORKDIR /app

COPY --from=build-env /publish .


ENTRYPOINT ["dotnet", "SourceCode/Library/Nsp.Common.dll"]

EXPOSE 57655
EXPOSE 9004
EXPOSE 5672
EXPOSE 8127
EXPOSE 7585
EXPOSE 9003
EXPOSE 1010