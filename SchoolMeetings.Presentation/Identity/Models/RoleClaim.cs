﻿namespace SchoolMeetings.Presentation.Identity.Models
{
	public partial class CookieAuthenticationStateProvider
	{
		public class RoleClaim
		{
			public string? Issuer { get; set; }
			public string? OriginalIssuer { get; set; }
			public string? Type { get; set; }
			public string? Value { get; set; }
			public string? ValueType { get; set; }
		}
	}
}
