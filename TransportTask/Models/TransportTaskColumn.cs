namespace TransportTask.Models;

public record TransportTaskColumn(int J, decimal B, IReadOnlyCollection<TransportTaskCell> Cells)
{
    public decimal RemainedB { get; set; }
}
