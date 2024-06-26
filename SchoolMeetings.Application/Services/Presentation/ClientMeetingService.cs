﻿using Amazon.Runtime;
using MongoDB.Bson;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using DnsClient.Protocol;
using System.Text;
using System.Text.Json;
using MongoDB.Bson.IO;
using SchoolMeetings.Domain.Dtos;
using MongoDB.Driver;

namespace SchoolMeetings.Application.Services.Presentation;

public class ClientMeetingService(IHttpClientFactory factory) : IMeetingService
{
    private readonly HttpClient _httpClient = factory.CreateClient("SchoolMeetingsApi");
    public Task<ICollection<Meeting>> GetAllAsync()
    {
        //Not implemented because no such endpoint exists as of now
        throw new NotImplementedException();
    }

    public async Task<Meeting?> GetByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"meetings/{id}");

        if (response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<Meeting>();

        return result;

    }

    public async Task<Meeting?> AddAsync(Meeting meeting)
    {
        var response = await _httpClient.PostAsJsonAsync<Meeting>("/meetings", meeting);

        if (response.IsSuccessStatusCode is false)
            return null;

        var newlyAddedMeeting = await response.Content.ReadFromJsonAsync<Meeting>();

        return newlyAddedMeeting;
    }

    public async Task<bool> UpdateAsync(Meeting meeting)
    {
        var response = await _httpClient.PutAsJsonAsync<Meeting>("/meetings", meeting);

        var result = await response.Content.ReadFromJsonAsync<bool>();

        return result;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"/meetings/{id}");

        var result = await response.Content.ReadFromJsonAsync<bool>();

        return result;
    }

    public async Task<ICollection<Meeting>?> GetAllByTeacherEmailAsync(string teacherEmail)
    {
        var response = await _httpClient.GetAsync($"/meetings/all/{teacherEmail}");

        if (response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<List<Meeting>>();

        return result;
    }

    public async Task<ICollection<Meeting>?> GetAllByTeacherEmailAndDateAsync(string teacherEmail, string date)
    {
        var response = await _httpClient.GetAsync($"/meetings/all?teacherEmail={teacherEmail}&date={date}");

        if (response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<List<Meeting>>();

        return result;
    }

    public async Task<ICollection<Meeting>?> GetAllByTeacherEmailAndMonthAsync(string teacherEmail, string date)
    {
        var response = await _httpClient.GetAsync($"/meetings/all/by-month?teacherEmail={teacherEmail}&date={date}");

        if (response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<List<Meeting>>();

        return result;
    }

    public async Task<ICollection<Meeting>?> GetUnBookedByTeacherEmailAndMonthAsync(string teacherEmail, string date)
    {
        var response = await _httpClient.GetAsync($"/meetings/unbooked/by-month?teacherEmail={teacherEmail}&date={date}");

        if (response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<List<Meeting>>();

        return result;
    }

    public async Task<ICollection<Meeting>?> GetUnbookedByTeacherEmailAsync(string teacherEmail)
    {
        var response = await _httpClient.GetAsync($"/meetings/unbooked/{teacherEmail}");

        if(response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<List<Meeting>>();

        return result;
    }

    public async Task<ICollection<Meeting>?> GetBookedByTeacherEmailAsync(string teacherEmail)
    {
        var response = await _httpClient.GetAsync($"meetings/booked/{teacherEmail}");

        if(response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<List<Meeting>>();

        return result;
    }

    public async Task<Meeting?> CancelMeeting(Meeting meeting)
    {
        var response = await _httpClient.PutAsJsonAsync<Meeting>("/meetings/cancel", meeting);

        if(response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<Meeting>();
        
        return result;
    }

    public async Task<Meeting?> BookMeeting(BookMeetingDto meetingDto)
    {
        var response = await _httpClient.PutAsJsonAsync<BookMeetingDto>("/meetings/book", meetingDto);

        if (response.IsSuccessStatusCode is false)
            return null;

        var result = await response.Content.ReadFromJsonAsync<Meeting>();

        return result;

    }
}