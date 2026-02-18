
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;

public static class ExcelReportCleaner
{
    public static List<int> CleanArticleMaster(string filePath)
    {
        using var workbook = new XLWorkbook(filePath);
        var worksheet = workbook.Worksheet(1);

        int rows = worksheet.LastRowUsed()?.RowNumber() ?? 0;
        var deletedRows = new List<int>();

        if (rows >= 1)
        {
            worksheet.Row(rows).Delete();
            deletedRows.Add(rows);
            rows--;
        }

        if (rows >= 2)
        {
            worksheet.Row(2).Delete();
            deletedRows.Add(2);
        }

        rows = worksheet.LastRowUsed()?.RowNumber() ?? 0;

        for (int row = rows; row >= 2; row--)
        {
            var articleCell = worksheet.Cell(row, 1);
            var unitConversion2Cell = worksheet.Cell(row, 6);

            string articleCode = articleCell.GetString().Trim();
            string unitText = unitConversion2Cell.GetString().Trim();

            bool isArticleMissing = string.IsNullOrWhiteSpace(articleCode);

            bool isUnitConversion2Invalid =
                string.IsNullOrWhiteSpace(unitText) ||
                !double.TryParse(unitText, out double unitValue) ||
                unitValue == 0;

            if (isArticleMissing || isUnitConversion2Invalid)
            {
                worksheet.Row(row).Delete();
                deletedRows.Add(row);
            }
        }

        workbook.Save();
        return deletedRows;
    }
}
