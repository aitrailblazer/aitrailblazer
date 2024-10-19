// Copyright (c) Microsoft. All rights reserved.
using Microsoft.TypeChat;

using System.Text.Json.Serialization;
using Microsoft.TypeChat.Schema;


public class SentimentResponse
{
    [JsonPropertyName("sentiment")]
    [JsonVocab("negative | neutral | positive")]
    public string Sentiment { get; set; }
}
