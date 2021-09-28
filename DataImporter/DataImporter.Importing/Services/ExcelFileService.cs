using System;
using System.Collections.Generic;
using System.Data;
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

        public void GetExcelDatabase(int groupId)
        {
            var allDatas = _importingUnitOfWork.ExcelDatas.Get(x => x.GroupId == groupId, null, "ExcelFieldData", true);

            var allExcelFieldData = new List<ExcelFieldData>();
            var coloumCounter = 0;
            foreach (var allData in allDatas)
            {
                coloumCounter = 0;
                foreach (var excelFieldData in allData.ExcelFieldData)
                {
                    coloumCounter++;
                    var oneExcelFielData = new ExcelFieldData()
                    {
                        ExcelDataId = excelFieldData.ExcelDataId,
                        Name = excelFieldData.Name,
                        Value = excelFieldData.Value
                    };
                    allExcelFieldData.Add(oneExcelFielData);
                }
            }



            convertExcelDataFieldToDataTable(allExcelFieldData, coloumCounter);

        }
        public static DataTable convertExcelDataFieldToDataTable(List<ExcelFieldData> allExcelFieldData,int coloumCounter)
        {
            var rows = allExcelFieldData.Count / coloumCounter;
            DataTable dataTable = new DataTable();
            bool columnsAdded = false;
            var coloum = 0;
            foreach (var excelFieldData in allExcelFieldData)
            {
                coloum++;
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = excelFieldData.Name;
                dataTable.Columns.Add(dataColumn);
                if (coloum==coloumCounter)
                {
                    break;
                }
            }
            var allvalueList = new List<object>();
            foreach (var excelFieldData in allExcelFieldData)
            {
                allvalueList.Add(excelFieldData.Value);
            }

            var z = 0;
            for (int i = 0; i < rows; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    dataRow[column] = allvalueList[z];
                    z++;
                }

                dataTable.Rows.Add(dataRow);
            }
           
           /* foreach (DataColumn dataTableColumn in dataTable.Columns)
            {
                
                   DataRow dataRow = dataTable.NewRow();

                foreach (var excelFieldData in allExcelFieldData)
                {
                    dataRow[dataTableColumn] = excelFieldData.Value;
                }
                    
                
            }*/
            /*foreach (string row in data.Split('$'))
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (string cell in row.Split('|'))
                {
                    string[] keyValue = cell.Split('~');
                    if (!columnsAdded)
                    {
                        DataColumn dataColumn = new DataColumn(keyValue[0]);
                        dataTable.Columns.Add(dataColumn);
                    }
                    dataRow[keyValue[0]] = keyValue[1];
                }
                columnsAdded = true;
                dataTable.Rows.Add(dataRow);
            }*/
            return dataTable;
        }
    }
}
