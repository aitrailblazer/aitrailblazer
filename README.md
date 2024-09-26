## Hi there 👋

<!--
**aitrailblazer/aitrailblazer** is a ✨ _special_ ✨ repository because its `README.md` (this file) appears on your GitHub profile.

Here are some ideas to get you started:

- 🔭 I’m currently working on ...
- 🌱 I’m currently learning ...
- 👯 I’m looking to collaborate on ...
- 🤔 I’m looking for help with ...
- 💬 Ask me about ...
- 📫 How to reach me: ...
- 😄 Pronouns: ...
- ⚡ Fun fact: ...
-->

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureAd__ClientId=fd0e3c3f-9fdc-423e-a542-cee5f867d7e0

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureAd__ClientId=fd0e3c3f-9fdc-423e-a542-cee5f867d7e0

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureAd__ClientSecret=wB68Q~U822IcvQmxSgzaUa1Opo.1SS6VXuADYcAR

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureAd__Domain=aitrailblazer.com

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureAd__TenantId=common

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureAd__Instance=https://login.microsoftonline.com/



az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureAd__CallbackPath=/signin-oidc



az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars DownstreamApi__Scopes="user.read presence.read mailboxsettings.read mail.read mail.send calendars.read calendars.readwrite files.readwrite contacts.read contacts.readwrite notes.read notes.readwrite notes.create"

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars DownstreamApi__BaseUrl=https://graph.microsoft.com/v1.0


az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars GITHUB_TOKEN=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

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars SmartComponents__ApiKey=fd9965b66b094fa09abbde8420bfe954

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars SmartComponents__DeploymentName=gpt-4o-mini

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars SmartComponents__Endpoint=https://AITrailblazerEastUS.openai.azure.com/

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars BING_API_KEY=c6eab27c40af42acb66c68de12076103


az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureOpenAIKey03=fd9965b66b094fa09abbde8420bfe954


az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureOpenAIModelName02=gpt-4o-mini


az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureOpenAIModelName03=gpt-4o

az containerapp update \
  -n webfrontend \
  -g rg-aitrailblazer \
  --set-env-vars AzureOpenAIEndpoint03=https://AITrailblazerEastUS.openai.azure.com/
