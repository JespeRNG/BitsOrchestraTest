using CsvHelper;
using System.IO;
using System.Linq;
using System.Globalization;
using CsvHelper.Configuration;
using System.Collections.Generic;
using BitsOrchestraTest.Contracts.DTO;

namespace BitsOrchestraTest.Core.Helpers
{
    public static class CsvProcessingHelper
    {
        public static List<ContactCreateDto> ProcessCsvFile(Stream csvFileStream)
        {
            using (var reader = new StreamReader(csvFileStream))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                };

                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<ContactCreateDto>().ToList();
                    return records;
                }
            }
        }
    }
}
