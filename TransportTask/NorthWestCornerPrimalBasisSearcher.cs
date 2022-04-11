using TransportTask.Models;

namespace TransportTask;

public class NorthWestCornerPrimalBasisSearcher : IPrimalBasisSearcher
{
    public void SearchBasis(TransportTaskTable table)
    {
        foreach (var row in table.Rows)
            foreach (var cell in row.Cells)
            {
                var column = cell.Column;
                var remainedA = row.RemainedA;
                var remainedB = column.RemainedB;

                if (column.RemainedB is 0)
                    continue;

                var isEnough = remainedA >= remainedB;

                cell.X = isEnough ? remainedB : remainedA;
                row.RemainedA = isEnough ? remainedA - remainedB : 0;
                column.RemainedB = isEnough ? 0 : remainedB - remainedA;


                if (!isEnough)
                    break;
            }
    }
}
