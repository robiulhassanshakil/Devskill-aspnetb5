using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Exceptions;
using DataImporter.Importing.UniteOfWorks;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Importing.Services
{
    public class ExcelFileService : IExcelFileService
    {
        private readonly IImportingUnitOfWork _importingUnitOfWork;
        private readonly IMapper _mapper;

        public ExcelFileService(IImportingUnitOfWork importingUnitOfWork, IMapper mapper)
        {
            _importingUnitOfWork = importingUnitOfWork;
            _mapper = mapper;
        }

        public  void FileUploadToDb(ExcelFile file)
        {
            if (file == null)
                throw new InvalidParameterException("File was not provided");
            _importingUnitOfWork.ExcelFiles.Add(new Entities.ExcelFile()
            {
                ExcelFileName = file.ExcelFileName,
                ExcelFilePath = file.ExcelFilePath,
                GroupId = file.GroupId,
                ImportDate = file.ImportDate,
                Status = file.Status
            });
            _importingUnitOfWork.Save();
        }
    }
}
