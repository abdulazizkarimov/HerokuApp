using OpenQA.Selenium;

namespace HerokuApp.Helpers;

public class TableHelper
{
    private readonly IWebElement _table;

    public TableHelper(IWebElement table)
    {
        _table = table;
    }

    public IWebElement FindRowByCellText(string text)
    {
        foreach (var row in GetRows())
        {
            if (row.Text.Contains(text))
                return row;
        }
        return null;
    }

    public string GetValueFromRow(string searchText, string columnName)
    {
        var headers = GetHeaderCells().Select(h => h.Text.Trim()).ToList();

        int columnIndex = headers.FindIndex(h => h.Equals(columnName.Trim(), StringComparison.OrdinalIgnoreCase));

        if (columnIndex == -1)
            throw new NoSuchElementException($"Column '{columnName}' was not found.");

        foreach (var row in GetRows())
        {
            if (row.Text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                var cells = row.FindElements(By.CssSelector("td, th"));

                if (columnIndex < cells.Count)
                    return cells[columnIndex].Text.Trim();

                throw new NoSuchElementException(
                    $"Column index '{columnIndex}' out of range for the matched row.");
            }
        }

        throw new NoSuchElementException($"No row contains the text '{searchText}'.");
    }

    public bool IsColumnSorted(string columnName, bool ascending = true)
    {
        var headers = GetHeaderCells().Select(h => h.Text.Trim()).ToList();

        int columnIndex = headers.FindIndex(h => h.Equals(columnName.Trim(), StringComparison.OrdinalIgnoreCase));

        if (columnIndex == -1)
            throw new NoSuchElementException($"Column '{columnName}' was not found.");

        var columnValues = new List<string>();

        foreach (var row in GetRows().Skip(1))
        {
            var cells = row.FindElements(By.CssSelector("td, th"));

            if (columnIndex < cells.Count)
                columnValues.Add(cells[columnIndex].Text.Trim());
        }

        bool allNumeric = columnValues.All(v => double.TryParse(v, out _));

        if (allNumeric)
        {
            var numericValues = columnValues.Select(double.Parse).ToList();
            var sorted = ascending ? numericValues.OrderBy(x => x) : numericValues.OrderByDescending(x => x);
            return numericValues.SequenceEqual(sorted);
        }
        else
        {
            var sorted = ascending
                ? columnValues.OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                : columnValues.OrderByDescending(x => x, StringComparer.OrdinalIgnoreCase);

            return columnValues.SequenceEqual(sorted);
        }
    }

    public void ClickHeader(string headerName)
    {
        var headers = GetHeaderCells();

        foreach (var header in headers)
        {
            if (header.Text.Trim().Equals(headerName.Trim(), System.StringComparison.OrdinalIgnoreCase))
            {
                header.Click();
                return;
            }
        }

        throw new NoSuchElementException($"Header with name '{headerName}' was not found.");
    }

    public bool IsValueInColumn(string columnName, string value)
    {
        var headers = GetHeaderCells().Select(h => h.Text.Trim()).ToList();

        int columnIndex = headers.FindIndex(h => h.Equals(columnName.Trim(), StringComparison.OrdinalIgnoreCase));

        if (columnIndex == -1)
            throw new NoSuchElementException($"Column '{columnName}' was not found.");

        foreach (var row in GetRows())
        {
            if (row.FindElements(By.CssSelector("th")).Any())
                continue;

            var cells = row.FindElements(By.CssSelector("td, th"));

            if (columnIndex < cells.Count)
            {
                string cellText = cells[columnIndex].Text.Trim();

                if (cellText.Equals(value.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }

        return false;
    }

    public IList<IWebElement> GetRows()
    {
        return _table.FindElements(By.TagName("tr"));
    }

    public IList<IWebElement> GetHeaderCells()
    {
        return _table.FindElements(By.CssSelector("thead th, tr th"));
    }

    public int RowCount => GetRows().Count;

    public int ColumnCount
    {
        get
        {
            var firstRow = GetRows().FirstOrDefault();
            if (firstRow == null) return 0;

            return firstRow.FindElements(By.CssSelector("th, td")).Count;
        }
    }

    public IWebElement GetCell(int rowIndex, int colIndex)
    {
        var rows = GetRows();
        var cells = rows[rowIndex].FindElements(By.CssSelector("th, td"));
        return cells[colIndex];
    }

    public string GetCellText(int rowIndex, int colIndex)
    {
        return GetCell(rowIndex, colIndex).Text;
    }

    public List<List<string>> GetTableData()
    {
        var data = new List<List<string>>();

        foreach (var row in GetRows())
        {
            var cells = row.FindElements(By.CssSelector("th, td"))
                           .Select(c => c.Text.Trim())
                           .ToList();
            if (cells.Count > 0)
                data.Add(cells);
        }
        return data;
    }

    public List<Dictionary<string, string>> GetTableAsDictionary()
    {
        var headers = GetHeaderCells().Select(h => h.Text.Trim()).ToList();
        var result = new List<Dictionary<string, string>>();

        foreach (var row in GetRows().Skip(1))
        {
            var cells = row.FindElements(By.CssSelector("td")).Select(c => c.Text.Trim()).ToList();
            if (cells.Count == 0) continue;

            var dict = new Dictionary<string, string>();
            for (int i = 0; i < headers.Count && i < cells.Count; i++)
                dict[headers[i]] = cells[i];

            result.Add(dict);
        }
        return result;
    }
    
    public int FindRowIndexByCellText(string text)
    {
        var rows = GetRows();
        for (int i = 0; i < rows.Count; i++)
        {
            if (rows[i].Text.Contains(text))
                return i;
        }
        return -1;
    }

    public void ClickInRow(string rowText, By elementLocator)
    {
        var row = FindRowByCellText(rowText);
        if (row != null)
        {
            row.FindElement(elementLocator).Click();
        }
    }
}
