namespace TransportTask.Models;

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

    public QMark Q { get; set; }

    public bool IsBases => X is not null;

    public decimal Mark => V + U - Cost ?? throw new NullReferenceException();
}

public enum QMark
{
    Empty,
    Add,
    Subtract
}
