using DAL.DataContext;
using DAL.Repositories.IRepositories;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GroupProfileRepository : IGroupProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public GroupProfile CreateGroupProfile(GroupProfile groupProfile)
        {
            _context.GroupProfiles.Add(groupProfile);
            if (Save())
            {
                return groupProfile;
            }
            else
            {
                return new GroupProfile();
            }
        }

        public bool DeleteGroupProfile(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if(groupProfile != null)
            {
                _context.GroupProfiles.Remove(groupProfile);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool DoProfileExist(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (groupProfile != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Guid> GetManagersIds(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (groupProfile != null)
            {
                return (IEnumerable<Guid>)groupProfile.ManagersId;
            }
            else
            {
                return new List<Guid>();
            }
        }

        public IEnumerable<Guid> GetMembersIds(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (groupProfile != null)
            {
                return (IEnumerable<Guid>)groupProfile.MembersId;
            }
            else
            {
                return new List<Guid>();
            }
        }

        public GroupProfile GetProfileById(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id);
        }

        public string GetProfileDescription(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).Description;
        }

        public string GetProfilePhoto(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ProfilePhoto;
        }

        public int GetProfileViews(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ProfileViews;
        }

        public string GetVideoLink(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).VideoLink;
        }

        public bool IsManager(Guid id, Guid userId)
        {
            var managersList = _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId;

            foreach(var manager in managersList)
            {
                if(manager.UserProfileId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsMember(Guid id, Guid userId)
        {
            var membersList = _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId;

            foreach (var member in membersList)
            {
                if (member.ProfileId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SignInFollower(Guid id, Guid profileId)
        {
            var followerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);

            if(followerProfile != null)
            {
                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Add(followerProfile);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignInManager(Guid id, Guid profileId)
        {
            var managerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if(managerProfile != null && groupProfile != null)
            {
                GroupProfileManagers groupProfileManager = new GroupProfileManagers()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = managerProfile,
                    UserProfileId = managerProfile.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId.Add(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignInMember(Guid id, Guid profileId)
        {
            var memberProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberProfile != null && groupProfile != null)
            {
                GroupProfileUserProfile groupProfileManager = new GroupProfileUserProfile()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = memberProfile,
                    ProfileId = memberProfile.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId.Add(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutFollower(Guid id, Guid profileId)
        {
            var followerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);

            if (followerProfile != null)
            {
                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Remove(followerProfile);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutManager(Guid id, Guid profileId)
        {
            var managerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (managerProfile != null && groupProfile != null)
            {
                GroupProfileManagers groupProfileManager = new GroupProfileManagers()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = managerProfile,
                    UserProfileId = managerProfile.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId.Remove(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutMember(Guid id, Guid profileId)
        {
            var memberProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberProfile != null && groupProfile != null)
            {
                GroupProfileUserProfile groupProfileManager = new GroupProfileUserProfile()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = memberProfile,
                    ProfileId = memberProfile.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId.Remove(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public GroupProfile UpdateGroupProfile(GroupProfile groupProfile)
        {
            var groupProfileDb = _context.GroupProfiles.FirstOrDefault(g => g.Id == groupProfile.Id);
            if(groupProfileDb != null)
            {
                groupProfileDb = groupProfile;
                return groupProfile;
            }
            else
            {
                return groupProfileDb;
            }
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
