using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Domain.Entities;

public class Parent 
{
    
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

}