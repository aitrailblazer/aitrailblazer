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
    --include-path "/me/drive" \
    --include-path "/me/photo" \
    --include-path "/me/profile" \
    --include-path "/me/profile/account" \
    --include-path "/me/calendar/events" \
    --include-path "/me/calendar/calendarView"  \
    --exclude-backward-compatible \
    --openapi ./AITGraph.Sdk/OpenApi/Graph.json

###

# dotnet add ./src/App reference ./src/NewsSearch.Sdk/


gh secret set AzureAd__ClientId -b "123"
gh secret set AzureAd__ClientSecret -b "123"  
gh secret set AzureAd__Domain -b "123"  
gh secret set AzureAd__TenantId -b "common"
gh secret set AzureAd__Instance -b "123" 
gh secret set AzureAd__CallbackPath -b "123"
gh secret set DownstreamApi__Scopes -b "123"
gh secret set DownstreamApi__BaseUrl -b "https://graph.microsoft.com/v1.0"
gh secret set GTB_TOKEN -b "123"
gh secret set SmartComponents__ApiKey -b "123"
gh secret set SmartComponents__DeploymentName -b "123" 
gh secret set SmartComponents__Endpoint -b "123"
gh secret set BING_API_KEY -b "123"
gh secret set AzureOpenAIKey03 -b "123"
gh secret set AzureOpenAIModelName02 -b "123"
gh secret set AzureOpenAIModelName03 -b "123"
gh secret set AzureOpenAIEndpoint03 -b "123"
gh secret set TenantId -b "common"
gh secret set ClientId -b "123"
gh secret set ClientSecret -b "123"
gh secret set StorageConnectionString -b "123"
gh secret set StorageContainerName -b "123"
