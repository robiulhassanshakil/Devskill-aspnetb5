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
            var filesStatus = _importingUnitOfWork.ExcelFiles.GetAll();

            foreach (var file in filesStatus)
            {
                 
                if (String.Compare(file.Status.ToLower(), "incomplete") == 0)
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
                        DataRowCollection row = dataSet.Tables["Sheet1"].Rows;
                        DataColumnCollection column = dataSet.Tables["Sheet1"].Columns;

                        for (int i = 0; i < row.Count; i++)
                        {
                            List<ExcelFieldData> excelFieldDatas=new List<ExcelFieldData>();
                            for (int j = 0; j < column.Count; j++)
                            {
                                var eniFieldData = new ExcelFieldData()
                                {
                                    Name = dataSet.Tables[0].Columns[j].ColumnName,
                                    Value = dataSet.Tables[0].Rows[i][j].ToString(),
                                };

                                excelFieldDatas.Add(eniFieldData);


                                //
                            }
                            _importingUnitOfWork.ExcelDatas.Add(new ExcelData()
                            {
                                CreateDate = _dateTimeUtility.Now,
                                GroupId = file.GroupId,
                                ExcelFieldDatas= excelFieldDatas

                            });

                            
                        }
                        _importingUnitOfWork.Save();
                    }
                }
                

                
            }
        }
    }
}
