using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities.DTOs
{
    public class UserProfileDto
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string ProfilePhoto { get; set; }
        public int ProfileViews { get; set; }
        public Guid UserAccountId { get; set; }
    }
}
