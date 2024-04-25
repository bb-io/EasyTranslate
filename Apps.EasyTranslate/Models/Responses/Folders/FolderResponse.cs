using Apps.EasyTranslate.Models.Dto.Folders;
using Apps.EasyTranslate.Models.Dto.Generic;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Folders;

public class FolderResponse
{
    [Display("Folder ID")]
    public string Id { get; set; }
    
    [Display("Folder name")]
    public string Name { get; set; }
    
    [Display("Folder description")] 
    public string? Description { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    [Display("Total projects")]
    public long TotalProjects { get; set; }

    public FolderResponse(Data<Dictionary<string, string>> dto)
    {
        Id = dto.Id;
        Name = dto.Attributes["name"];
        
        if(dto.Attributes.TryGetValue("description", out string description))
        {
            Description = description;
        }
        
        if(dto.Attributes.TryGetValue("created_at", out string createdAt))
        {
            CreatedAt = DateTime.Parse(createdAt);
        }
        
        if(dto.Attributes.TryGetValue("updated_at", out string updatedAt))
        {
            UpdatedAt = DateTime.Parse(updatedAt);
        }
        
        if(dto.Attributes.TryGetValue("total_projects", out string totalProjects))
        {
            TotalProjects = long.Parse(totalProjects);
        }
    }
}