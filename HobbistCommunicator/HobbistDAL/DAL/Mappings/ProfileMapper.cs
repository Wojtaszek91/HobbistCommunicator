using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Mappings
{
    public static class ProfileMapper
    {
        public static UserProfileDto MapProfileToProfileDto(UserProfile profile)
        {
            return new UserProfileDto()
            {
                Username = profile.Username,
                Description = profile.Description,
                VideoLink = profile.VideoLink,
                ProfilePhoto = profile.ProfilePhoto,
                ProfileViews = profile.ProfileViews,
                UserAccountId = profile.UserAccountId
            };
        }

        public static UserProfile MapProfileDtoToProfile(UserProfileDto profileDto)
        {
        return new UserProfile()
        {
            Username = profileDto.Username,
                Description = profileDto.Description,
                VideoLink = profileDto.VideoLink,
                ProfilePhoto = profileDto.ProfilePhoto,
                ProfileViews = profileDto.ProfileViews,
                UserAccountId = profileDto.UserAccountId
            };
        }
    }
}
