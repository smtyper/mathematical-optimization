using TransportTask;
using TransportTask.Models;

var costMatrix = new[]
{
    new[] { 1M, 2M, 1M, 3M },
    new[] { 0.5M, 1M, 2M, 0.5M },
    new[] { 2M, 1M, 1M, 2M }
};
var reserves = new[] { 200M, 30, 100M };
var requests = new[] { 150M, 50, 30M, 100M };

var table = new TransportTaskTable(costMatrix, reserves, requests);
var solver = new TransportTaskSolver(table, new NorthWestCornerPrimalBasisSearcher());

solver.Solve();

;
