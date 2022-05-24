namespace TransportTask.Models;

public record TransportTaskRow(int I, decimal A, IReadOnlyCollection<TransportTaskCell> Cells)
{
    public decimal RemainedA { get; set; }
}
