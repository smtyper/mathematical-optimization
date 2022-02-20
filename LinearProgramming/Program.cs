using System;
using LinearProgramming;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build()
    .Get<TwoDimensionalTaskSolverConfiguration>();

var solver = new TwoDimensionalTaskSolver(configuration);
var result = solver.Solve();

Console.WriteLine();
