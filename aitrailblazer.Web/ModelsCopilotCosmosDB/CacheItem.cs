﻿namespace Cosmos.Copilot.Models;

public record CacheItem
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public string Id { get; set; }

    public float[] Vectors { get; set; }
    public string Prompts { get; set; }

    public string Output { get; set; }

    public CacheItem(float[] vectors, string prompts, string output)
    {
        Id = Guid.NewGuid().ToString();
        Vectors = vectors;
        Prompts = prompts;
        Output = output;
    }
}
