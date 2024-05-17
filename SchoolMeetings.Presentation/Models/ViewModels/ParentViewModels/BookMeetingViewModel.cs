using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Presentation.Models.ViewModels.ParentViewModels;

public class BookMeetingViewModel(IMeetingService clientMeetingService)
{
    private readonly IMeetingService _clientMeetingService = clientMeetingService;

    public List<Meeting> UnbookedMeetings { get; set; } = [];
}