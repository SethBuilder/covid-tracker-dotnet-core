FROM ubuntu:18.04
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
ENV container docker
RUN apt-get update
RUN apt-get install -y snapd squashfuse
RUN snap alias dotnet-sdk.dotnet dotnet
RUN dotnet restore
RUN apt-get update
RUN apt-get -y install curl gnupg
RUN curl -sL https://deb.nodesource.com/setup_11.x  | bash -
RUN apt-get -y install nodejs
# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "dg.dll"]