using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IGroupProfileRepository
    {
        public GroupProfile GetProfileById(Guid id);
        public string GetProfileDescription(Guid id);
        public string GetVideoLink(Guid id);
        public string GetProfilePhoto(Guid id);
        public int GetProfileViews(Guid id);
        public GroupProfile CreateGroupProfile(GroupProfile groupProfile);
        public bool DeleteGroupProfile(Guid id);
        public GroupProfile UpdateGroupProfile(GroupProfile groupProfile);
        public bool DoProfileExist(Guid id);
        public IEnumerable<Guid> GetManagersIds(Guid id);
        public bool IsManager(Guid id, Guid userId);
        public IEnumerable<Guid> GetMembersIds(Guid id);
        public bool IsMember(Guid id, Guid userId);
        public bool SignInMember(Guid id, Guid userId);
        public bool SignOutMember(Guid id, Guid userId);
        public bool SignInManager(Guid id, Guid userId);
        public bool SignOutManager(Guid id, Guid userId);
        public bool SignInFollower(Guid id, Guid userId);
        public bool SignOutFollower(Guid id, Guid userId);
        public bool Save();
    }
}
