#!/usr/bin/env bash

#rm -rf src

###
# kiota search graph

/search/microsoft.graph.query

# kiota search apisguru::microsoft.com:graph-beta

# kiota show -k apisguru::microsoft.com:graph-beta > graph-beta.log

# kiota download apisguru::microsoft.com:graph-beta \
#    --output ./src/AITGraph.Sdk/OpenApi/Graph.json


#  generate the client SDK for the Bing News Search API
kiota generate -l CSharp \
    --log-level trace \
    --output ./AITGraph.Sdk \
    --namespace-name AITGraph.Sdk \
    --class-name AITGraphApiClient \
    --include-path "me/outlook/microsoft.graph.supportedTimeZones()" \
    --include-path "/me/messages" \
    --include-path "/me/mailFolders" \
    --include-path "/me/contacts" \
    --include-path "/me/drive" \
    --include-path "/me/photo" \
    --include-path "/me/profile" \
    --include-path "/me/profile/account" \
    --include-path "/me/calendar" \
    --include-path "/me/calendar/events" \
    --include-path "/me/calendar/calendarView"  \
    --exclude-backward-compatible \
    --openapi ./AITGraph.Sdk/OpenApi/Graph.json

###

kiota search apisguru::microsoft.com
Key                                                          Title                                        De
apisguru::microsoft.com:cognitiveservices-AutoSuggest        AutoSuggest Client                           Au
apisguru::microsoft.com:cognitiveservices-ComputerVision     Computer Vision Client                       ThCo
apisguru::microsoft.com:cognitiveservices-CustomImageSearch  Custom Image Search Client                   Th
apisguru::microsoft.com:cognitiveservices-CustomSearch       Custom Search Client                         Th
apisguru::microsoft.com:cognitiveservices-EntitySearch       Entity Search Client                         Th
apisguru::microsoft.com:cognitiveservices-ImageSearch        Image Search Client                          Th
apisguru::microsoft.com:cognitiveservices-LocalSearch        Local Search Client                          Th
apisguru::microsoft.com:cognitiveservices-NewsSearch         News Search Client                           Th
apisguru::microsoft.com:cognitiveservices-Ocr                Computer Vision Client                       ThCo
apisguru::microsoft.com:cognitiveservices-Prediction         Custom Vision Prediction Client
apisguru::microsoft.com:cognitiveservices-SpellCheck         Spell Check Client                           Th
apisguru::microsoft.com:cognitiveservices-Training           Custom Vision Training Client
apisguru::microsoft.com:cognitiveservices-VideoSearch        Video Search Client                          Th
apisguru::microsoft.com:cognitiveservices-VisualSearch       Visual Search Client                         Vi
apisguru::microsoft.com:cognitiveservices-WebSearch          Web Search Client                            Th
apisguru::microsoft.com:graph-beta                           OData Service for namespace microsoft.graph  Th

# NewsSearch.Sdk
kiota search news

kiota download apisguru::microsoft.com:cognitiveservices-NewsSearch \
    --output ./src/CognitiveServices.Sdk/OpenApi/CognitiveServicesNewsSearch.json

kiota download apisguru::microsoft.com:cognitiveservices-WebSearch \
    --output ./src/CognitiveServices.Sdk/OpenApi/CognitiveServicesWebSearch.json

kiota download apisguru::microsoft.com:cognitiveservices-EntitySearch \
    --output ./src/CognitiveServices.Sdk/OpenApi/CognitiveServicesEntitySearch.json

kiota generate -l CSharp \
    --log-level trace \
    --output ./CognitiveServices.Sdk \
    --namespace-name CognitiveServices.Sdk \
    --class-name CognitiveServicesApiClient \
    --include-path "**/trendingtopics" \
    --include-path "**/search" \
    --exclude-backward-compatible \
    --openapi ./src/CognitiveServices.Sdk/OpenApi/CognitiveServicesNewsSearch.json

kiota generate -l CSharp \
    --log-level trace \
    --output ./CognitiveServices.Sdk \
    --namespace-name CognitiveServices.Sdk \
    --class-name CognitiveServicesApiClient \
    --include-path "**/search" \
    --exclude-backward-compatible \
    --openapi ./src/CognitiveServices.Sdk/OpenApi/CognitiveServicesWebSearch.json

kiota generate -l CSharp \
    --log-level trace \
    --output ./CognitiveServices.Sdk \
    --namespace-name CognitiveServices.Sdk \
    --class-name CognitiveServicesApiClient \
    --include-path "**/entities" \
    --exclude-backward-compatible \
    --openapi ./src/CognitiveServices.Sdk/OpenApi/CognitiveServicesEntitySearch.json


cd CognitiveServices.Sdk
dotnet new classlib 

dotnet add package Microsoft.Kiota.Authentication.Azure 
dotnet add package Microsoft.Kiota.Bundle 

