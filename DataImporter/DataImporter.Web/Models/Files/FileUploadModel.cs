using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Exceptions;
using DataImporter.Importing.Services;
using DataImporter.Web.Models.GroupModel;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Models.Files
{
    public class FileUploadModel
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTime;


        public FileUploadModel()
        {
            _fileService= Startup.AutofacContainer.Resolve<IFileService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTime= Startup.AutofacContainer.Resolve<IDateTimeUtility>();

        }
        public FileUploadModel(IFileService fileService,IMapper mapper,IDateTimeUtility dateTime)
        {
            _fileService = fileService;
            _mapper = mapper;
            _dateTime = dateTime;
        }

        public async Task FileUpload(IFormFile file,Group group)
        {
            if (group.Id==0)
            {
                throw new InvalidParameterException("Select Group");
            }
            else
            {
                string fileext = Path.GetExtension(file.FileName);
                if (fileext == ".xlsx" || fileext == ".xlsm" || fileext == ".xls" || fileext == ".xlsb")
                {
                    var fileName = file.FileName;
                    var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles"));

                    using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    var excelFile = new ExcelFile()
                    {
                        ExcelFileName = file.FileName,
                        ExcelFilePath = filePath,
                        DateTime = _dateTime.Now,
                        GroupId = group.Id
                    };

                    _fileService.FileUploadToDb(excelFile);

                }
            }
           
        }
    }
}
