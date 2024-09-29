using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace aitrailblazer.net.Models
{
    public class WizardStep
    {
        public string Label { get; set; }
        public string Summary { get; set; }
        public RenderFragment Content { get; set; }
    }
}
