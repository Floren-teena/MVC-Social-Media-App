﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotnetcoremorningclass.Helpers;
using dotnetcoremorningclass.Interfaces;
using Microsoft.Extensions.Options;

namespace dotnetcoremorningclass.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            
            _cloudinary = new Cloudinary(acc);

        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
          
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                
                using var stream = file.OpenReadStream();//read in streams

                var uploadparams = new ImageUploadParams()
                { 
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadparams);

            }

            return uploadResult;
        }

        /*public async Task<DeletionResult> DeletePhotoAsync(string publicUrl)
        {
            throw new NotImplementedException();
        }*/
    }
}