using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder= "img";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            //"~/img/user.Picture"
             _wwwroot = _env.WebRootPath; //wwwroot'un dosya yolunu dinamik olarak verir.
        }

        public async Task<IDataResult<UploadedImageDto>> UploadeUserImage(string userName, IFormFile pictureFile, string folderName="userImages")
        {
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))//ilgili klasör var mı?
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);//sonundaki uzantı olmadan almamızı sağlar. ->dogustuluk
            string fileExtension = Path.GetExtension(pictureFile.FileName); //dosyanın uzantısını almamızı sağlar -> .png / .jpeg
            DateTime dateTime = DateTime.Now;
            string newFileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}"; // ---->DoğuşTuluk_587_5_38_12_3_10_2020.png
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return new DataResult<UploadedImageDto>(ResultStatus.Error,$"{userName} adlı kullanıcının resmi başarıyla yüklenmiştir.",new UploadedImageDto
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });
        }
    }
}
