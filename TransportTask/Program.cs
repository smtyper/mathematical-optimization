using TransportTask;
using TransportTask.Models;

var costMatrix = new[]
{
    new[] { 3M, 2.4M, 1.1M, 1.2M, 2.5M },
    new[] { 2.6M, 0.4M, 2.9M, 2M, 2.4M },
    new[] { 2.7M, 1.4M, 1.4M, 1M, 1.8M },
    new[] { 0.6M, 1.4M, 2.8M, 0.8M, 0.2M }
};
var reserves = new[] { 210M, 190M, 150M, 250M };
var requests = new[] { 150M, 150M, 150M, 150M, 200M };

var table = new TransportTaskTable(costMatrix, reserves, requests);
var solver = new TransportTaskSolver(new NorthWestCornerPrimalBasisSearcher());

solver.Solve(table);

var temp = table.Cells
    .Where(cell => cell.IsBases)
    .Sum(cell => cell.Cost * cell.X);

;
