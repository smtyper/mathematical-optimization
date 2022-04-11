using TransportTask.Models;

namespace TransportTask;

public class TransportTaskSolver
{
    private readonly IPrimalBasisSearcher _primalBasisSearcher;

    public TransportTaskSolver(IPrimalBasisSearcher primalBasisSearcher)
    {
        _primalBasisSearcher = primalBasisSearcher;
    }

    public void Solve(TransportTaskTable table)
    {
        _primalBasisSearcher.SearchBasis(table);
        EnsureIsValidPrimalBasis(table);

        var basisCells = table.Cells.Where(cell => cell.IsBases).ToArray();

    }

    private void FillWeights(TransportTaskTable table, IReadOnlyCollection<TransportTaskTable.TransportTaskCell> cells)
    {

    }

    private void EnsureIsValidPrimalBasis(TransportTaskTable table)
    {
        if (table.Cells.Count(cell => cell.IsBases) != table.N + table.M - 1)
            throw new Exception("Incorrect basis cells count.");
    }
}
