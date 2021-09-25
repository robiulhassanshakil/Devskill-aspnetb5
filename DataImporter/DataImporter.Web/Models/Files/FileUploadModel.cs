using System;
using System.Collections.Generic;
using System.Data;
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
using ExcelDataReader;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Models.Files
{
    public class FileUploadModel
    {
        private readonly IExcelFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTime;

        public DataTable DataTable{ get; set; }
        
        public FileUploadModel()
        {
            _fileService= Startup.AutofacContainer.Resolve<IExcelFileService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTime= Startup.AutofacContainer.Resolve<IDateTimeUtility>();

        }
        public FileUploadModel(IExcelFileService fileService,IMapper mapper,IDateTimeUtility dateTime)
        {
            _fileService = fileService;
            _mapper = mapper;
            _dateTime = dateTime;
        }

        public void FileUpload(IFormFile file,AllGroupForContacts allGroupForContacts)
        {
           
            if (allGroupForContacts.GroupId==0)
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
                    var filePathWithName = Path.Combine(filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create))
                    {
                         file.CopyToAsync(stream);
                    }
                    using (var stream = new FileStream(filePathWithName, FileMode.Open, FileAccess.Read))
                    {

                        IExcelDataReader reader;


                       reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                        //// reader.IsFirstRowAsColumnNames
                        var conf = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };

                       var dataset = reader.AsDataSet(conf);

                        DataTable = dataset.Tables[0];
                        // Now you can get data from each sheet by its index or its "name"
                        /*var singleTable = dataSet.Tables[0];*/



                    }
                     
                    

                    //1. Reading Excel file
                   
                    /*
                                        var excelFile = new ExcelFile()
                                        {
                                            ExcelFileName = file.FileName,
                                            ExcelFilePath = filePathWithName,
                                            Status = "Incomplete",
                                            ImportDate = _dateTime.Now,
                                            GroupId = allGroupForContacts.GroupId
                                        };

                                        _fileService.FileUploadToDb(excelFile);*/



                }
            }

            
        }
    }
}
