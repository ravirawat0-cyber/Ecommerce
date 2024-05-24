using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName = "ecommerce-55ed9.appspot.com";

    // Service account JSON content
    private readonly string serviceAccountJson = @"
    {
      ""type"": ""service_account"",
      ""project_id"": ""ecommerce-55ed9"",
      ""private_key_id"": ""ecfdd47ad4a82856fd0c0cee0704b11bc549f9cb"",
      ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDWkR5sOdteM7AK\nqf7lGKovXYHJq6Z/5A07pRUSRuy6OSBrAqOZ9+mBu4a5Fq3A8NyosO5rAIHUi4em\nOJAqigO2eI9ZSjn+aDVbvQp0ojjdfdgDLJ0EC38LCbJaBpF9l9spHbQqkgBrtg+1\nCUd36ipBbTEcYrfW1qZ4jRfnSuwJMEf+gxmTmsKrHbdzClYKl6wb8m11eeBJf/7o\n8PpN6tUlQzRGPqjGbi7B1VXf+zXNwxBFOi5+xNrrarq6xxm2p4zg2xOXujBXo2FZ\nhgMer4q8q2l6i9tPJ/V89wMELpvKFPWoUmE6/uu9cSrsoKqpAM82r6TGlhtVKrLS\nOedZGy8TAgMBAAECggEAZsvjzWnypTESOCm1isDEWCjuU1Y/UKpLhLXUYItGQvm3\nhE0hYFSbtKHJK8enuvYqYU9TCkSV7uRiOsqClz7EBJnB479iBXabibYJ2lMV91eh\nM/QdzaBMX1dn8SWQcqFcSFiIxpRoSzJQWCV37gVr9SiBDN5mwTDZVIwUVaVZbBSq\n1UAnbqDI/jq1dRXq3scQoWLAF65j+UyXeYf29jpWkD6+Eo1lZuE2Fj+G9jVaWT+z\n4dMGm7aJeu20KWnllkT0rtoXZ2kebSQrgVrplvNcV/yxu6gbL/gqvSBENZLfAm4R\ni5ZLxOlRxQYjzVEAbTii54J65NP0JDHJxPYRiNH0MQKBgQDyj2nv49lYnDDPPACb\n/CZwWyJSYWuok3YOudkQEVCHgjuE7bHKvu/T4VnZl5qjjx9TYOCqxq/s0c2zbbpC\nhwWA0XM3TNrtveU4SJaUD5GaTxG4vl8S1X+XbJoPsmHafcQDV4U6d9FgNKar2LmY\nRYjd+228/sIBoYbdNYrH83wjLwKBgQDidKJxqIN3y/CLyjYyGegQ0FNhKUygXteH\nFfghCs69SNfKFBUcWfwKKauYzjAit7/j81gwrdDCD1BtPeaTpF5mG5Se2kUEB4Pf\n7yTtuy2hzVyO/lUzYb2ss3BcZtOl6I8TVvNIKkbY/Ruh1Pfesbt74wKdkvirKCsy\nfUMimiZJXQKBgD9NzogdHwxRNyverQoItL9+CWcWpdJJKfaN6miXHD1Bb+cxYcVY\nL59MoPZ46pLlN3e7QOdRE3jrCJRbftD+DCGiiD9Wf4yskrYtu57IsWdkW2urw4RW\n1AXhAhQ2qfMr/4wfu1WXD73b/+O5nDZxb1b/15QLqPZkAZ6Z6q5kjXFhAoGAXo2c\nliBqZEa+9V9FKb4va3XQmq0H1Z5EF59Qr1s330je8P1ZozDfT87+WxscdiKK+L7O\nf+TTWXPTfQlvkQ38bF3vvDQexQPuSD4uCSUydHCuyPXg1UdmTwnNTIeSr59evhB8\nHBGq60cjlnq51D4ZriynFuwa8DwhbT9oHLaIbfECgYEAnosKaTDfXvXIasnAsCT6\nIA3wy7qmHlgc9SL7gFzzcucREsKzuYdU4hbNWtZuClXDTHAH7QtOLSTkFkKK9him\nQAEjGHYITYNaimUFNJg7VJX+liMHhprG1BV5ZiSOkq0XuMUK4f8PMFs1464/HMf7\nMjmw8/Q8eyFsf8eiZXdjhVg=\n-----END PRIVATE KEY-----\n"",
      ""client_email"": ""firebase-adminsdk-pfznz@ecommerce-55ed9.iam.gserviceaccount.com"",
      ""client_id"": ""109106354690503073734"",
      ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
      ""token_uri"": ""https://oauth2.googleapis.com/token"",
      ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
      ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-pfznz@ecommerce-55ed9.iam.gserviceaccount.com"",
      ""universe_domain"": ""googleapis.com""
    }";

    public UploadController()
    {
        var credential = GoogleCredential.FromJson(serviceAccountJson);
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