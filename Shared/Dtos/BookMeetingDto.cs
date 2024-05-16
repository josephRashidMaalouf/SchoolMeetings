using System.ComponentModel.DataAnnotations;

namespace SchoolMeetings.Domain.Dtos;

public class BookMeetingDto
{
    public string MeetingId { get; set; } = string.Empty;
    public string ParentName1 { get; set; } = string.Empty;
    
    public string? ParentEmail1 { get; set; }
    
    public string? ParentPhone1 { get; set; }

    public string? ParentName2 { get; set; }
    
    public string? ParentEmail2 { get; set; }
    
    public string? ParentPhone2 { get; set; }


    public string? NameOfStudent { get; set; }
}