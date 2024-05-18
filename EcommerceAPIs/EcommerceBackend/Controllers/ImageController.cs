using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName = "ecommerce-55ed9.appspot.com";

    public UploadController()
    {
        var credential = GoogleCredential.FromFile("C:\\Users\\ravir\\OneDrive\\Desktop\\DotNet\\Ecommerce\\EcommerceAPIs\\EcommerceBackend\\firebaseKey.json");
        _storageClient = StorageClient.Create(credential);
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            await _storageClient.UploadObjectAsync(
                _bucketName,
                uniqueFileName,
                file.ContentType,
                memoryStream,
                new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead }
            );

            var imageUrl = $"https://storage.googleapis.com/{_bucketName}/{uniqueFileName}";

            return Ok(new { Url = imageUrl });
        }
    }
}
