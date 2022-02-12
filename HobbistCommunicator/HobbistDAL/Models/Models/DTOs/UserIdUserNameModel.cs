using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class UserIdUserNameModel
    {
        public Guid TargetUserId { get; set; }
        public string UserName { get; set; }

        public UserIdUserNameModel(Guid targetUserId, string userName)
        {
            TargetUserId = targetUserId;
            UserName = userName;
        }
    }
}
