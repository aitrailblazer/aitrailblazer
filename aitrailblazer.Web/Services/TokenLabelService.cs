namespace AITrailblazer.net.Services
{
    public class TokenLabelService
    {

        public static string GetLabelForMaxTokensFromInt(int maxTokens)
        {
            return maxTokens switch
            {
16 => "16 tokens: This count is typically utilized in basic applications where minimal data exchange is required. It serves as a foundational unit in many programming contexts.",

32 => "32 tokens: Commonly found in cryptographic functions and hashing algorithms, 32 tokens provide a balance between security and performance.",

64 => "64 tokens: Often used in networking protocols, 64 tokens allow for more extensive data packets while maintaining efficiency.",

128 => "128 tokens: Frequently employed in machine learning models, 128 tokens can encapsulate a significant amount of information without overwhelming processing capabilities.",

256 => "256 tokens: A common choice for text generation tasks, 256 tokens enable the creation of coherent paragraphs while retaining focus on the main topic.",

512 => "512 tokens: Often used in academic writing or technical documentation, 512 tokens provide ample space for thorough explanations and analyses.",

1024 => "1024 tokens: A standard length for comprehensive articles or discussions, this count facilitates detailed exploration of subjects while ensuring readability.",

2048 => "2048 tokens: Ideal for extensive research papers or lengthy narratives, 2048 tokens allow for an in-depth examination of complex topics.",

4096 => "4096 tokens: This size is often utilized in large-scale data processing tasks or when handling substantial datasets that require intricate analysis.",

8192 => "8192 tokens: Typically reserved for exhaustive reports or extensive documentation, 8192 tokens enable the inclusion of vast amounts of information without sacrificing coherence.",

16384 => "16384 tokens: The upper limit for many applications, this count is suitable for comprehensive databases or large-scale machine learning models that demand significant input data.",
            };
        }
    }
}
