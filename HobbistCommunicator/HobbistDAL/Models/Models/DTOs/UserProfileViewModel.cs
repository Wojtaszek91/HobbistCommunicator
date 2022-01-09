using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class UserProfileViewModel
    {
        public UserProfileDto UserProfle { get; set; }
        public IEnumerable<string> HashTagNames { get; set; }
    }
}
