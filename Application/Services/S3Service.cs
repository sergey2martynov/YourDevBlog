using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Dtos.Options;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class S3Service : IS3Service
    {
        private readonly string _bucketName;
        private readonly string _accessKey;
        private readonly string _secretKey;

        public S3Service(IOptions<S3Options> options) 
        {
            _bucketName = options.Value.BucketName;
            _accessKey = options.Value.AccessKey;
            _secretKey = options.Value.SecretKey;
        }

        public async Task<string> UploadMediaToS3(IFormFile file)
        {
            var s3Client = GetS3Client();
            string key = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                var response = await s3Client.PutObjectAsync(request);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return $"https://{_bucketName}.s3.timeweb.cloud/{key}";
                }
                else
                {
                    throw new AmazonS3Exception("Error occurred while uploading file to Amazon S3. HTTP status code: "
                        + response.HttpStatusCode);
                }
            }
        }

        public async Task DeleteMediaFromS3(IEnumerable<string> fileUrls)
        {
            var keys = new List<KeyVersion>();

            foreach(var fileUrl in fileUrls) 
            {
                Uri uri = new Uri(fileUrl);
                string key = uri.PathAndQuery.TrimStart('/');
                keys.Add(new KeyVersion { Key = key });
            }            

            var s3Client = GetS3Client();

            var request = new DeleteObjectsRequest
            {
                BucketName = _bucketName,
                Objects = keys
            };

            var response = await s3Client.DeleteObjectsAsync(request);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AmazonS3Exception($"Error occurred while deleting file from Amazon S3. HTTP status code: {response.HttpStatusCode}");
            }
        }

        private AmazonS3Client GetS3Client()
        {
            var credentials = new BasicAWSCredentials(_accessKey, _secretKey);
            var config = new AmazonS3Config
            {
                ServiceURL = "https://s3.timeweb.cloud"
            };

            return new AmazonS3Client(credentials, config);
        }
    }
}
