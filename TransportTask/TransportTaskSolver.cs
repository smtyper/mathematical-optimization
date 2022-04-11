using TransportTask.Models;

namespace TransportTask;

public class TransportTaskSolver
{
    private readonly TransportTaskTable _transportTaskTable;
    private readonly IPrimalBasisSearcher _primalBasisSearcher;

    public TransportTaskSolver(TransportTaskTable transportTaskTable, IPrimalBasisSearcher primalBasisSearcher)
    {
        _transportTaskTable = transportTaskTable;
        _primalBasisSearcher = primalBasisSearcher;
    }

    public void Solve()
    {
        _primalBasisSearcher.SearchBasis(_transportTaskTable);

        EnsureIsValidPrimalBasis();
    }

    private void EnsureIsValidPrimalBasis()
    {
        if (_transportTaskTable.Cells.Count(cell => cell.IsBases) != _transportTaskTable.N + _transportTaskTable.M - 1)
            throw new Exception("Incorrect basis cells count.");
    }
}
