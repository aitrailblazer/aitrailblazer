using System;
using System.IO;
using System.Threading.Tasks;

namespace AITrailblazer.net.Services
{
    public class PromptyTemplateService
    {
        private readonly PluginService _pluginService;

        public PromptyTemplateService(PluginService pluginService)
        {
            _pluginService = pluginService;
        }

        public async Task<string> GetPromptyTemplateAsync(string pluginName)
        {
            // Get the base path for plugins
            var pluginPath = _pluginService.GetPluginsPath();

            // Append the plugin name and the .prompty extension
            pluginPath = pluginPath + "/" + pluginName + ".prompty";

            // Log the path for debugging purposes
            Console.WriteLine($"GetPromptyTemplateAsync Prompty file path: {pluginPath}");

            // Read the content of the .prompty file
            if (!File.Exists(pluginPath))
            {
                throw new FileNotFoundException($"Prompty file not found at {pluginPath}");
            }

            var promptyTemplate = await File.ReadAllTextAsync(pluginPath);

            // Return the content of the .prompty file
            return promptyTemplate;
        }
    }
}
