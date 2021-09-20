using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataImporter.ExcelToDatabaseService.Model;

namespace DataImporter.ExcelToDatabaseService
{
    public class Worker : BackgroundService
    {   
        
        private readonly ILogger<Worker> _logger;
        private readonly IImportingDataModel _importingDataModel;

        public Worker(ILogger<Worker> logger,IImportingDataModel importingDataModel)
        {
            _logger = logger;
            _importingDataModel = importingDataModel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _importingDataModel.ImportDatabase();
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
