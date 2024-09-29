using System.Collections.Generic;
using System.Linq;

namespace aitrailblazer.net.Models
{
    /// <summary>
    /// Represents a customized copilot as part of an order
    /// </summary>
    public class Copilot
    {
        public const int DefaultSize = 12;
        public const int MinimumSize = 9;
        public const int MaximumSize = 17;

        public int Id { get; set; }

        public int OrderId { get; set; }

        public CopilotSpecial? Special { get; set; }

        public int SpecialId { get; set; }

        public int Size { get; set; }

        public List<CopilotSpecial> Toppings { get; set; } = new List<CopilotSpecial>();

        public decimal GetBasePrice()
        {
            return Special != null ? ((decimal)Size / (decimal)DefaultSize) * Special.BasePrice : 0;
        }

        public decimal GetTotalPrice()
        {
            return GetBasePrice();
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }
    }
}
