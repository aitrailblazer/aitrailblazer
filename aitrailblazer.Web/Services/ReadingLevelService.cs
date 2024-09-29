
namespace aitrailblazer.net.Services
{
    public class ReadingLevelService
    {
        public static string TransformTargetReadingRange(float value)
        {
            return value switch
            {
                0.0F => "Novice: Use simple words and short sentences from daily life. Introduce new vocabulary in familiar contexts, and repeat them for better understanding.",
                0.2F => "Intermediate: Write in an easy-to-understand style using simple patterns and contexts. Explain main ideas directly with common words, avoiding complexity.",
                0.4F => "Advanced: Use clear prose, relatable topics, and predictable structures. Encourage engagement with narratives and reasoned arguments, using contextual clues for complex ideas.",
                0.6F => "Superior: Write for readers who understand diverse and complex texts. Use a broad vocabulary, intricate grammar, and cultural insights. Cover specialized topics clearly for those familiar with various genres.",
                0.8F => "Distinguished: Engage with complex, challenging material using sophisticated vocabulary and advanced structures. Include specific terminology and rhetorical forms to convey deep knowledge, catering to readers accustomed to a wide range of topics and styles.",
                _ => "Unknown Communication Preference"
            };
        }
        public static string TransformTargetReadingRangePrompt(string value)
        {
            // Translate the input value to specific instructions for creating content tailored to different levels of English reading proficiency according to ACTFL standards.
            return value switch
            {
                "Novice" => "You are addressing Novice readers under the ACTFL English Reading Proficiency Levels. Employ simple vocabulary and straightforward sentences that appear in everyday life. Introduce new words within familiar contexts, using repetition to aid memorization and comprehension.",
                "Intermediate"  => "You are crafting content for Intermediate readers according to ACTFL guidelines. Utilize a clear, accessible writing style, featuring common vocabulary and simple sentence structures. Focus on conveying main ideas directly, steering clear of complex language or concepts.",
                "Advanced" => "You are writing for Advanced readers as defined by ACTFL's proficiency levels. Your prose should be clear, engaging, and structured predictably, revolving around relatable subjects. Foster interaction through stories and logical arguments, guiding understanding of more complex ideas with contextual hints.",
                "Superior" => "You are targeting Superior readers in ACTFL's framework. Your writing should cater to individuals who comprehend a variety of complex texts. Incorporate an extensive vocabulary, sophisticated grammatical constructions, and cultural nuances. Ensure specialized subjects are articulated clearly, appealing to those well-versed across multiple genres.",
                "Distinguished" => "You are engaging with Distinguished readers according to ACTFL standards. Your material should be complex and challenging, marked by refined vocabulary and advanced grammatical structures. Use specific terminology and rhetorical strategies to express in-depth knowledge, aiming at readers familiar with an extensive range of topics and stylistic approaches.",
                _ => ""
            };
        }
  
    }
}
