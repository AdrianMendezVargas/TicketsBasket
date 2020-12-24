using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsBasket.Models.Domain {
    public class JobApplcation : Record {

        public JobApplcation() {
            ApplicationDate = DateTime.UtcNow;
        }

        public string Position { get; set; }

        public string Description { get; set; }

        public string CvUrl { get; set; }

        public JobApplicationstatus Status { get; set; }

        public virtual UserProfile AppliedUser { get; set; }

        [ForeignKey(nameof(AppliedUser))]
        public string AppliedUserId { get; set; }

        public virtual UserProfile Organizer { get; set; }

        [ForeignKey(nameof(Organizer))]
        public string OrganizerId { get; set; }

        public DateTime ApplicationDate { get; set; }

    }

    public enum JobApplicationstatus {
        Pending = 0,
        Reviewng = 1,
        Rejected = 2,
        Accepted = 3
    }
}
