﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketsBasket.Models.Domain {
    public class UserProfile : Record {

        public UserProfile() {
            CreatedOn = DateTime.UtcNow;
        }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string ProfilePicture { get; set; }

        [Required]
        [StringLength(25)]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string Country { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsOrganizer { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual List<Event> Events { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public virtual List<WishListEvent> WishListEvents { get; set; }
        public virtual List<Like> Likes { get; set; }
        public virtual List<JobApplcation> SenApplications { get; set; } //Sent by normal user
        public virtual List<JobApplcation> RecievedApplications { get; set; }

    }
    
}
