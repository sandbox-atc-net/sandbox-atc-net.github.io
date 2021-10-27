using System;
using System.Text.Json.Serialization;

namespace AtcWeb.Domain.GitHub.Models
{
    public class GitHubPath
    {
        [JsonPropertyName("path")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("git_url")]
        public string RootUrl { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string TreeUrl { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Sha { get; set; } = string.Empty;

        public int Size { get; set; }

        public bool IsDirectory => "dir".Equals(Type, StringComparison.Ordinal) || "tree".Equals(Type, StringComparison.Ordinal);

        public bool IsFile => "file".Equals(Type, StringComparison.Ordinal) || "blob".Equals(Type, StringComparison.Ordinal);

        public override string ToString()
            => $"{nameof(Name)}: {Name}, {nameof(Type)}: {Type}, {nameof(Size)}: {Size}, {nameof(IsDirectory)}: {IsDirectory}";
    }
}