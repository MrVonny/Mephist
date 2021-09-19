using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Encryption;
using Amazon.S3.Model;
using Mephist.Services;
using Xunit;

namespace Mephist.Tests
{
    public class AwsS3Test
    {
        [Fact]
        public void ConnectToBucketTest()
        {
            string accessKey = "AKIA3ROLEMG5OVT44U5K";
            string secretKey = "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";

            BasicAWSCredentials awsCreds = new BasicAWSCredentials(accessKey, secretKey);

            AmazonS3Config s3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.EUNorth1
            };

            AmazonS3Client s3Client = new AmazonS3Client(awsCreds, s3Config);

            var buckets = s3Client.ListBucketsAsync().Result;

            Assert.True(buckets.Buckets.Count == 1); 
        }

        [Fact]
        public async void CreateAndDeleteTest()
        {
            var storage = new AwsS3Storage();
            var bytes = await File.ReadAllBytesAsync(@"N:\Users\boraf\Desktop\plSrWihcY8.gif");

            await storage.AddItem(bytes, "test", "image/gif");
            await storage.GetItem("test");
            await storage.DeleteItem("test");
        }
    }
}
