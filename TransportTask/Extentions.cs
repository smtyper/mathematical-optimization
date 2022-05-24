using TransportTask.Models;

namespace TransportTask;

public static class Extentions
{
    public static void ClearWeights(this TransportTaskTable table)
    {
        foreach (var cell in table.Cells)
        {
            cell.U = null;
            cell.V = null;
        }
    }

    public static bool CanFillWeight(this TransportTaskCell cell) =>
        (cell.U is null && cell.V is not null) || (cell.U is not null && cell.V is null);

    public static void FillWeight(this TransportTaskCell cell)
    {
        if (cell.U is null && cell.V is null)
            throw new Exception("Cannot calculate weight because none initialized.");

        if (cell.U is null)
        {
            var u = cell.Cost - cell.V;

            cell.U = u;
            cell.Row.SetU(u.Value);
        }
        else
        {
            var v = cell.Cost - cell.U;

            cell.V = cell.Cost - cell.U;
            cell.Column.SetV(v.Value);
        }
    }

    public static void SetU(this TransportTaskRow row, decimal u)
    {
        foreach (var cell in row.Cells)
            cell.U = u;
    }

    public static void SetV(this TransportTaskColumn column, decimal v)
    {
        foreach (var cell in column.Cells)
            cell.V = v;
    }
}
