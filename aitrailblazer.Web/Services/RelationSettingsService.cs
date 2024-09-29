namespace aitrailblazer.net.Services
{
    public class RelationSettingsService
    {
    
        public static string GetLabelForRelationSettingsTitle(string preference)
        {
            return preference switch
            {
                "Peer-to-Peer" => "Peer-to-Peer" + " |",
                "Upward"=> "Upward Communication." + " |",
                "Downward" => "Downward Communication." + " |",
                "Customer" => "Customer Engagement" + " |",
                "Partner" => "Partner Relations" + " |",
                "Investor" => "Investor Relations" + " |",
                "Public Relations" => "Public Relations" + " |",
                "Crisis" => "Crisis Communication" + " |",
                "Instructional" => "Instructional" + " |",
                "Motivational" => "Motivational" + " |",
                "Innovative" => "Innovative Communication." + " |",
                _ => ""
            };
        }

        public static string GetLabelForRelationSettingsPrompt(string preference)
        {
            return preference switch
            {
                "Peer-to-Peer" => "Peer-to-Peer: Direct and informal, fostering a collaborative environment.",
                "Upward" => "Upward Communication: Respectful and formal, designed for reporting to superiors.",
                "Downward" => "Downward Communication: Clear and directive, for providing guidance to subordinates.",
                "Customer" => "Customer Engagement: Friendly and service-oriented, aimed at satisfying customer needs.",
                "Partner" => "Partner Relations: Cooperative and mutually beneficial, focusing on long-term relationship building.",
                "Investor" => "Investor Relations: Informative and strategic, aimed at maintaining investor confidence.",
                "Public Relations" => "Public Relations: Positive and engaging, intended to shape public perception.",
                "Crisis" => "Crisis Communication: Urgent and transparent, for managing emergencies and negative feedback.",
                "Instructional" => "Instructional: Educational and clear, designed for teaching or explaining.",
                "Motivational" => "Motivational: Inspiring and uplifting, aimed at encouraging and motivating others.",
                "Innovative" => "Innovative Communication: Creative and visionary, pushing boundaries for new ways of interaction.",
                _ => ""
            };
        }
    }

}
