using System.ComponentModel.DataAnnotations;
using SchoolMeetings.Presentation.Models.People;

namespace SchoolMeetings.Presentation.Models.Calendar;

public class BookMeetingModel
{
    [Required]
    public string ParentName1 { get; set; } = string.Empty;
    [EmailAddress]
    public string? ParentEmail1 { get; set; }
    [Phone]
    public string? ParentPhone1 { get; set; }
    
    public string? ParentName2 { get; set; }
    [EmailAddress]
    public string? ParentEmail2 { get; set; }
    [Phone]
    public string? ParentPhone2 { get; set; }


    public string? NameOfStudent { get; set; }

}