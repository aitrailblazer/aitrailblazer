using System.Collections.Generic;
using Microsoft.FluentUI.AspNetCore.Components;

namespace aitrailblazer.net.Models
{
    public class ComponentModel
    {
        public string WorkflowName { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        public Icon Icon { get; set; }
        public List<ComponentModel> SubComponents { get; set; } = new List<ComponentModel>();
    }

}
