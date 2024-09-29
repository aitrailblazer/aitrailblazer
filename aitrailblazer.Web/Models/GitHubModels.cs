public class GitHubRepo
{
    public string name { get; set; }
    public string full_name { get; set; }
    public bool @private { get; set; }
    public string html_url { get; set; }
    public string description { get; set; }
    public string default_branch { get; set; } // Add this line
}


public class GitTree
{
    public string sha { get; set; }
    public string url { get; set; }
    public List<GitTreeItem> tree { get; set; }
    public bool truncated { get; set; }
}

public class GitTreeItem
{
    public string path { get; set; }
    public string mode { get; set; }
    public string type { get; set; }
    public string sha { get; set; }
    public long size { get; set; }
    public string url { get; set; }
}
// Define the GitContent class
public class GitContent
{
    public string Type { get; set; }  // "file" or "dir"
    public string Path { get; set; }  // The path of the file or directory
    public string Sha { get; set; }   // The SHA identifier
    public string Url { get; set; }   // The URL to access the content
}
public class GitHubIssue
{
    public int Number { get; set; }
    public string State { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    // Updated properties
    public List<User> Assignees { get; set; } = new List<User>(); // List of User objects for assignees
    public int? Milestone { get; set; } // Optional, milestone number
    public List<string> Labels { get; set; } = new List<string>(); // Optional, list of label names
}




public class User
{
    public string Login { get; set; } // GitHub username of the user
    public int Id { get; set; }
    public string AvatarUrl { get; set; } // Optional, the avatar URL for the user
    // Add other fields as necessary
}

public class SimplePerson
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class GitHubIssueUpdate
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string State { get; set; } // open or closed
    public string StateReason { get; set; } // e.g., completed, not_planned, reopened
    public int? Milestone { get; set; }
    public List<string> Labels { get; set; }
    public List<string> Assignees { get; set; }
}
public class GitHubFileUpdate
{
    public string Path { get; set; } // The path to the file in the repository.
    public string Content { get; set; } // The new file content, in Base64 encoding.
    public string CommitMessage { get; set; } // The commit message for this file update.
    public string Sha { get; set; } // Required if you are updating a file. The blob SHA of the file being replaced.
    public string Branch { get; set; } = "main"; // The branch name. Default to the repository’s default branch.
}