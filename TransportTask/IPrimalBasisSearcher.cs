using TransportTask.Models;

namespace TransportTask;

public interface IPrimalBasisSearcher
{
    void SearchBasis(TransportTaskTable table);
}
