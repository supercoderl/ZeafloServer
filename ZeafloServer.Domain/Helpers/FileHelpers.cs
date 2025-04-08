using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Helpers
{
    public sealed class FileHelpers
    {
        public class RowError
        {
            public int RowNumber { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class ParseExcelResult<T>
        {
            public List<T> ValidItems { get; set; } = new();
            public List<RowError> Errors { get; set; } = new();
            public int TotalRows => ValidItems.Count + Errors.Count;

        }
        public ParseExcelResult<T> ParseExcel<T>(
            Stream stream,
            Dictionary<string, string> headerToPropertyMap // header => property name of T
        ) where T : new()
        {
            var result = new ParseExcelResult<T>();

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();

            var headerRow = worksheet.Row(1);
            var headers = new Dictionary<int, string>();

            // Get map: column index => property name
            foreach (var cell in headerRow.CellsUsed())
            {
                var headerText = cell.Value.ToString().Trim();
                if (headerToPropertyMap.TryGetValue(headerText, out var propertyName))
                {
                    headers[cell.Address.ColumnNumber] = propertyName;
                }
            }

            // Iterate through each row from row 2 onwards
            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                try
                {
                    var obj = new T();

                    foreach (var col in headers)
                    {
                        var cell = row.Cell(col.Key);
                        var propertyName = col.Value;
                        var property = typeof(T).GetProperty(propertyName);

                        if (property == null || !property.CanWrite) continue;

                        string cellValue = worksheet.Cell(row.RowNumber(), col.Key).GetValue<string>();

                        if (string.IsNullOrWhiteSpace(cellValue) && property.PropertyType != typeof(string))
                            continue;

                        var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        object? convertedValue;

                        if (targetType.IsEnum)
                            convertedValue = Enum.Parse(targetType, cellValue, ignoreCase: true);
                        else if (targetType == typeof(Guid))
                            convertedValue = Guid.Parse(cellValue);
                        else if (targetType == typeof(bool))
                            convertedValue = cellValue.ToLower() switch
                            {
                                "true" or "1" or "yes" => true,
                                "false" or "0" or "no" => false,
                                _ => throw new InvalidCastException($"Cannot convert '{cellValue}' to bool")
                            };
                        else
                            convertedValue = Convert.ChangeType(cellValue, targetType);

                        property.SetValue(obj, convertedValue);
                    }

                    result.ValidItems.Add(obj);
                }
                catch (Exception ex)
                {
                    result.Errors.Add(new RowError
                    {
                        RowNumber = row.RowNumber(),
                        ErrorMessage = $"Row {row.RowNumber()}: {ex.Message}"
                    });
                }
            }

            return result;
        }
    }
}
