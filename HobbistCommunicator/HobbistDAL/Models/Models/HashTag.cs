using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class HashTag
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string HashTagName { get; set; }
        public int Popularity { get; set; }
        public ICollection<UserProfileHashTag> UserAccountHashTags { get; set; }
    }
}
