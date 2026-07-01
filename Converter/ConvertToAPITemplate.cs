using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

public static class ConvertToAPITemplate
{
    public static string ConvertToApiTemplate(string isrFilePath, string outputFolder)
    {
        using var workbook = new XLWorkbook(isrFilePath);
        var sourceSheet = workbook.Worksheet(1);

        var apiWorkbook = new XLWorkbook();
        var apiSheet = apiWorkbook.AddWorksheet("PJP");

        string[] apiHeaders =
        {
            "Route Code",
            "Outlet Code",
            "Frequency Code",
            "Visit Sequence",
            "Week 1",
            "Week 2",
            "Week 3",
            "Week 4",
            "Week 5",
            "SUNDAY",
            "MONDAY",
            "TUESDAY",
            "WEDNESDAY",
            "THURSDAY",
            "FRIDAY",
            "SATURDAY"
        };

        for (int i = 0; i < apiHeaders.Length; i++)
            apiSheet.Cell(1, i + 1).Value = apiHeaders[i];

        var headerRow = sourceSheet.Row(1);

        var columnMap = headerRow.CellsUsed()
            .ToDictionary(
                c => c.GetString().Trim(),
                c => c.Address.ColumnNumber,
                StringComparer.OrdinalIgnoreCase);

        int targetRow = 2;

        foreach (var row in sourceSheet.RowsUsed().Skip(1))
        {
            if (!columnMap.TryGetValue("Visit Day", out int visitDayCol))
                continue;

            if (!int.TryParse(row.Cell(visitDayCol).GetString(), out int visitDay))
                continue;

            int dayIndex = (visitDay - 1) % 6;

            apiSheet.Cell(targetRow, 1).Value = row.Cell(3).GetString().Trim();
            apiSheet.Cell(targetRow, 2).Value = Get(row, columnMap, "Outlet");
            apiSheet.Cell(targetRow, 3).Value = Get(row, columnMap, "Frequency");
            apiSheet.Cell(targetRow, 4).Value = Get(row, columnMap, "Visit Sequence");

            for (int i = 1; i <= 5; i++)
                apiSheet.Cell(targetRow, 4 + i).Value = "";

            apiSheet.Cell(targetRow, 10).Value = "";

            for (int i = 0; i < 6; i++)
            {
                int colIndex = 11 + i;

                apiSheet.Cell(targetRow, colIndex).Value =
                    (i == dayIndex) ? "Y" : "";
            }

            targetRow++;
        }

        string outputPath = Path.Combine(
            outputFolder,
            $"PjpPlanUploaderTemplate.xlsx");

        apiWorkbook.SaveAs(outputPath);
        return outputPath;
    }

    private static string Get(IXLRow row, Dictionary<string, int> map, string column)
    {
        if (!map.TryGetValue(column, out int col))
            return "";

        return row.Cell(col).GetString().Trim();
    }
}

