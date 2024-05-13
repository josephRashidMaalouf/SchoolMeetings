using MongoDB.Bson;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Domain.Entities;

public class Meeting : IEntity<ObjectId>
{
    public ObjectId Id { get; set; }
    public string? TeacherEmail { get; set; }
    public List<Parent>? Parents { get; set; }
    public string? StudentName { get; set; }
    public DateTime MeetingStart { get; set; }
    public DateTime MeetingEnd { get; set; }
    public bool IsBooked { get; set; } = false;
}