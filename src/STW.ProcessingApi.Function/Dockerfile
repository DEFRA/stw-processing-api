FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS installer-env

COPY . /src/STW.processingApi.Function
RUN cd /src/STW.processingApi.Function && \
mkdir -p /home/site/wwwroot && \
dotnet publish STW.ProcessingApi.Function/STW.ProcessingApi.Function.csproj --output /home/site/wwwroot

# To enable ssh & remote debugging on app service change the base image to the one below
# FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0-appservice
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]