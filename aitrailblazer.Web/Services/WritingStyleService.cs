namespace AITrailblazer.net.Services
{
    public class WritingStyleService
    {
     
        public static string GetLabelForWritingStyle(string writingStyle)
        {
            return writingStyle switch
            {
                "Casual" => "Casual, professional: Strikes a balance between casual and formal language, suitable for everyday communication in professional settings. This style is polished and refined, conveying information clearly and respectfully while maintaining professionalism.",
                "Formal" => "Formal: Adheres to formal conventions and standards, using formal language and a structured approach to present scholarly information. Formal writing is rigorous, objective, and meticulously researched, catering to an academic audience.",
                "Technical" => "Technical: Utilizes specialized terminology and precise language to communicate complex or specialized information. Technical writing is clear, concise, and structured, catering to a specific audience with technical expertise.",
                "Persuasive" => "Persuasive: Aims to convince or persuade the audience to adopt a particular viewpoint or take a specific action. Persuasive writing employs rhetorical devices, compelling arguments, and emotional appeals to influence the reader's perspective.",
                "Narrative" => "Narrative: Tells a story or recounts events in a compelling and engaging manner, drawing the reader into the narrative world. Narrative writing evokes emotions, creates vivid imagery, and captivates the audience through storytelling techniques.",
                "Exhaustive" => "Exhaustive: Represents the highest level of detail and thoroughness, leaving no stone unturned in its exploration of a topic. Exhaustive writing provides an extensive examination, covering every conceivable aspect comprehensively.",
                "Comprehensive" => "Comprehensive: Goes beyond mere detail to offer a complete and exhaustive overview of a subject, covering all relevant aspects comprehensively. Comprehensive writing provides a thorough understanding by delving into various dimensions of the topic.",
                "Detailed" => "Detailed: Provides thorough and comprehensive coverage of a topic, offering in-depth explanations and insights into various aspects. Detailed writing is meticulous in its exploration, leaving no important information unaddressed.",
                "Concise" => "Concise: Focuses on delivering information efficiently and effectively, using clear and straightforward language to communicate ideas succinctly. Concise writing avoids unnecessary details and verbosity, ensuring that the message is easily understood.",
                "Laconic" => "Laconic: Emphasizes brevity and succinctness, conveying information with minimal words while maintaining clarity and relevance. Laconic writing is concise yet impactful, leaving a lasting impression with its directness.",
                _ => string.Empty
            };
        }
        public static string GetLabelForWritingStylePrompt(string writingStyle)
        {
            return writingStyle switch
            {
                "Casual" => "Casual, professional: Everyday language is the language used in daily communication that is informal and conversational. It is straightforward and accessible, typically free from technical or specialized jargon, making it suitable for casual interactions among a wide audience. This language style emphasizes ease of understanding while upholding professionalism and ensuring clarity and respect in every interaction.Use a mix of long and short sentences, as it is in natural spoken language.",
                "Formal" => "Formal: This style adheres to high standards of professionalism and formality, suitable for environments that require a structured and rigorous approach to communication. It uses formal language to present information in a clear, objective manner, ensuring meticulous attention to detail and thorough research. While it is particularly well-suited for scholarly or professional audiences, its principles of clarity, objectivity, and structured presentation are applicable across various formal contexts.",
                "Technical" => "Technical: Utilizes specialized terminology and precise language to communicate complex or specialized information. Technical writing is clear, concise, and structured, catering to a specific audience with technical expertise.",
                "Persuasive" => "Persuasive: Aims to convince or persuade the audience to adopt a particular viewpoint or take a specific action. Persuasive writing employs rhetorical devices, compelling arguments, and emotional appeals to influence the reader's perspective.",
                "Narrative" => "Narrative: Tells a story or recounts events in a compelling and engaging manner, drawing the reader into the narrative world. Narrative writing evokes emotions, creates vivid imagery, and captivates the audience through storytelling techniques.",
                "Exhaustive" => "Exhaustive:Represents the highest level of detail and thoroughness, leaving no stone unturned in its exploration of a topic. Exhaustive writing provides an extensive examination, covering every conceivable aspect comprehensively.",
                "Comprehensive" => "Comprehensive: Goes beyond mere detail to offer a complete and exhaustive overview of a subject, covering all relevant aspects comprehensively. Comprehensive writing provides a thorough understanding by delving into various dimensions of the topic.",
                "Detailed" => "Detailed: Provides thorough and comprehensive coverage of a topic, offering in-depth explanations and insights into various aspects. Detailed writing is meticulous in its exploration, leaving no important information unaddressed.",
                "Concise" => "Concise: Focuses on delivering information efficiently and effectively, using clear and straightforward language to communicate ideas succinctly. Concise writing avoids unnecessary details and verbosity, ensuring that the message is easily understood.",
                "Laconic" => "Laconic: Emphasizes brevity and succinctness, conveying information with minimal words while maintaining clarity and relevance. Laconic writing is concise yet impactful, leaving a lasting impression with its directness.",
                _ => string.Empty // Default case for values without specific labels
            };
        }


    }
}
