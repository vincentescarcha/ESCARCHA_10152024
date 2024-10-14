using FileProcessor.Models;
using System.Text.Json;

namespace FileProcessor.Services
{
    public class JsonFileProcessor : IFileProcessor
    {
        public async Task<FileProcessingResult> ProcessFileAsync(Stream fileStream, string query = null)
        {
            var result = new FileProcessingResult();
            List<Dictionary<string, object>> dynamicDataList = await ParseJsonFile(fileStream);
            List<Dictionary<string, object>> filteredData = new();

            // Validate and apply filtering if provided
            if (!string.IsNullOrEmpty(query))
            {
                var filterErrors = ValidateFilter(dynamicDataList, query);
                if (filterErrors.Count > 0)
                {
                    result.Errors.AddRange(filterErrors);
                }
                else
                {
                    filteredData = FilterData(dynamicDataList, query);
                }
            }
            else
            {
                result.Warnings.Add(Constants.Messages.NoQueryProvided);
                filteredData = dynamicDataList;
            }

            result.Message.Add($"Processed JSON file with {dynamicDataList.Count} items.");

            result.Result = JsonSerializer.Serialize(filteredData);

            return result;
        }

        private static async Task<List<Dictionary<string, object>>> ParseJsonFile(Stream fileStream)
        {
            var dynamicDataList = new List<Dictionary<string, object>>();

            using var document = await JsonDocument.ParseAsync(fileStream);
            foreach (var element in document.RootElement.EnumerateArray())
            {
                var dataItem = new Dictionary<string, object>();
                foreach (var property in element.EnumerateObject())
                {
                    dataItem[property.Name] = property.Value.ToString();
                }
                dynamicDataList.Add(dataItem);
            }

            return dynamicDataList;
        }

        private List<string> ValidateFilter(List<Dictionary<string, object>> dynamicDataList, string filter)
        {
            var errors = new List<string>();

            // Split the filter string into key-value pairs
            var filters = filter.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var f in filters)
            {
                var parts = f.Split('=');
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();

                    // Check if the key exists in any dynamic data item
                    if (!dynamicDataList.Any(item => item.Keys.Any(k => string.Equals(k, key, StringComparison.OrdinalIgnoreCase))))
                    {
                        errors.Add($"The filter key '{key}' does not exist in the provided data.");
                    }
                }
            }

            return errors;
        }

        private List<Dictionary<string, object>> FilterData(List<Dictionary<string, object>> dynamicDataList, string filter)
        {
            var filteredData = new List<Dictionary<string, object>>();

            // Split the filter string into key-value pairs
            var filters = filter.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Loop each item in the dynamic data list
            foreach (var dataItem in dynamicDataList)
            {
                bool isMatch = true;

                // Check each filter condition
                foreach (var fragment in filters)
                {
                    var parts = fragment.Split('=');
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();

                        // Retrieve the item key and value and perform case-insensitive comparison
                        var matchingKey = dataItem.Keys.FirstOrDefault(k => string.Equals(k, key, StringComparison.OrdinalIgnoreCase));
                        if (matchingKey == null)
                        {
                            isMatch = false; // Key doesn't exist
                            break;
                        }
                        
                        if (!dataItem.TryGetValue(matchingKey, out var itemValue) ||
                            !string.Equals(itemValue?.ToString(), value, StringComparison.OrdinalIgnoreCase))
                        {
                            isMatch = false; // Value does not match
                            break;
                        }

                    }
                }

                // If all conditions match, add the item to the filtered results
                if (isMatch)
                {
                    filteredData.Add(dataItem);
                }
            }

            return filteredData;
        }

    }
}
