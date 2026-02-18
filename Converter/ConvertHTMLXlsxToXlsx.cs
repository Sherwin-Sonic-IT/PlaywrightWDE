
using ClosedXML.Excel;
using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

public static class ConvertHTMLXlsToXlsx
{
    public static void ConverToXlsx(string htmlFilePath, string xlsxFilePath)
    {
        var doc = new HtmlDocument();
        doc.Load(htmlFilePath);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        var rows = doc.DocumentNode.SelectNodes("//tr");
        if (rows == null)
            return;

        int rowNumber = 1;
        foreach (var row in rows)
        {
            var cells = row.SelectNodes("th|td");
            if (cells == null) continue;

            int colNumber = 1;
            foreach (var cell in cells)
            {
                string cellText = HtmlEntity.DeEntitize(cell.InnerText.Trim());

                if (DateTime.TryParseExact(
                        cellText,
                        new[] { "dd.MM.yy", "dd.MM.yyyy" },
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var date))
                {
                    worksheet.Cell(rowNumber, colNumber).Value = date;
                    worksheet.Cell(rowNumber, colNumber).Style.DateFormat.Format = "dd.MM.yyyy";
                }
                else
                {
                    worksheet.Cell(rowNumber, colNumber).Value = cellText;
                }

                colNumber++;
            }
            rowNumber++;
        }

        worksheet.Cell(13, 1).Value = "Distributor";
        worksheet.Cell(13, 2).Value = "";
        worksheet.Cell(13, 3).Value = "Site";
        worksheet.Cell(13, 4).Value = "";
        worksheet.Cell(13, 5).Value = "Sales Route";
        worksheet.Cell(13, 6).Value = "";
        worksheet.Cell(13, 7).Value = "Outlet";
        worksheet.Cell(13, 8).Value = "";

        worksheet.Cell(13, 9).Value = "Calendar Day";
        worksheet.Cell(13, 10).Value = "Invoice Due Date";
        worksheet.Cell(13, 11).Value = "Invoice No";

        worksheet.Cell(13, 16).Value = "ML";
        worksheet.Cell(13, 17).Value = "ML";
        worksheet.Cell(13, 32).Value = "PC";
        worksheet.Cell(13, 38).Value = "%";

        worksheet.Range(13, 1, 13, 2).Merge();
        worksheet.Range(13, 3, 13, 4).Merge();
        worksheet.Range(13, 5, 13, 6).Merge();
        worksheet.Range(13, 7, 13, 8).Merge();

        worksheet.Cell(13, 12).Clear(XLClearOptions.Contents);
        worksheet.Cell(13, 13).Clear(XLClearOptions.Contents);
        worksheet.Cell(13, 28).Clear(XLClearOptions.Contents);
        worksheet.Cell(13, 34).Clear(XLClearOptions.Contents);

        int headerRow = 12;
        int targetStartCol = 12;

        string[] orderedColumns =
        {
            "Sale Quantity in CS",
            "Sale Quantity in PC",
            "Free Quantity in CS",
            "Free Quantity in PC",
            "Sales Quantity in Liter",
            "Free Quantity in Liter",
            "Sale Quantity in Aggr.CS",
            "Sale Quantity in Aggr.PC",
            "Free Quantity in Aggr.CS",
            "Free Quantity in Aggr.PC",
            "GSV",
            "TPR",
            "UW",
            "TPR + UW/DT Discount",
            "Sale After TPR & UW",
            "Volume Discount",
            "Coupon Discount 1.0",
            "NIV",
            "VAT",
            "Total Sales Amt Inc. Tax",
            "Aggr. Pieces",
            "Cabinet Count",
            "Loyalty Discount",
            "UL GSV",
            "UL Price(T)",
            "DT Price(T)",
            "Mark-up/Mark-down Value in %",
            "Coupon Discount 2.0"
        };

        var lastRowUsed = worksheet.LastRowUsed();
        var lastColUsed = worksheet.LastColumnUsed();
        if (lastRowUsed == null || lastColUsed == null)
            return;

        int lastRow = lastRowUsed.RowNumber();

        var sourceCols = new Dictionary<string, int>();
        for (int col = 1; col <= lastColUsed.ColumnNumber(); col++)
        {
            var header = worksheet.Cell(headerRow, col).GetString().Trim();
            if (orderedColumns.Contains(header))
                sourceCols[header] = col;
        }

        foreach (var colIndex in sourceCols.Values)
            worksheet.Cell(headerRow, colIndex).Clear(XLClearOptions.Contents);

        for (int i = 0; i < orderedColumns.Length; i++)
            worksheet.Cell(headerRow, targetStartCol + i).Value = orderedColumns[i];

        var cellB8 = worksheet.Cell(8, 2);
        cellB8.Style.Alignment.WrapText = true;

        worksheet.Column(2).Width = 50;
        worksheet.Row(8).AdjustToContents();

        for (int col = 1; col <= lastColUsed.ColumnNumber(); col++)
        {
            if (col == 2) continue;
            worksheet.Column(col).AdjustToContents();
        }

        worksheet.Columns().Style.Alignment.WrapText = true;

        workbook.SaveAs(xlsxFilePath);
    }
}




