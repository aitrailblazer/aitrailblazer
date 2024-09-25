#!/usr/bin/env bash

#rm -rf src

kiota generate --openapi https://aka.ms/graph/v1.0/openapi.yaml --include-path /me/todo/lists --language CSharp --class-name TaskClient --namespace-name MyTaskApp.Client --output ./src/MyTaskApp/Client

kiota generate --openapi https://aka.ms/graph/v1.0/openapi.yaml --include-path /me/drive --exclude-backward-compatible --language CSharp --output ./src/MyDriveApp/

kiota search messages

kiota generate --openapi https://aka.ms/graph/v1.0/openapi.yaml --include-path /me/messages --exclude-backward-compatible --language CSharp --output ./src/MyMesagesApp/

cd src/MyMesagesApp
dotnet new console
   dotnet add package Microsoft.Kiota.Abstractions --version 1.9.7
   dotnet add package Microsoft.Kiota.Authentication.Azure --version 1.1.7
   dotnet add package Microsoft.Kiota.Http.HttpClientLibrary --version 1.4.3
   dotnet add package Microsoft.Kiota.Serialization.Form --version 1.2.5
   dotnet add package Microsoft.Kiota.Serialization.Json --version 1.3.3
   dotnet add package Microsoft.Kiota.Serialization.Multipart --version 1.1.5
   dotnet add package Microsoft.Kiota.Serialization.Text --version 1.2.2

dotnet publish -c Release -o publish

###
kiota search graph

kiota search apisguru::microsoft.com:graph-beta

kiota show -k apisguru::microsoft.com:graph-beta > graph-beta.log

kiota download apisguru::microsoft.com:graph-beta \
    --output ./src/AITGraph.Sdk/OpenApi/Graph.json

 ├─me

#  generate the client SDK for the Bing News Search API
kiota generate -l CSharp \
    --log-level trace \
    --output ./src/AITGraph.Sdk \
    --namespace-name AITGraph.Sdk \
    --class-name AITGraphApiClient \
    --include-path "/me/messages" \
    --include-path "/me/mailFolders" \
    --include-path "/me/photo" \
    --include-path "/me/profile" \
    --include-path "/me/profile/account" \
    --include-path "/me/calendar/events" \
    --exclude-backward-compatible \
    --openapi ./src/AITGraph.Sdk/OpenApi/Graph.json

cd src/AITGraph.Sdk
dotnet new console

   dotnet add package Microsoft.Kiota.Abstractions --version 1.9.7
   dotnet add package Microsoft.Kiota.Authentication.Azure --version 1.1.7
   dotnet add package Microsoft.Kiota.Http.HttpClientLibrary --version 1.4.3
   dotnet add package Microsoft.Kiota.Serialization.Form --version 1.2.5
   dotnet add package Microsoft.Kiota.Serialization.Json --version 1.3.3
   dotnet add package Microsoft.Kiota.Serialization.Multipart --version 1.1.5
   dotnet add package Microsoft.Kiota.Serialization.Text --version 1.2.2

###


kiota generate -l csharp -o bingNews -d https://raw.githubusercontent.com/Azure/azure-rest-api-specs/master/specification/cognitiveservices/data-plane/NewsSearch/stable/v1.0/NewsSearch.json -c BingNewsClient -n Bing

kiota info -d "https://raw.githubusercontent.com/Azure/azure-rest-api-specs/master/specification/cognitiveservices/data-plane/NewsSearch/stable/v1.0/NewsSearch.json" -l CSharp

The language CSharp is currently in Stable maturity level.
After generating code for this language, you need to install the following packages:
Package Name                             Version
Microsoft.Kiota.Abstractions             1.9.7
Microsoft.Kiota.Authentication.Azure     1.1.7
Microsoft.Kiota.Http.HttpClientLibrary   1.4.3
Microsoft.Kiota.Serialization.Form       1.2.5
Microsoft.Kiota.Serialization.Json       1.3.3
Microsoft.Kiota.Serialization.Multipart  1.1.5
Microsoft.Kiota.Serialization.Text       1.2.2

Hint: use the install command to install the dependencies.
Example:

cd bingNews
dotnet new console

   dotnet add package Microsoft.Kiota.Abstractions --version 1.9.7
   dotnet add package Microsoft.Kiota.Authentication.Azure --version 1.1.7
   dotnet add package Microsoft.Kiota.Http.HttpClientLibrary --version 1.4.3
   dotnet add package Microsoft.Kiota.Serialization.Form --version 1.2.5
   dotnet add package Microsoft.Kiota.Serialization.Json --version 1.3.3
   dotnet add package Microsoft.Kiota.Serialization.Multipart --version 1.1.5
   dotnet add package Microsoft.Kiota.Serialization.Text --version 1.2.2

kiota search news

kiota search apisguru::microsoft.com:cognitiveservices-NewsSearch

kiota download apisguru::microsoft.com:cognitiveservices-NewsSearch \
    --output ./src/NewsSearch.Sdk/OpenApi/NewsSearch.json

kiota show \
    --openapi ./src/NewsSearch.Sdk/OpenApi/NewsSearch.json

#  generate the client SDK for the Bing News Search API
kiota generate -l CSharp \
    --log-level trace \
    --output ./src/NewsSearch.Sdk \
    --namespace-name NewsSearch.Sdk \
    --class-name NewsSearchApiClient \
    --include-path "**/trendingtopics" \
    --exclude-backward-compatible \
    --openapi ./src/NewsSearch.Sdk/OpenApi/NewsSearch.json

kiota info -d "/Users/constantinevassilev02/MyLocalDocuments/go-projects/dotnetdev/kiota/./src/NewsSearch.Sdk/OpenApi/NewsSearch.json" -l CSharp

mkdir App
cd App
dotnet new console --use-program-main

dotnet new classlib
   dotnet add package Microsoft.Kiota.Abstractions --version 1.9.7
   dotnet add package Microsoft.Kiota.Authentication.Azure --version 1.1.7
   dotnet add package Microsoft.Kiota.Http.HttpClientLibrary --version 1.4.3
   dotnet add package Microsoft.Kiota.Serialization.Form --version 1.2.5
   dotnet add package Microsoft.Kiota.Serialization.Json --version 1.3.3
   dotnet add package Microsoft.Kiota.Serialization.Multipart --version 1.1.5
   dotnet add package Microsoft.Kiota.Serialization.Text --version 1.2.2

   dotnet add ./src/App reference ./src/NewsSearch.Sdk/