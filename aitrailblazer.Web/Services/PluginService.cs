using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aitrailblazer.net.Services
{
    public class PluginService
    {
        private readonly string _pluginsPath;

        public PluginService(string pluginsPath)
        {
            _pluginsPath = pluginsPath;
        }

        // Method to get the path of the Plugins folder
        public string GetPluginsPath()
        {
            return _pluginsPath;
        }

        // Method to get the list of plugin files
        public IEnumerable<string> GetPluginFiles()
        {
            try
            {
                // Log the path being checked
                Console.WriteLine($"Checking directory: {_pluginsPath}");

                // Check if the directory exists
                if (Directory.Exists(_pluginsPath))
                {
                    // Log the full path to ensure it's correct
                    Console.WriteLine($"Directory exists: {_pluginsPath}");

                    // Get all files from the directory and subdirectories
                    var files = Directory.GetFiles(_pluginsPath, "*.*", SearchOption.AllDirectories);

                    // Log the number of files found and their paths
                    Console.WriteLine($"Found {files.Length} files.");
                    foreach (var file in files)
                    {
                        Console.WriteLine($"File: {file}");
                    }

                    // Return the file names relative to the plugins path
                    return files.Select(file => Path.GetRelativePath(_pluginsPath, file));
                }
                else
                {
                    // Log if the directory does not exist
                    Console.WriteLine("Directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur
                Console.WriteLine($"Error accessing directory: {ex.Message}");
            }

            // Return an empty list if there are no files or if there was an error
            return Enumerable.Empty<string>();
        }
    }
}
