namespace AITrailblazer.net.Services
{
    public class AICopilotDescriptionService
    {

        public static string GetLabelForAICopilotDescriptionService(string label)
        {
            return label switch
            {
                "AIWritingAssistant" => "A comprehensive tool for crafting high-quality content tailored to your needs.",
                "AIKeyPointsWizard" => "Extracts and summarizes the key points from any document.",
                "AIClearNote" => "Summarizes notes effectively, ensuring clarity and conciseness. Includes Gartner's example with best practices for scaling generative AI.",
                "AIEmailWizard" => "Assists in composing professional and effective emails.",
                "AIInternalMemo" => "Facilitates the creation of internal memos following best practices.",
                "AIMessageOptimizer" => "Optimizes your messages for better engagement using neural marketing techniques.",
                "AINamesGen" => "Helps in generating and choosing the right names for your projects or products.",
                "AiTaskWizard" => "Converts documents into manageable tasks effortlessly.",
                "AIOntologyGen" => "Generates custom ontologies for structured data representation.",
                "AIReportGen" => "Creates professional reports with ease.",
                "AISoftwareDocGen" => "Generates comprehensive software documentation.",
                "AISoftwareCodeGen" => "Generates high-quality code for various applications.",
                "COSE" => "Implements Cognitive Optimized Sparse Encoding for efficient data processing.",
                "AIDiagramCodexSequence" => "Transforms your vision into detailed Sequence diagrams.",
                "AIDiagramCodexActivity" => "Transforms your vision into detailed Activity diagrams.",
                 "AIStrategiX" => "Provides strategic insights for informed decision-making.",
                "AiInSightOut" => "Enhances conversations and analysis for strategic analysis.",
                "AIBizVisual" => "Generates business visualizations to support your data-driven decisions.",
                "AILightRayArt" => "Creates stunning corporate art for your brand.",
                "AIVideoPromptGen" => "Generates engaging video prompts for various purposes.",
                _ => string.Empty
            };
        }

        public static string GetFriendlyNameForAICopilotService(string label)
        {
            return label switch
            {
                "AIWritingAssistant" => "WritingAssistant",
                "AIKeyPointsWizard" => "KeyPoints",
                "AIClearNote" => "ClearNote",
                "AIEmailWizard" => "EmailWizard",
                "AIInternalMemo" => "InternalMemo",
                "AIMessageOptimizer" => "MessageOptimizer",
                "AINamesGen" => "NamesGen",
                "AiTaskWizard" => "Tasks",
                "AIOntologyGen" => "OntologyGen",
                "AIReportGen" => "ReportGen",
                "AISoftwareDocGen" => "DocGen",
                "AISoftwareCodeGen" => "CodeGen",
                "COSE" => "COSE",
                "AIDiagramCodexSequence" => "DiagrammingSequence",
                "AIDiagramCodexActivity" => "DiagrammingActivity",
                "AIStrategiX" => "StrategicInsight",
                "AIInSightOut" => "StrategicAnalysis",
                "AIAletheia" => "AIAletheia",
                "AIBizVisual" => "BizVisual",
                "AILightRayArt" => "LightRayArt",
                "AIVideoPromptGen" => "VideoPromptGen",
                "AICustomerAccountStatus" => "CustomerAccountStatus",
                _ => string.Empty
            };
        }
        public static string GetSystemNameForAICopilotService(string label)
        {
            return label switch
            {
                "WritingAssistant" => "AIWritingAssistant",
                "KeyPoints" => "AIKeyPointsWizard",
                "ClearNote" => "AIClearNote",
                "EmailWizard" => "AIEmailWizard",
                "InternalMemo" => "AIInternalMemo",
                "MessageOptimizer" => "AIMessageOptimizer",
                "NamesGen" => "AINamesGen",
                "Tasks" => "AiTaskWizard",
                "OntologyGen" => "AIOntologyGen",
                "ReportGen" => "AIReportGen",
                "DocGen" => "AISoftwareDocGen",
                "CodeGen" => "AISoftwareCodeGen",
                "COSE" => "COSE",
                "DiagrammingSequence" => "AIDiagramCodexSequence",
                "DiagrammingActivity" => "AIDiagramCodexActivity",
                "StrategicInsight" => "AIStrategiX",
                "StrategicAnalysis" => "AiInSightOut",
                "AIAletheia" => "AIAletheia",
                "BizVisual" => "AIBizVisual",
                "LightRayArt" => "AILightRayArt",
                "VideoPromptGen" => "AIVideoPromptGen",
                _ => string.Empty
            };
        }

    }
}
