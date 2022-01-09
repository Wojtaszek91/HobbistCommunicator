using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class GroupProfile : UserProfile
    {
        public ICollection<GroupProfileManagers> ManagersId { get; set; }
        public ICollection<GroupProfileUserProfile> MembersId { get; set; }
    }
}
