// Models/OntologyTree.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text; // For StringBuilder
using System.Text.Json.Serialization;
using Microsoft.FluentUI; // Fluent UI namespace
using Microsoft.FluentUI.AspNetCore.Components; // Fluent UI Blazor components
using System.Linq; // For LINQ operations
using System.Threading.Tasks; // For Task

namespace OntologyTreeApp.Models
{
    public enum ItemStatus
    {
        Draft,
        Review,
        Published,
        Archived
    }

    public abstract class TreeItem : ITreeItem
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("title")]
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters.")]
        public virtual string Title { get; set; } = string.Empty;

        [JsonPropertyName("parentId")]
        public Guid? ParentId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemStatus Status { get; set; } = ItemStatus.Draft;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        public virtual string GetTitle() => Title;

        public virtual string GetId() => Id.ToString();

        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public interface ITreeItem
    {
        Guid Id { get; set; }
        string Title { get; set; }
        Guid? ParentId { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        string GetTitle();
        string GetId();
        void UpdateTimestamp();
    }

    public interface IParentItem
    {
        List<ITreeItem> Children { get; set; }
        void AddChild(ITreeItem child);
    }

    public class Chapter : TreeItem, IParentItem
    {
        [JsonPropertyName("sections")]
        public List<Section> Sections { get; set; } = new List<Section>();

        [JsonPropertyName("children")]
        public List<ITreeItem> Children
        {
            get => Sections.Cast<ITreeItem>().ToList();
            set
            {
                Sections = value.OfType<Section>().ToList();
            }
        }

        public Chapter()
        {
            ParentId = null;
        }

        public void AddChild(ITreeItem child)
        {
            if (child is Section section)
            {
                section.ParentId = this.Id;
                Sections.Add(section);
                UpdateTimestamp();
            }
            else
            {
                throw new ArgumentException("Only Section items can be added to a Chapter.");
            }
        }

        public void AddSection(Section section)
        {
            section.ParentId = this.Id;
            Sections.Add(section);
            UpdateTimestamp();
        }
    }

    public class Section : TreeItem, IParentItem
    {
        [JsonPropertyName("subSections")]
        public List<SubSection> SubSections { get; set; } = new List<SubSection>();

        [JsonPropertyName("children")]
        public List<ITreeItem> Children
        {
            get => SubSections.Cast<ITreeItem>().ToList();
            set
            {
                SubSections = value.OfType<SubSection>().ToList();
            }
        }

        public void AddChild(ITreeItem child)
        {
            if (child is SubSection subSection)
            {
                subSection.ParentId = this.Id;
                SubSections.Add(subSection);
                UpdateTimestamp();
            }
            else
            {
                throw new ArgumentException("Only SubSection items can be added to a Section.");
            }
        }

        public void AddSubSection(SubSection subSection)
        {
            subSection.ParentId = this.Id;
            SubSections.Add(subSection);
            UpdateTimestamp();
        }

        public string GetContent()
        {
            StringBuilder contentBuilder = new StringBuilder();
            foreach (var subSection in SubSections)
            {
                contentBuilder.AppendLine(subSection.Content);
            }
            return contentBuilder.ToString();
        }
    }

    public class SubSection : TreeItem
    {
        [JsonPropertyName("content")]
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("contentFinal")]
        public string ContentFinal { get; set; } = string.Empty;

        public string GetContent() => Content;

        public string GetContentFinal() => ContentFinal;
    }

    /// <summary>
    /// Represents a tree view item for FluentTreeView, wrapping around existing TreeItem models.
    /// </summary>
    public class OntologyTreeViewItem : FluentTreeItem, ITreeViewItem
    {
        // Required by ITreeViewItem
        public string Text { get; set; }
        public string Id { get; set; }
        public IEnumerable<ITreeViewItem> Items { get; set; } = new List<ITreeViewItem>();
        public Icon IconCollapsed { get; set; } = new Icons.Regular.Size20.ChevronRight();
        public Icon IconExpanded { get; set; } = new Icons.Regular.Size20.ChevronDown();
        public bool Disabled { get; set; } = false;
        public bool Expanded { get; set; } = false;
        public Func<TreeViewItemExpandedEventArgs, Task> OnExpandedAsync { get; set; } = (args) => Task.CompletedTask;

        /// <summary>
        /// Holds a reference to the original TreeItem (Chapter, Section, SubSection).
        /// </summary>
        public ITreeItem? OriginalItem { get; set; }

        /// <summary>
        /// Constructs an OntologyTreeViewItem from a TreeItem.
        /// </summary>
        public OntologyTreeViewItem(ITreeItem treeItem)
        {
            Text = treeItem.GetTitle();
            Id = treeItem.GetId();
            OriginalItem = treeItem;

            if (treeItem is IParentItem parent)
            {
                Items = parent.Children.Select(child => new OntologyTreeViewItem(child));
            }
        }
    }
}
