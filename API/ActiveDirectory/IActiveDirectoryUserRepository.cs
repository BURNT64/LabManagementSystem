using Microsoft.Graph;

namespace LabManagementSystem.API.ActiveDirectory;

public interface IActiveDirectoryUserRepository
{
    public Task<User?> GetUser(string objectId);

    public Task<User?> GetUserByEmail(string emailAddress);

    public Task<string?> GetUserProfilePictureBase64(User user);
}