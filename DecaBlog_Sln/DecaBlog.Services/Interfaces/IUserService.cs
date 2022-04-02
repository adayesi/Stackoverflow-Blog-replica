using System.Threading.Tasks;
using DecaBlog.Models.DTO;

namespace DecaBlog.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedListDto<UserMinInfoToReturnDto>> GetUsers(int pageNumber, int perPage);
        Task<bool> ActivateUser(string userid);
        Task<ResponseDto<string>> AddInvitee(RegisterInvitedUserDto model, string inviteToken);
        Task<bool> ConfirmEmail(EmailConfirmationDto model);
        Task<(bool, bool)> AddSupportingEmail(string userId, string email);
        Task<(bool, AddressToReturnDto)> UpdateAddress(AddressToUpdateDto model, string userId);
        public Task<bool> DeactivateUser(string UserId);
        Task<UserInfoToReturnDto> UpdateUser(string id, UserToUpdateDto model);
        Task<PhotoToReturnDto> UpdateUserPhoto(PhotoToUploadDto model, string userId);
        Task<bool> RemoveProfilePhoto(string userId);
        Task<RegisterResponseDto> AddDecadev(UserToRegisterDto model);
        Task<PaginatedListDto<InviteesMinInfoToReturnDto>> GetInvitees(int pageNumber, int perPage);
        public Task<(bool, string)> InviteUser(string email, string inviterId);
        Task<(bool, string)> ApproveInvitee(string inviteId);
        Task<PaginatedListDto<UserMinInfoToReturnDto>> SearchUsersByName(string name, int pageNumber, int perPage);
        Task<PaginatedListDto<InviteeSearchToReturnDto>> SearchInviteeByName(string author, int pageNumber, int perPage);
        Task<InviteeSearchToReturnDto> GetInviteeById(string inviteeId);

    }
}
