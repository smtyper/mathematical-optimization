namespace TransportTask.Models;

public record TransportTaskTable
{

    public TransportTaskTable(IReadOnlyList<IReadOnlyList<decimal>> costsMatrix, IReadOnlyList<decimal> reserves,
        IReadOnlyList<decimal> requests)
    {
        var m = costsMatrix.Count;
        var n = costsMatrix[0].Count;

        var cells = Enumerable.Range(0, m)
            .SelectMany(i => Enumerable.Range(0, n)
                .Select(j => new TransportTaskCell { I = i, J = j, Cost = costsMatrix[i][j] }))
            .ToArray();

        var rows = Enumerable.Range(0, m)
            .Select(i =>
            {
                var rowCells = cells.Where(cell => cell.I == i).ToArray();
                var a = reserves[i];
                var row = new TransportTaskRow(i, a, rowCells) { RemainedA = a };

                return row;
            })
            .ToArray();
        var columns = Enumerable.Range(0, n)
            .Select(j =>
            {
                var columnCells = cells.Where(cell => cell.J == j).ToArray();
                var b = requests[j];
                var column = new TransportTaskColumn(j, b, columnCells) { RemainedB = b };

                return column;
            })
            .ToArray();

        foreach (var cell in cells)
        {
            cell.Column = columns[cell.J];
            cell.Row = rows[cell.I];
        }

        M = m;
        N = n;
        Cells = cells;
        Rows = rows;
        Columns = columns;
    }

    public int M { get; }

    public int N { get; }

    public IReadOnlyCollection<TransportTaskCell> Cells { get; }

    public IReadOnlyList<TransportTaskRow> Rows { get; }

    public IReadOnlyList<TransportTaskColumn> Columns { get; }

    public record TransportTaskRow(int I, decimal A, IReadOnlyCollection<TransportTaskCell> Cells)
    {
        public decimal RemainedA { get; set; }
    }

    public record TransportTaskColumn(int J, decimal B, IReadOnlyCollection<TransportTaskCell> Cells)
    {
        public decimal RemainedB { get; set; }
    }

    public record TransportTaskCell
    {
        public int I { get; init; }

        public int J { get; init; }

        public decimal Cost { get; init; }

        public TransportTaskRow Row { get; set; }

        public TransportTaskColumn Column { get; set; }

        public decimal? X { get; set; }

        public decimal? U { get; set; }

        public decimal? V { get; set; }

        public bool IsBases => X is not null;

        public decimal Mark => V + U - X ?? throw new NullReferenceException();
    }
}
