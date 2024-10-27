namespace AITrailblazer.net.Services
{
    public class ResponseStylePreferenceService
    {
      
        public  static string GetTextLabelForCommandsCustomPrompt(string value)
        {
            return value switch
            {
                "one" => "Self-consistency (SC-CoT)",
                "two" => "Zero-Shot Chain-of-Thought (ZS-CoT)",
                "three" => "Chain-of-Thought (CoT)",
                "four" => "Automatic Chain of Thought (Auto-CoT)",
                "five" => "Tree of thoughts (ToT)",
                "six" => "Graph of thoughts (GoT)",
                "seven" => "Skeleton-of-Thought (SoT)",
                _ => "Unknown Custom Command"
            };
        }


        public  static string GetLabelForResponseStylePreferencePrompt(string preference)
        {
            return preference switch
            {
                "Summary" => "Provide concise summary to enable users to quickly understand the essential information from the text. Focus on clarity and brevity, distilling complex content into its most important elements.",
                "Alternatives" => "Explore Alternatives: Identify and analyze alternatives related to the topic at hand.",
                "Elaborate" => "Elaborate: Elaborate on the topic at hand.",
                "Compare" => "Compare and Contrast: Analyze differences and similarities.",
                "Clarify" => "Clarify: Seek additional clarity on confusing or ambiguous information.",
                "Predict" => "Predict: Predict future outcomes based on current information",
                "Synthesize" => "Synthesize: Combine information from multiple sources into a cohesive understanding.",
                "Question"=> "Question: Encourage questions about the presented information.",
                "Reflect"=> "Reflect: Reflect on the information or ideas presented.",
                "Strict" => "Strict: Adhere closely to the format of the input. If the input contain instructions follow the instructions precisely, without deviation.",
                "Innovate" => "Innovate: Foster creativity and innovation by pushing the boundaries of conventional thinking.",
                _ => ""
            };
        }
    }
}
