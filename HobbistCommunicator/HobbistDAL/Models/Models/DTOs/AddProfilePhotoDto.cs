using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class AddProfilePhotoDto
    {
        public string PhotoBase64 { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
