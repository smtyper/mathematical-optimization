using TransportTask.Models;

namespace TransportTask;

public class TransportTaskSolver
{
    private readonly IPrimalBasisSearcher _primalBasisSearcher;

    public TransportTaskSolver(IPrimalBasisSearcher primalBasisSearcher) => _primalBasisSearcher = primalBasisSearcher;

    public void Solve(TransportTaskTable table)
    {
        _primalBasisSearcher.SearchBasis(table);
        EnsureIsValidPrimalBasis(table);

        while (true)
        {
            FillWeights(table);

            if (table.Cells.Any(cell => cell.Mark > 0))
            {
                ToNewBasis(table);
                table.ClearWeights();
            }
            else
                break;
        }


    }

    private static void ToNewBasis(TransportTaskTable table)
    {
        static void Fill(TransportTaskCell cell)
        {
            var crossCells = cell.Row.Cells.Concat(cell.Column.Cells).ToArray();

            cell.Q = crossCells.Any(cell => cell.Q is QMark.Add) ?
                QMark.Subtract :
                QMark.Add;
        }

        var newBasisCell = table.Cells.MaxBy(cell => cell.Mark);
        newBasisCell!.Q = QMark.Add;

        bool CanFill(TransportTaskCell cell) => OptionsCount(cell) is 1 or 2;

        int OptionsCount(TransportTaskCell cell) =>
            cell.Row.Cells.Count(rowCell => (rowCell.IsBases || rowCell == newBasisCell) &&
                                            rowCell.Q is not QMark.Empty) +
            cell.Column.Cells.Count(columnCell => (columnCell.IsBases || columnCell == newBasisCell) &&
                                               columnCell.Q is not QMark.Empty);

        var basisCells = table.Cells
            .Where(cell => cell.IsBases &&
                           cell.Row.Cells.Count(rowCell => rowCell.IsBases || rowCell == newBasisCell) > 1 &&
                           cell.Column.Cells.Count(columnCell => columnCell.IsBases || columnCell == newBasisCell) > 1)
            .ToArray();

        while (basisCells.Any(CanFill))
            Fill(basisCells.First(CanFill));

        var excludedCell = basisCells.Where(cell => cell.Q is QMark.Subtract).MinBy(cell => cell.X);
        var qValue = excludedCell!.X!.Value;

        excludedCell.X = null;

        foreach (var cell in table.Cells.Where(cell => cell.Q is not QMark.Empty))
            if (cell.Q is QMark.Add)
                cell.X = cell.X is null ?
                    qValue :
                    cell.X + qValue;
            else
                cell.X -= qValue;
    }

    private static void FillWeights(TransportTaskTable table)
    {
        var basisCells = table.Cells.Where(cell => cell.IsBases).ToArray();

        basisCells.First().Row.SetU(0);

        while (table.Cells.Any(Extentions.CanFillWeight))
            if (basisCells.Any(Extentions.CanFillWeight))
                foreach (var cell in basisCells.Where(Extentions.CanFillWeight))
                    cell.FillWeight();
            else
                basisCells.First(cell => cell.U is null && cell.V is null).Row.SetU(0);
    }

    private static void EnsureIsValidPrimalBasis(TransportTaskTable table)
    {
        if (table.Cells.Count(cell => cell.IsBases) != table.N + table.M - 1)
            throw new Exception("Incorrect basis cells count.");
    }
}
