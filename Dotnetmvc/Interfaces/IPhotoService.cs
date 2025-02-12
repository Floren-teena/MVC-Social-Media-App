﻿using CloudinaryDotNet.Actions;

namespace dotnetcoremorningclass.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        //Task<DeletionResult> DeletePhotoAsync(string publicUrl);

    }
}