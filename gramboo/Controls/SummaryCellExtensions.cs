using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gramboo.Controls
{
    /// <summary>
    /// Safe extension methods for SummaryCellCollection
    /// 
    /// These extension methods provide safe access to summary cell values.
    /// </summary>
    public static class SummaryCellExtensions
    {
        // Safe access method - TryGet pattern
        /// <summary>
        /// Try to get a cell by column name.
        /// </summary>
        public static bool TryGetCell(this SummaryCellCollection collection, string columnName, out ReadOnlyTextBox cell)
        {
            if (collection == null)
            {
                cell = null;
                return false;
            }

            cell = collection[columnName];
            return cell != null;
        }

        // Get cell text safely with default value
        /// <summary>
        /// Safely get cell text value with fallback default.
        /// </summary>
        public static string GetCellText(this SummaryCellCollection collection, string columnName, string defaultText = "")
        {
            try
            {
                if (collection == null)
                    return defaultText;

                var cell = collection[columnName];
                return cell?.Text ?? defaultText;
            }
            catch
            {
                return defaultText;
            }
        }

        // Get cell value (Tag) safely with default value
        /// <summary>
        /// Safely get cell value (Tag property) as decimal with fallback default.
        /// </summary>
        public static decimal GetCellValue(this SummaryCellCollection collection, string columnName, decimal defaultValue = 0m)
        {
            try
            {
                if (collection == null)
                    return defaultValue;

                var cell = collection[columnName];
                if (cell != null && cell.Tag is decimal decValue)
                    return decValue;

                if (cell != null && cell.Tag != null)
                {
                    if (decimal.TryParse(cell.Tag.ToString(), out decimal parsed))
                        return parsed;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        // Check if column exists
        /// <summary>
        /// Check if a cell with the given column name exists in the collection.
        /// </summary>
        public static bool ContainsColumn(this SummaryCellCollection collection, string columnName)
        {
            try
            {
                if (collection == null)
                    return false;

                var cell = collection[columnName];
                return cell != null;
            }
            catch
            {
                return false;
            }
        }

        // Get all column names for debugging
        /// <summary>
        /// Get list of all available column names in the collection.
        /// </summary>
        public static List<string> GetColumnNames(this SummaryCellCollection collection)
        {
            var names = new List<string>();

            try
            {
                if (collection == null)
                    return names;

                for (int i = 0; i < collection.Count; i++)
                {
                    var cell = collection[i];
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.DataPropertyName))
                        names.Add(cell.DataPropertyName);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting column names: {ex.Message}");
            }

            return names;
        }

        // Get multiple values at once
        /// <summary>
        /// Get multiple cell values at once.
        /// </summary>
        public static Dictionary<string, string> GetCellTexts(this SummaryCellCollection collection, params string[] columnNames)
        {
            var result = new Dictionary<string, string>();

            try
            {
                if (collection == null || columnNames == null)
                    return result;

                foreach (var columnName in columnNames)
                {
                    result[columnName] = collection.GetCellText(columnName, "");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting cell texts: {ex.Message}");
            }

            return result;
        }

        // Get multiple values at once (numeric)
        /// <summary>
        /// Get multiple cell values as decimals at once.
        /// </summary>
        public static Dictionary<string, decimal> GetCellValues(this SummaryCellCollection collection, params string[] columnNames)
        {
            var result = new Dictionary<string, decimal>();

            try
            {
                if (collection == null || columnNames == null)
                    return result;

                foreach (var columnName in columnNames)
                {
                    result[columnName] = collection.GetCellValue(columnName, 0m);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting cell values: {ex.Message}");
            }

            return result;
        }
    }
}

