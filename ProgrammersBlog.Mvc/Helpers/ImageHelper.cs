using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Helpers
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            //"~/img/user.Picture"
             _wwwroot = _env.WebRootPath; //wwwroot'un dosya yolunu dinamik olarak verir.
        }

        public async Task<IDataResult<UploadedImageDto>> UploadeUserImage(string userName, IFormFile pictureFile, string folderName)
        {
            
            //string fileName2 = Path.GetFileNameWithoutExtension(picture.FileName);//sonundaki uzantı olmadan almamızı sağlar. ->dogustuluk
            string fileExtension = Path.GetExtension(pictureFile.FileName); //dosyanın uzantısını almamızı sağlar -> .png / .jpeg
            DateTime dateTime = DateTime.Now;
            string fileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}"; // ---->DoğuşTuluk_587_5_38_12_3_10_2020.png
            var path = Path.Combine($"{_wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return fileName; //DoğuşTuluk_587_5_38_12_3_10_2020.png - "~/img/user.Picture"
        }
    }
}
