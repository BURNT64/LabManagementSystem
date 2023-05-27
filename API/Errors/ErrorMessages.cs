namespace LabManagementSystem.API.Errors;

public static class ErrorMessages
{
    public static string CouldNotRetrieveFromDatabase(string entityName)
    {
        return "Could not retrieve " + entityName + " from database.";
    }

    public static string CouldNotRetrieveActiveDirectoryUser()
    {
        return "Could not retrieve user from Active Directory.";
    }

    public static string HttpParameterNotProvided(string parameterName)
    {
        return "HTTP parameter \"" + parameterName + "\" was not provided.";
    }

    public static string NoFileToDownload()
    {
        return "No file is available to download.";
    }
}