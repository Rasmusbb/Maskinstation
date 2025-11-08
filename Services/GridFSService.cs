using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Net.Http;

namespace Maskinstation.Services
{
    public class GridFSService
    {
        private readonly string _cluster;
        private readonly GridFSBucket _bucket;


        public GridFSService(IConfiguration configuration)
        {
            _cluster = configuration["MongoDB:ConnectionStrings"] ?? throw new Exception("MongoDB ConnectionStrings not set in configuration.");
            MongoClient Client = new(_cluster);
            var database = Client.GetDatabase("MaskinstationDB");
            _bucket = new GridFSBucket(database);
        }


        public async Task<string> UploadImageAsync(IFormFile imageData)
        {
            using var stream = imageData.OpenReadStream();
            GridFSUploadOptions Options = new()
            {
                Metadata = new BsonDocument
                {
                    {"contentType",imageData.ContentType}
                }
            };
            var fileId = await _bucket.UploadFromStreamAsync(imageData.FileName, stream,Options);
            return fileId.ToString();

        }

        public async Task<(MemoryStream,string ContentType)> DownloadImageAsync(string FileId)
        {
            MemoryStream ms = new();
            FilterDefinition<GridFSFileInfo> filter = Builders<GridFSFileInfo>.Filter.Eq("_id", new ObjectId(FileId));
            GridFSFileInfo fileinfo = await _bucket.Find(filter).FirstOrDefaultAsync();
            string contentType = fileinfo.Metadata["contentType"]?.AsString ?? "application/octet-stream";
            await _bucket.DownloadToStreamAsync(new ObjectId(FileId), ms);
            ms.Position = 0;
            return (ms, contentType);
        }

        public async Task<(Stream Stream, string ContentType)> GetVideoAsync(string FileId)
        {
            var Filter = Builders<GridFSFileInfo>.Filter.Eq("_id", new ObjectId(FileId));
            var fileInfo = await _bucket.Find(Filter).FirstOrDefaultAsync();

            if (fileInfo == null)
                throw new KeyNotFoundException($"Video '{FileId}' not found in GridFS.");
            string contentType = fileInfo.Metadata["contentType"]?.AsString ?? "application/octet-stream";
            var stream = await _bucket.OpenDownloadStreamAsync(new ObjectId(FileId));
            return (stream, contentType);
        }



    }
}
