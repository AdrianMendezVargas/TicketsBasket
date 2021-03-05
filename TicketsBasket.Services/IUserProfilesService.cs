using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Domain;
using TicketsBasket.Models.Mappers;
using TicketsBasket.Repositories;
using TicketsBasket.Services.Storage;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Request;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services {
    public interface IUserProfilesService {
        Task<OperationResponse<UserProfileDetail>> GetProfilebyUserIdAsync();

        Task<OperationResponse<UserProfileDetail>> CreateProfileAsync(CreateProfileRequest model);

        Task<OperationResponse<UserProfileDetail>> UpdateProfilePictureAsync(IFormFile image); 
    }

    public class UserProfilesService : BaseService, IUserProfilesService {

        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _storageService;

        public UserProfilesService(IdentityOptions identity, IUnitOfWork unitOfWork, IStorageService storageService) {
            _identity = identity;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }

        public async Task<OperationResponse<UserProfileDetail>> CreateProfileAsync(CreateProfileRequest model) {
            var user = _identity.User;

            string city = user.FindFirst("city").Value;
            string country = user.FindFirst("country").Value;
            string firstName = user.FindFirst(ClaimTypes.GivenName).Value;
            string lastName = user.FindFirst(ClaimTypes.Surname).Value;
            string fullName = user.FindFirst("name").Value;
            var email = user.FindFirst("emails").Value;

            //TODO: upload the picture to azure Blob storage
            string profilePictureUrl = "Unknown";

            var newUser = new UserProfile {
                City = city ,
                Country = country ,
                FirstName = firstName ,
                LastName = lastName ,
                Email = email ,
                CreatedOn = DateTime.UtcNow ,
                Id = Guid.NewGuid().ToString() ,
                UserId = _identity.UserId ,
                IsOrganizer = model.IsOrganizer ,
                ProfilePicture = profilePictureUrl
            };

            await _unitOfWork.UserProfiles.CreateAsync(newUser);
            await _unitOfWork.CommitChangesAsync();

            return Success("User profile created successfully", newUser.ToUserProfileDetail());
        }

        public async Task<OperationResponse<UserProfileDetail>> GetProfilebyUserIdAsync() {

            var userProfile = await _unitOfWork.UserProfiles.GetByUserIdAsync(_identity.UserId);
            if (userProfile == null) {
                return Error<UserProfileDetail>("Profile not found" , null);
            } else {
                return Success("Profile retrieved successfully" , userProfile.ToUserProfileDetail());
            }
        }

        public async Task<OperationResponse<UserProfileDetail>> UpdateProfilePictureAsync(IFormFile image) {

            var userProfile = await _unitOfWork.UserProfiles.GetByUserIdAsync(_identity.UserId);
            if (userProfile == null) {
                return Error<UserProfileDetail>("Profile not found" , null);
            }

            //Save the new image
            string imageUrl = userProfile.ProfilePicture;
            try {

                imageUrl = await _storageService.SaveBlobAsync("users" , image, BlobType.Image);

                //remove the old blob
                if (userProfile.ProfilePicture != "Unknown") {
                    await _storageService.RemoveBlobAsync("users", userProfile.ProfilePicture);
                }

                if (string.IsNullOrWhiteSpace(imageUrl)) {
                    return Error("Image is required" , userProfile.ToUserProfileDetail());
                }

            } catch (BadImageFormatException) {

                return Error("Invalid image file" , userProfile.ToUserProfileDetail());
            }

            userProfile.ProfilePicture = imageUrl;
            await _unitOfWork.CommitChangesAsync();

            return Success("Profile picture updated!" , userProfile.ToUserProfileDetail());


        }
    }
}
