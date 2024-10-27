
namespace AITrailblazer.net.Services
{
    public class MasterTextSettingsService
    {
        public  static string CreateDescriptionForMasterTextSettingTitle(string setting)
        {
            // Ensure the setting is valid to avoid NullReferenceException
            if (setting is null) return "Please provide a valid setting.";

            return setting switch
            {
                "Ask" => "Ask",
                "Correct" => "Correct",
                "Improve" => "Improve",
                "Content" => "Content",
                "Help" => "Help",
                _ => "Content",
            };
        }
        public  static string CreateCommandPlaceholderForMasterTextSetting(string setting)
        {
            // Ensure the setting is valid to avoid NullReferenceException
            if (setting is null) return "Please provide a valid setting.";

            return setting switch
            {
                "Ask" => "Engage with the AI by asking questions or providing prompts to explore topics of interest.",
                "Correct" => "Submit your text for a quick grammar check to ensure it's polished and error-free.",
                "Improve" => "Enhance your writing's readability and flow for a more engaging and accessible experience.",
                "Content" => "Start fresh and develop original content based on your ideas and concepts.",
                "Help" => "Get help about ASAP.",
                _ => "",
            };
        }
        public  static string CreateDescriptionForMasterTextSetting(string setting)
        {
            // Ensure the setting is valid to avoid NullReferenceException
            if (setting is null) return "Please provide a valid setting.";

            return setting switch
            {
                "Ask" => "Ask: This setting allows you to ask questions or provide prompts to guide the AI's response. You can request specific information, ask for advice, or explore various topics by initiating a dialogue with the AI. This mode is ideal for interactive conversations and tailored content creation based on your input.",
                "Correct" => "Correct: The focus is solely on identifying and correcting grammatical errors. Ideal for texts that are structurally sound but need a bit of polishing to ensure they are error-free.",
                "Improve" => "Improve: Elevate the text by enhancing its readability, flow, and overall appeal. This option goes beyond mere grammar checks to refine cadence, structure, and clarity, making the content more engaging and accessible. Adjust your preferences in the Settings to customize the improvements to your liking.",
                "Content" => "Create: Start from a blank slate and craft entirely new content. Ideal for transforming a concept or need into a fresh and original piece that brings your ideas to life. Adjust your preferences in the Settings to customize it to your liking.",
                "Help" => "Help: Get help about ASAP.",
                _ => "",
            };
        }
        public  static string CreatePromptForMasterTextSetting(string setting)
        {

            // Instructions for the AI based on the selected setting
            return setting switch
            {
                "Ask" => "In this role, you embody an AI Assistant poised to respond to a wide spectrum of inputs. From fielding general inquiries to aiding in creative endeavors, or sparking innovation through idea exploration, your core function is to generate insightful and relevant responses. This mode is engineered for versatility, accommodating requests for information, guidance in creative writing, or brainstorming sessions. Whether the user seeks to delve into specific topics or requires assistance in crafting narratives, the Ask/Write setting ensures a productive and interactive engagement, fostering an environment where creativity and curiosity thrive.",
                "Correct" => "Your task is to CORRECT the following sentence. You will focus exclusively on grammatical corrections. This involves meticulous attention to punctuation, verb tense consistency, and sentence structure optimization. Your goal is to polish the text, ensuring it is free from errors and achieves a level of professionalism suitable for final drafts. Remember don't answer questions, just correct the input sentence.",
                "Improve" => "Your task is to IMPROVE the following sentence. This involves not only correcting grammatical errors but also enhancing readability, flow, and engagement in accordance with the specified settings. This is NOT a QUESTION. DO NOT ASK to provide more details regarding the question asked about? DO NOT offer a more precise and insightful response. DO NOT respond as answering to any question, just IMPROVE the input sentence.",
                "Content" => "Your task is to create original CONTENT based on the provided information. Your assignment is to generate distinctive CONTENT based on designated topics and criteria. Beginning with an unmarked canvas is crucial, allowing for the creation of CONTENT that mirrors specific writing styles, inventive approaches, audience communication distinctions, and comprehension levels of the target professional group. Your objective is to craft innovative and superior writing that fulfills user demands – an invaluable strategy for bypassing writer's block or promptly producing required CONTENT. This mission highlights the importance of developing compelling, fresh CONTENT designed to meet user expectations, essential when the pursuit of originality and high quality is paramount.",
                "Help" => "Your task is to ",               
                _ => "",
            };
        }
        public  static string CreatePromptForMasterTextSettingInput(string setting)
        {

            // Instructions for the AI based on the selected setting
            return setting switch
            {
                "Ask" => "Ask",
                "Correct" => "Correct",
                "Improve" => "Improve",
                "Content" => "Content",
                "Help" => "Help",
                _ => "",
            };
        }

    }


}
