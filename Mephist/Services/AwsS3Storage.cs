using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Mephist.Models;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Operators;

namespace Mephist.Services
{
    public class AwsS3Storage
    {
        private readonly AmazonS3Client _client;
        //private readonly S3Bucket _bucket;
        private readonly string _bucketName = "mephist";
        public AwsS3Storage()
        {
            string accessKey = "AKIA3ROLEMG5OVT44U5K";
            string secretKey = "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";

            BasicAWSCredentials awsCreds = new BasicAWSCredentials(accessKey, secretKey);

            AmazonS3Config s3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.EUNorth1
            };

            _client = new AmazonS3Client(awsCreds, s3Config);
            //_bucket = ( _client.ListBucketsAsync().Result).Buckets.Single(b => b.BucketName == _bucketName);
        }

        public async Task AddItem(Stream obj, string key, string contentType)
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = obj,
                BucketName = _bucketName,
                Key = key,
                ContentType = contentType
            };
            await _client.PutObjectAsync(request);
        }

        public async Task AddItem(byte[] data, string key, string contentType)
        {
            await AddItem(new MemoryStream(data), key, contentType);
        }

        public async Task DeleteItem(string key)
        {
            var deleteObjectRequest = new DeleteObjectRequest()
            {
                Key = key,
                BucketName = _bucketName,
            };
            await _client.DeleteObjectAsync(deleteObjectRequest);
        }

        public async Task<Stream> GetItem(string key)
        {
            var request = new GetObjectRequest()
            {
                BucketName = _bucketName,
                Key = key
            };
            var response = await _client.GetObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
                return response.ResponseStream;
            
            throw new BadHttpRequestException("Error occurred", (int)response.HttpStatusCode);
            
        }

        public string GetPreSignedUrl(Media obj)
        {
            
            var preSignedUrlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = _bucketName,
                Key = obj.Key,
                Expires = DateTime.Now.AddMinutes(15)
            };
            return _client.GetPreSignedURL(preSignedUrlRequest);
        }
    }
}
