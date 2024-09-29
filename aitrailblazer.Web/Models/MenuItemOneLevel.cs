using System;
using System.Collections.Generic;
using System.Linq;

public class MenuItemOneLevel
{
    public string Category { get; set; }
    public List<string> Items { get; set; } = new List<string>();

    public MenuItemOneLevel(string category, List<string> items)
    {
        Category = category;
        Items = items ?? new List<string>();
    }

    public static MenuItemOneLevel CreateMenuItemOneLevelFromCommaDelimitedString(string category, string itemsCommaDelimited)
    {
        var items = itemsCommaDelimited.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(item => item.Trim())
                                       .ToList();
        return new MenuItemOneLevel(category, items);
    }
}
