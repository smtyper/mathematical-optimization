using TransportTask.Models;

namespace TransportTask;

public static class Extentions
{
    

    public static void FillWeight(this TransportTaskTable.TransportTaskCell cell)
    {
        if (cell.U is not null || cell.V is not null)
            throw new Exception("Cannot calculate weight because none initialized.");

        if (cell.U is null)
            cell.V = cell.Cost - cell.U;
        else
            cell.U = cell.Cost - cell.V;
    }

    public static void SetU(this TransportTaskTable.TransportTaskColumn column, decimal u)
    {
        foreach (var cell in column.Cells)
            cell.U = u;
    }

    public static void SetU(this TransportTaskTable.TransportTaskRow row, decimal u)
    {
        foreach (var cell in row.Cells)
            cell.U = u;
    }

    public static void SetV(this TransportTaskTable.TransportTaskColumn column, decimal v)
    {
        foreach (var cell in column.Cells)
            cell.V = v;
    }

    public static void SetV(this TransportTaskTable.TransportTaskRow row, decimal v)
    {
        foreach (var cell in row.Cells)
            cell.V = v;
    }
}
