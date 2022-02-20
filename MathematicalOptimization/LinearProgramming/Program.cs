using System;
using LinearProgramming;
using LinearProgramming.Models;

var configuration = new TwoDimensionalTaskSolverConfiguration()
{
    ConstraintFunctions = new[]
    {
        new LinearFunction
        {
            Coefficients = new[] { -1m, 1m },
            ConstantPart = 5,
            ConstraintType = ConstraintType.LessThanOrEqual
        },
        new LinearFunction
        {
            Coefficients = new[] { 5m, 1m },
            ConstantPart = 5,
            ConstraintType = ConstraintType.LessThanOrEqual
        },
        new LinearFunction
        {
            Coefficients = new[] { 0, 1m },
            ConstantPart = -2,
            ConstraintType = ConstraintType.GreaterThanOrEqual
        },
        new LinearFunction
        {
            Coefficients = new[] { -1m, -1m },
            ConstantPart = 1,
            ConstraintType = ConstraintType.LessThanOrEqual
        }
    },
    MainFunction = new LinearFunction
    {
        Coefficients = new[] { 1m, -2m },
        ConstantPart = null,
        ConstraintType = ConstraintType.LessThanOrEqual
    },
    Type = OptimizationType.ToMax
};
var solver = new TwoDimensionalTaskSolver(configuration);
var result = solver.Solve();

Console.WriteLine();
