
namespace AITrailblazer.net.Services
{
    public class ResponseLengthService
    {
        public static int TransformResponseLength(string value)
        {
            return value switch
            {
                "16" => 16,
                "32" => 32,
                "64" => 64,
                "128" => 128,
                "1024" => 1024,
                "2048" => 2048,
                "4096" => 4096,
                "8192" => 8192,
                "16384" => 16384,
                _ => 512, // Default case
            };
        }


    }
}
