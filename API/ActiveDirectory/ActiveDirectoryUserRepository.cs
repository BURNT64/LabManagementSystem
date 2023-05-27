using System.Security.Claims;
using Microsoft.Graph;

namespace LabManagementSystem.API.ActiveDirectory;

public class ActiveDirectoryUserRepository : IActiveDirectoryUserRepository
{
    private readonly GraphServiceClient _graphServiceClient;
        
    public ActiveDirectoryUserRepository(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }
    
    public async Task<User?> GetUser(string objectId)
    {
        try
        {
            return await _graphServiceClient.Users[objectId].Request().GetAsync();
        }
        catch (ServiceException)
        {
            return null;
        }
    }

    public async Task<User?> GetUserByEmail(string emailAddress)
    {
        var mailUsers = await _graphServiceClient.Users.Request().Filter("mail eq '" + emailAddress + "'").GetAsync();
        var principalNameUsers = await _graphServiceClient.Users.Request().Filter("userprincipalname eq '" + emailAddress + "'").GetAsync();

        if (mailUsers.Count > 0) return mailUsers[0];
        return principalNameUsers.Count > 0 ? principalNameUsers[0] : null;
    }

    public async Task<string?> GetUserProfilePictureBase64(User user)
    {
        Stream? photoStream;
        try
        {
            photoStream = await _graphServiceClient.Users[user.Id].Photo.Content.Request().GetAsync();
        }
        catch (ServiceException)
        {
            return null;
        }
        
        if (photoStream == null) return null;

        MemoryStream memoryStream = new MemoryStream();
        await photoStream.CopyToAsync(memoryStream);
        
        byte[] data = memoryStream.ToArray();
        
        memoryStream.Close();
        await memoryStream.DisposeAsync();
        
        return Convert.ToBase64String(data);
    }

    public static string GetUserEmail(User user)
    {
        return user.Mail ?? user.UserPrincipalName;
    }

    public static string GetUserObjectId(ClaimsPrincipal claimsPrincipal)
    {
        return (from claimsPrincipalClaim in claimsPrincipal.Claims where claimsPrincipalClaim.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier" select claimsPrincipalClaim.Value).FirstOrDefault()!;
    }
}