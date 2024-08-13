using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Helpers
{
    public class S3Utility
    {
        // private  AmazonS3Client s3Client;

        public static void Initialize()
        {
            Console.WriteLine("AWS S3 INITIALIZED");
            // var awsOptions = new AmazonS3Config
            // {
            //     RegionEndpoint = Amazon.RegionEndpoint.USEast1 // Change to your region
            // };

            // s3Client = new AmazonS3Client(
            //     keyConfig.aws.accessKey,
            //     keyConfig.aws.secretAccessKey,
            //     awsOptions
            // );
        }

        public async Task<string> UploadToS3Async(IFormFile file, string key)
        {
            return "uploaded/file/here";
        }

        public async Task DeleteFromS3Async(string key)
        {
            return;
        }

        // public static async Task<string> UploadToS3Async(IFormFile file, string key)
        // {
        //     if (s3Client == null)
        //     {
        //         throw new Exception("S3 is not initialized. Call Initialize() method first.");
        //     }

        //     var putRequest = new PutObjectRequest
        //     {
        //         BucketName = keyConfig.aws.bucketName,
        //         Key = key,
        //         InputStream = file.OpenReadStream(),
        //         ContentType = file.ContentType
        //     };

        //     try
        //     {
        //         var response = await s3Client.PutObjectAsync(putRequest);
        //         return $"{keyConfig.aws.cloudfrontUrl}{key}";
        //     }
        //     catch (AmazonS3Exception ex)
        //     {
        //         throw new Exception($"Error encountered on server. Message:'{ex.Message}' when writing an object");
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Unknown encountered on server. Message:'{ex.Message}' when writing an object");
        //     }
        // }

        // public static async Task DeleteFromS3Async(string key)
        // {
        //     if (s3Client == null)
        //     {
        //         throw new Exception("S3 is not initialized. Call Initialize() method first.");
        //     }

        //     var deleteObjectRequest = new DeleteObjectRequest
        //     {
        //         BucketName = keyConfig.aws.bucketName,
        //         Key = key
        //     };

        //     try
        //     {
        //         var response = await s3Client.DeleteObjectAsync(deleteObjectRequest);
        //     }
        //     catch (AmazonS3Exception ex)
        //     {
        //         throw new Exception($"Error encountered on server. Message:'{ex.Message}' when deleting an object");
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Unknown encountered on server. Message:'{ex.Message}' when deleting an object");
        //     }
        // }
    }
}
