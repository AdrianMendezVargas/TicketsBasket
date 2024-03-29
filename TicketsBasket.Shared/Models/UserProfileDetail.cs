﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsBasket.Shared.Models {
    public class UserProfileDetail {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string UserId { get; set; }
        public bool IsOrganizer { get; set; }
        public string CreatedSince { get; set; }
    }
}
