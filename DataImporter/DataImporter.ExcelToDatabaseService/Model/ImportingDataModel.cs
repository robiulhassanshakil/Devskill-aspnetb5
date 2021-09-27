using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Common.Utilities;
using DataImporter.Importing.Entities;
using DataImporter.Importing.UniteOfWorks;
using ExcelDataReader;

namespace DataImporter.ExcelToDatabaseService.Model
{
    public class ImportingDataModel : IImportingDataModel
    {
        private readonly IImportingUnitOfWork _importingUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;

        public ImportingDataModel(IImportingUnitOfWork importingUnitOfWork, IDateTimeUtility dateTimeUtility)
        {
            _importingUnitOfWork = importingUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
        }

        public void ImportDatabase()
        {
            var files = _importingUnitOfWork.ExcelFiles.GetAll();

            foreach (var file in files)
            {
                if (String.CompareOrdinal(file.Status.ToLower(), "incomplete") == 0)
                {

                    var filePath = file.ExcelFilePath;

                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {

                        IExcelDataReader excelDataReader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                        var conf = new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = a => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };

                        DataSet dataSet = excelDataReader.AsDataSet(conf);
                        DataTable dataTable = dataSet.Tables[0];
                        DataRowCollection row = dataTable.Rows;
                        DataColumnCollection column = dataTable.Columns;

                        for (int i = 0; i < row.Count; i++)
                        {
                            _importingUnitOfWork.ExcelDatas.Add(new ExcelData()
                            {
                                GroupId = file.GroupId,
                                ImportDate = _dateTimeUtility.Now
                            });
                            _importingUnitOfWork.Save();

                            var exceldataId = _importingUnitOfWork.ExcelDatas.GetAll().Last().Id;

                            for (int j = 0; j < column.Count; j++)
                            {
                                var excelFieldData = new ExcelFieldData()
                                {
                                    Name = dataTable.Columns[j].ColumnName,
                                    Value = dataTable.Rows[i][j].ToString(),
                                    ExcelDataId = exceldataId
                                };
                                _importingUnitOfWork.ExcelFieldDatas.Add(excelFieldData);
                                _importingUnitOfWork.Save();
                            }
                        }
                    }
                    file.Status = "Completed";
                    File.Delete(filePath);
                }
            }
            _importingUnitOfWork.Save();
        }
    }

}
