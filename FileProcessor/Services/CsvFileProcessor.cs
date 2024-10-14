using CsvHelper;
using CsvHelper.Configuration;
using FileProcessor.Models;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace FileProcessor.Services
{
    public class CsvFileProcessor : IFileProcessor
    {
        public async Task<FileProcessingResult> ProcessFileAsync(Stream fileStream, string query = null)
        {
            var result = new FileProcessingResult();
            var dynamicDataList = new List<Dictionary<string, object>>();

            using var reader = new StreamReader(fileStream, Encoding.UTF8);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            // Read the CSV into a list of dictionaries
            var records = csv.GetRecords<dynamic>().ToList();

            foreach (var record in records)
            {
                var dict = (IDictionary<string, object>)record;
                dynamicDataList.Add(dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            }

            // Handle aggregation query like "aggregate=2"
            if (!string.IsNullOrWhiteSpace(query))
            {
                if (query.StartsWith("aggregate=") && int.TryParse(query.Substring("aggregate=".Length), out int colIndex))
                {
                    var errors = ValidateColumn(dynamicDataList, colIndex);
                    if (errors.Any())
                    {
                        result.Errors.AddRange(errors);
                        return result;
                    }

                    var aggregateResult = CalculateColumnAverage(dynamicDataList, colIndex);
                    result.Result = $"The average of column '{colIndex}' is: {aggregateResult}";
                }
                else
                {
                    result.Errors.Add(Constants.Messages.InvalidCsvQuery);
                }
            }
            else
            {
                result.Warnings.Add(Constants.Messages.NoQueryProvided);
            }

            return result;
        }

        // Validation for column existence
        private List<string> ValidateColumn(List<Dictionary<string, object>> dynamicDataList, int colIndex)
        {
            var errors = new List<string>();

            if (dynamicDataList.Count == 0 || colIndex < 0 || colIndex >= dynamicDataList[0].Count)
            {
                errors.Add($"The column index '{colIndex}' does not exist.");
            }

            return errors;
        }

        private double CalculateColumnAverage(List<Dictionary<string, object>> dynamicDataList, int colIndex)
        {
            var numericValues = new List<double>();

            foreach (var dataItem in dynamicDataList)
            {
                var value = dataItem.ElementAt(colIndex).Value;
                if (double.TryParse(value?.ToString(), out double numericValue))
                {
                    numericValues.Add(numericValue);
                }
            }

            if (numericValues.Count == 0)
            {
                return 0;
            }

            double average = numericValues.Average();
            return average;
        }
    }

}
