using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string ProfilePhoto { get; set; }
        public int ProfileViews { get; set; }    
        public ICollection<Post> Posts { get; set; }
        public ICollection<UserProfileHashTag> UserProfileHashTags { get; set; }
        public ICollection<GroupProfileUserProfile> GroupProfiles { get; set; }
        public ICollection<GroupProfileManagers> GroupManagers { get; set; }
        public Guid UserAccountId { get; set; }
        [ForeignKey("UserAccountId")]
        public virtual UserAccount UserAccount { get; set; }
        public ICollection<UserProfile> FollowersId { get; set; }
    }
}
