using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class NewUserMessageDto
    {
        public Guid SenderProfileId { get; set; }
        public Guid TargetProfileId { get; set; }
        public string Content { get; set; }
    }
}
