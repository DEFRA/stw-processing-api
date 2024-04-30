# SWT Processing API

The Processing API is an Azure Function that uses a Service Bus Queue Trigger to process messages placed on the queue by the Submission API.

The function will perform validation, mapping, payload enhancement and routing on the incoming payload.

> ***Important:*** At present, the Azure Function is using a HTTP Trigger which is temporary and will be changed soon.


## Running the Application

You can either run the application directly on your local machine or via Docker.

### Environment Variables

The table below outlines the environment variables required for running the application along with a description.

| Variable Name            | Description                                                                                          |
|--------------------------|------------------------------------------------------------------------------------------------------|
| AzureWebJobsStorage      | Connection string for an Azure Storage account that the Functions runtime uses for normal operations |
| FUNCTIONS_WORKER_RUNTIME | Language or language stack of the worker runtime to load in the function app                         |

### Running via Docker

#### Prerequisites

If not already installed, you will need [Docker Desktop](https://www.docker.com/products/docker-desktop).

#### Steps

1. Open terminal and `cd` into the root of the repository
2. Run `docker-compose build`
3. Run `docker-compose up`

Following the completion of the steps above, the application should now be running on port `5000`.

### Running on Local Machine

#### Prerequisites

If not already installed, you will need:

- .NET 8 Runtime and SDK - [Microsofts .NET 8 Downloads](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Azure Function Core Tools - [Read More](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)

#### Steps

1. Open terminal and `cd` into the `src/STW.ProcessingApi.Function` directory
2. Run `func start`

Following the competion of the steps above, the application should now be running on port `7071`.


## Licence

THIS INFORMATION IS LICENSED UNDER THE CONDITIONS OF THE OPEN GOVERNMENT LICENCE found at:

<http://www.nationalarchives.gov.uk/doc/open-government-licence/version/3>

The following attribution statement MUST be cited in your products and applications when using this information.

> Contains public sector information licensed under the Open Government licence v3

### About the licence

The Open Government Licence (OGL) was developed by the Controller of Her Majesty's Stationery Office (HMSO) to enable information providers in the public sector to license the use and re-use of their information under a common open licence.

It is designed to encourage use and re-use of information freely and flexibly, with only a few conditions.