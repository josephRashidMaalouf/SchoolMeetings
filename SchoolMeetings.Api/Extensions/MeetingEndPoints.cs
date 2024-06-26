﻿using MongoDB.Bson;
using SchoolMeetings.Domain.Dtos;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Api.Extensions;

public static class MeetingEndPoints
{
    public static IEndpointRouteBuilder MapMeetingEndPoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("meetings");

        group.MapGet("/unbooked/{teacherEmail}", GetAllUnbookedByTeacherEmailAsync);
        group.MapGet("/booked/{teacherEmail}", GetAllBookedByTeacherEmailAsync).RequireAuthorization();
        group.MapGet("/all/{teacherEmail}", GetAllByTeacherEmailAsync).RequireAuthorization();
        group.MapGet("/all/by-month", GetAllByTeacherEmailAndMonthAsync).RequireAuthorization();
        group.MapGet("/unbooked/by-month", GetUnbookedByTeacherEmailAndMonthAsync);
        group.MapGet("/all/", GetAllByTeacherEmailAndDateAsync).RequireAuthorization();
        group.MapGet("/{id}", GetByIdAsync).RequireAuthorization();

        group.MapPut("/book", BookAsync);
        group.MapPut("/cancel", CancelAsync);
        group.MapPut("/", UpdateAsync).RequireAuthorization();

        group.MapPost("/", AddAsync).RequireAuthorization();

        group.MapDelete("/{id}", DeleteAsync).RequireAuthorization();

        return app;
    }

    #region Get

    public static async Task<IResult> GetAllUnbookedByTeacherEmailAsync(IMeetingService meetingService, string teacherEmail)
    {
        var meetings = await meetingService.GetUnbookedByTeacherEmailAsync(teacherEmail);

        return Results.Ok(meetings);
    }

    public static async Task<IResult> GetAllBookedByTeacherEmailAsync(IMeetingService meetingService, string teacherEmail)
    {
        var meetings = await meetingService.GetBookedByTeacherEmailAsync(teacherEmail);

        return Results.Ok(meetings);
    }

    public static async Task<IResult> GetAllByTeacherEmailAsync(IMeetingService meetingService, string teacherEmail)
    {
        var meetings = await meetingService.GetAllByTeacherEmailAsync(teacherEmail);

        return Results.Ok(meetings);
    }
    public static async Task<IResult> GetAllByTeacherEmailAndDateAsync(IMeetingService meetingService, string teacherEmail, string date)
    {
        var meetings = await meetingService.GetAllByTeacherEmailAndDateAsync(teacherEmail, date);

        return Results.Ok(meetings);
    }

    public static async Task<IResult> GetAllByTeacherEmailAndMonthAsync(IMeetingService meetingService,
        string teacherEmail, string date)
    {
        var meetings = await meetingService.GetAllByTeacherEmailAndMonthAsync(teacherEmail, date);

        return Results.Ok(meetings);
    }

    public static async Task<IResult> GetUnbookedByTeacherEmailAndMonthAsync(IMeetingService meetingService,
        string teacherEmail, string date)
    {
        var meetings = await meetingService.GetUnBookedByTeacherEmailAndMonthAsync(teacherEmail, date);

        return Results.Ok(meetings);
    }

    public static async Task<IResult> GetByIdAsync(IMeetingService meetingService, string id)
    {
        var meeting = await meetingService.GetByIdAsync(id);

        if (meeting is null)
            return Results.NotFound($"Meeting with id: {id} could not be found");

        return Results.Ok(meeting);
    }

    #endregion

    #region Update

    public static async Task<IResult> UpdateAsync(IMeetingService meetingService, Meeting meeting)
    {
        var updateSuccess = await meetingService.UpdateAsync(meeting);

        if (!updateSuccess)
            return Results.UnprocessableEntity("Update failed");

        return Results.Ok(updateSuccess);
    }

    public static async Task<IResult> BookAsync(IMeetingService meetingService, BookMeetingDto meetingDto)
    {
        var meeting = await meetingService.BookMeeting(meetingDto);

        if (meeting is null)
            return Results.UnprocessableEntity("Update failed");

        return Results.Ok(meeting);
    }

    public static async Task<IResult> CancelAsync(IMeetingService meetingService, Meeting meeting)
    {
        var canceledMeeting = await meetingService.CancelMeeting(meeting);

        if (canceledMeeting is null)
            return Results.UnprocessableEntity("Update failed");

        return Results.Ok(canceledMeeting);
    }

    #endregion

    public static async Task<IResult> DeleteAsync(IMeetingService meetingService, string id)
    {
        var deleteSuccess = await meetingService.DeleteAsync(id);

        if (!deleteSuccess)
            return Results.UnprocessableEntity("Deletion failed");

        return Results.Ok(deleteSuccess);
    }


    public static async Task<IResult> AddAsync(IMeetingService meetingService, Meeting meeting)
    {
        var newlyAddedMeeting = await meetingService.AddAsync(meeting);

        return Results.Ok(newlyAddedMeeting);
    }

}