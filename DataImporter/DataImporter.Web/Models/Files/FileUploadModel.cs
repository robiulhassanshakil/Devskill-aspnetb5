using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Services;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Models.Files
{
    public class FileUploadModel
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        

        public FileUploadModel()
        {
            _fileService= Startup.AutofacContainer.Resolve<IFileService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            
        }
        public FileUploadModel(IFileService fileService,IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
            
        }

        public async Task FileUpload(IFormFile formFile)
        {
            string fileext = Path.GetExtension(formFile.FileName);
            if (fileext == ".xlsx" || fileext == ".xlsm" || fileext == ".xls" || fileext == ".xlsb")
            {
                var fileName = formFile.FileName;
                var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles"));
                
                using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                

                var file = new ExcelFile()
                {
                    ExcelFileName= formFile.FileName,
                    ExcelFilePath = filePath
                };

                _fileService.FileUploadToDb(file);

            }
        }
    }
}
