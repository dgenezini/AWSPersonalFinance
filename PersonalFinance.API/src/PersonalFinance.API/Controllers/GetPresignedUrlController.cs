using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPresignedUrlController : ControllerBase
    {
        private readonly AmazonS3Client _AmazonS3Client;

        public GetPresignedUrlController(AmazonS3Client amazonS3Client)
        {
            _AmazonS3Client = amazonS3Client;
        }

        [HttpGet]
        public string GetAsync(string bucketName, string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(3)
            };

            return _AmazonS3Client.GetPreSignedURL(request);
        }
    }
}
