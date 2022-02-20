using System;
using System.Collections.Generic;
using System.Linq;
using LinearProgramming.Models;

namespace LinearProgramming
{
    public class TwoDimensionalTaskSolver
    {
        public record Point(decimal First, decimal Second);

        private readonly TwoDimensionalTaskSolverConfiguration _configuration;

        public TwoDimensionalTaskSolver(TwoDimensionalTaskSolverConfiguration configuration)
        {
            EnsureIsValidConfiguration(configuration);

            _configuration = configuration;
        }
    }

    public class TwoDimensionalTaskSolverConfiguration
    {
        public IReadOnlyCollection<LinearFunction> ConstraintFunctions { get; init; }

        public LinearFunction MainFunction { get; init; }

        public OptimizationType Type { get; init; }
    }

    public enum OptimizationType
    {
        ToMin,
        ToMax
    }
}
