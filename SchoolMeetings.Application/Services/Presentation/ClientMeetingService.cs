using Amazon.Runtime;
using MongoDB.Bson;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace SchoolMeetings.Application.Services.Presentation;

public class ClientMeetingService(IHttpClientFactory factory) : IMeetingService
{
    private readonly HttpClient _httpClient = factory.CreateClient("SchoolMeetingsApi");
    public Task<ICollection<Meeting>> GetAllAsync()
    {
        //Not implemented because no such endpoint exists as of now
        throw new NotImplementedException();
    }

    public async Task<Meeting?> GetByIdAsync(ObjectId id)
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

        var newlyAddedMeeting = response.Content.ReadFromJsonAsync<Meeting>();

        return meeting;
    }

    public async Task<bool> UpdateAsync(Meeting meeting)
    {
        var response = await _httpClient.PutAsJsonAsync<Meeting>("/meetings", meeting);

        var result = await response.Content.ReadFromJsonAsync<bool>();

        return result;
    }

    public async Task<bool> DeleteAsync(ObjectId id)
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
}