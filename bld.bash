#!/usr/bin/env bash

#rm -rf src

###
# kiota search graph

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
    --include-path "/me/messages" \
    --include-path "/me/mailFolders" \
    --include-path "/me/photo" \
    --include-path "/me/profile" \
    --include-path "/me/profile/account" \
    --include-path "/me/calendar/events" \
    --exclude-backward-compatible \
    --openapi ./AITGraph.Sdk/OpenApi/Graph.json

###

# dotnet add ./src/App reference ./src/NewsSearch.Sdk/