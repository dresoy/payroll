using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Utilities
{
    public static class ExcelManager
    {
        public static async Task<DataSet> ExcelReaderAsync(Stream file)
        {
            //using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var stream = file)
            {

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    //// Choose one of either 1 or 2:

                    //// 1. Use the reader methods
                    //do
                    //{
                    //    while (reader.Read())
                    //    {
                    //        // reader.GetDouble(0);
                    //    }
                    //} while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    DataSet result = await Task.Run(()=> reader.AsDataSet());
                    return result;
                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
    }
}