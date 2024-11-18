using System;

namespace AITrailblazer.net.Services;
public class CreativitySettingsService
{

    public static float GetLabelForCreativityTitle(string temperature)
    {
        return temperature switch
        {
            "Exact" => 0.0F,
            "Focused" => 0.1F,
            "Analytical" => 0.2F,
            "Balanced" => 0.3F,
            "Adaptable" => 0.4F,
            "Open" => 0.5F,
            "Inspired" => 0.6F,
            "Creative" => 0.7F,
            "Innovative" => 0.8F,
            "Visionary" => 0.9F,
            "Revolutionary" => 1.0F,
            _ => 0.5F
        };
    }

    public static string GetTextLabelForCreativityPrompt(double temperature)
    {
        return temperature switch
        {
            0.0F => "Deterministic: Highly deterministic and precise, eliminating randomness to ensure accuracy and relevance. Ideal for tasks demanding strict adherence to a given topic or style.",
            0.1F => "Focused: Highly deterministic and precise, minimizing randomness in favor of accuracy and relevance. Best for tasks requiring strict adherence to a topic or style.",
            0.2F => "Analytical: Outputs are logical and detailed, with a slight increase in diversity to accommodate complex reasoning or explanations.",
            0.3F => "Balanced: A middle ground, offering a blend of predictability and creativity that's suitable for a wide range of tasks.",
            0.4F => "Adaptable: Versatile and flexible, capable of producing varied outputs that remain relevant to the context.",
            0.5F => "Open: Welcoming a broader range of ideas and expressions without straying too far from coherence.",
            0.6F => "Inspired: Encourages more novel and unexpected outputs, akin to a burst of inspiration, while still grounded in the initial prompt.",
            0.7F => "Creative: Highly inventive and original, pushing the boundaries of conventional responses.",
            0.8F => "Innovative: Focused on groundbreaking ideas, encouraging the model to explore less obvious paths and solutions.",
            0.9F => "Visionary: Prioritizes bold, forward-thinking concepts, even at the expense of occasional relevance or coherence.",
            1.0F => "Revolutionary: Maximizes the potential for radical and transformative ideas, with full openness to the spectrum of possible outputs.",
            _ => "Focused: Highly deterministic and precise, minimizing randomness in favor of accuracy and relevance. Best for tasks requiring strict adherence to a topic or style."
        };

    }


}

