# go-sec-edgar-ws

## Prerequisites

Before you begin, ensure you have the following installed:

- Docker
- jq

## Building the Docker Image

To build the Docker image for `go-sec-edgar-ws`, run the following command:

```bash
docker build -t go-sec-edgar-ws .
```

## Running the Docker Container

To run the Docker container, use the following command:

```bash
docker run -p 8001:8001 go-sec-edgar-ws
```

## Using the `/` Root Endpoint

You can access the root endpoint to get a simple "Hello" response. Here is an example curl command:

```bash
curl http://localhost:8001/
```

## Using the `/company-info` Endpoint

You can retrieve company information using the `/company-info` endpoint. Here is an example curl command:

```bash
curl http://localhost:8001/company-info?ticker=AAPL
```

## Using the `/sec-filings` Endpoint

You can retrieve SEC filings data using the `/sec-filings` endpoint. Here is an example curl command:

```bash
curl http://localhost:8001/sec-filings?ticker=AAPL

curl -X GET "http://localhost:8001/sec-filings?ticker=AAPL" -H "Accept: application/json"

```
curl http://localhost:8001/forms?ticker=AAPL

