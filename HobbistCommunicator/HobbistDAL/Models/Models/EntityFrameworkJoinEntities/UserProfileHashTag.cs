using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities
{
    public class UserProfileHashTag
    {
        public Guid HashTagId { get; set; }
        public HashTag HashTag { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
