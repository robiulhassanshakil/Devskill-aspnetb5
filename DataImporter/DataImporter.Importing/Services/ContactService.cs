using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Importing.UniteOfWorks;

namespace DataImporter.Importing.Services
{
    public class ContactService : IContactService
    {
        private readonly IImportingUnitOfWork _importingUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;

        public ContactService(IImportingUnitOfWork importingUnitOfWork, IDateTimeUtility dateTimeUtility, IMapper mapper)
        {
            _importingUnitOfWork = importingUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public void LoadAllData()
        {
            var p = _importingUnitOfWork.ExcelFiles.Get(null, null, "Group", true);
            
        }
    }
}
