using MongoDB.Bson;
using MongoDB.Driver;
using SchoolMeetings.Domain.Dtos;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;
using SharpCompress.Common;


namespace SchoolMeetings.Application.Services.Api;

public class ApiMeetingService(IMeetingRepository metingRepository) : IMeetingService
{
    private readonly IMeetingRepository _metingRepository = metingRepository;

    public async Task<ICollection<Meeting>> GetAllAsync()
    {
        var meetings = await _metingRepository.GetAllAsync();
        return meetings;
    }

    public async Task<Meeting?> GetByIdAsync(string id)
    {
        var meeting = await _metingRepository.GetByIdAsync(id);
        return meeting;
    }

    public async Task<Meeting?> AddAsync(Meeting entity)
    {
        var newlyAddedMeeting = await _metingRepository.AddAsync(entity);

        return newlyAddedMeeting;
    }

    public async Task<bool> UpdateAsync(Meeting entity)
    {
        var updateSuccess = await _metingRepository.UpdateAsync(entity, entity.Id);

        return updateSuccess;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var deleteSuccess = await _metingRepository.DeleteAsync(id);

        return deleteSuccess;
    }

    public async Task<ICollection<Meeting>?> GetAllByTeacherEmailAsync(string teacherEmail)
    {
        var meetings = await _metingRepository.GetAllByTeacherEmailAsync(teacherEmail);

        return meetings;
    }

    public async Task<ICollection<Meeting>?> GetAllByTeacherEmailAndDateAsync(string teacherEmail, string date)
    {
        var meetings = await _metingRepository.GetAllByTeacherEmailAndDateAsync(teacherEmail, date);

        return meetings;
    }

    public async Task<ICollection<Meeting>?> GetAllByTeacherEmailAndMonthAsync(string teacherEmail, string date)
    {
        var meetings = await _metingRepository.GetAllByTeacherEmailAndMonthAsync(teacherEmail, date);

        return meetings;

    }

    public async Task<ICollection<Meeting>?> GetUnbookedByTeacherEmailAsync(string teacherEmail)
    {
        var meetings = await _metingRepository.GetUnbookedByTeacherEmailAsync(teacherEmail);

        return meetings;
    }

    public async Task<ICollection<Meeting>?> GetBookedByTeacherEmailAsync(string teacherEmail)
    {
        var meetings = await _metingRepository.GetBookedByTeacherEmailAsync(teacherEmail);

        return meetings;
    }

    public async Task<Meeting?> CancelMeeting(Meeting meeting)
    {
        meeting.Parents = [];
        meeting.StudentName = string.Empty;
        meeting.IsBooked = false;

        var updateSuccess = await _metingRepository.UpdateAsync(meeting, meeting.Id);

        if (updateSuccess is false)
        {
            return null;
        }
        return meeting;
    }

    public async Task<Meeting?> BookMeeting(BookMeetingDto meetingDto)
    {
        var parentOne = new Parent()
        {
            Name = meetingDto.ParentName1
        };
        if (string.IsNullOrWhiteSpace(meetingDto.ParentEmail1) is false)
            parentOne.Email = meetingDto.ParentEmail1;
        if (string.IsNullOrWhiteSpace(meetingDto.ParentPhone1) is false)
            parentOne.PhoneNumber = meetingDto.ParentPhone1;

        var parentTwo = new Parent();
        if (string.IsNullOrWhiteSpace(meetingDto.ParentName2) is false)
            parentTwo.Name = meetingDto.ParentName2;
        if (string.IsNullOrWhiteSpace(meetingDto.ParentEmail2) is false)
            parentTwo.Email = meetingDto.ParentEmail2;
        if (string.IsNullOrWhiteSpace(meetingDto.ParentPhone2) is false)
            parentTwo.PhoneNumber = meetingDto.ParentPhone2;

        var meetingToBook = await _metingRepository.GetByIdAsync(meetingDto.MeetingId);

        if (meetingToBook is null)
            return null;

        meetingToBook.Parents ??= [];
        //Add Parent(s)
        meetingToBook.Parents.Add(parentOne);
        if (string.IsNullOrWhiteSpace(parentTwo.Name) is false)
            meetingToBook.Parents.Add(parentTwo);
        //Add student
        meetingToBook.StudentName = meetingDto.NameOfStudent;

        //Mark as booked
        meetingToBook.IsBooked = true;

        var updateSuccess = await _metingRepository.UpdateAsync(meetingToBook, meetingToBook.Id);

        if (updateSuccess is false)
        {
            return null;
        }

        return meetingToBook;
    }
}