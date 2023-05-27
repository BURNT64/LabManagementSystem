namespace LabManagementSystem.API.Files;

public static class FileSerialisationHelper
{
    public static async Task<byte[]> ConvertFileToBase64(IFormFile file)
    {
        MemoryStream memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        byte[] data = memoryStream.ToArray();
        
        memoryStream.Close();
        await memoryStream.DisposeAsync();

        return data;
    }
}