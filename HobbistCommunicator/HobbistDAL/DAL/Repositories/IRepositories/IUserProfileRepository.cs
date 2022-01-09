using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserProfileRepository
    {
        bool CreateProfile(UserProfile userProfile);
        bool UpdateProfile(UserProfileDto profileDto);
        bool UpdateProfilePhotoBase64(string photoBase64, Guid userId);
        bool DoesProfileExist(Guid userId);
        bool DeleteProfile(Guid id);
        UserProfile GetProfileById(Guid id);
        UserProfileDto GetProfileByIdDto(Guid id);
        UserProfileDto GetProfileByUserId(Guid userId);
        bool AddProfileView(Guid id);
        string GetProfileDescription(Guid id);
        string GetProfilePhoto(Guid id);
        int GetProfileViews(Guid id);
        Guid GetUserIdByProfileId(Guid id);
        string GetVideoLink(Guid id);
        bool SignInFollower(Guid id, Guid followerProfileId);
        bool SignOutFollower(Guid id, Guid followerProfileId);
        IEnumerable<HashTag> GetUserHashTags(Guid profileId);
        IEnumerable<string> GetProfileHashTagsNames(Guid profileId);
        bool AddHashTagToProfileAccount(Guid hashTagId, Guid profileId);
        bool AddHashTagByNameToUserProfile(string hashTagName, Guid profileId);
        bool AddProfileHashtagById(Guid profileId, Guid HashTagid);
        bool RemoveHashTagByNameFromUserProfile(string hashTagName, Guid userProfileId);
        bool AddUserGroupByIdToProfile(Guid profileId, Guid groupId);
        IEnumerable<Guid> GetUserGroupsIdListByProfileId(Guid profileId);
        bool IsUserNameAvailable(string username);
        bool UpdateUsername(Guid profileId, string newUsername);
        public bool Save();
    }
}
