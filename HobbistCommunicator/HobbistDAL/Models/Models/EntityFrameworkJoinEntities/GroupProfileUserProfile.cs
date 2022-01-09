using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities
{
    public class GroupProfileUserProfile
    {
        public Guid GroupProfileId { get; set; }
        public GroupProfile GroupProfile { get; set; }
        public Guid ProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
