using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Domain.Entities;

public class Meeting : IEntity<string>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string? TeacherEmail { get; set; }
    public List<Parent>? Parents { get; set; }
    public string? StudentName { get; set; }
    public DateTime MeetingStart { get; set; }
    public DateTime MeetingEnd { get; set; }
    public bool IsBooked { get; set; } = false;
}