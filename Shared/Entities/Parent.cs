using MongoDB.Bson;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Domain.Entities;

public class Parent : IEntity<ObjectId>
{
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NameOfChild { get; set; }

}