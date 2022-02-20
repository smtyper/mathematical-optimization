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

        public Point Solve()
        {
            var constraintPredicates = _configuration.ConstraintFunctions
                .Select(linearFunction => linearFunction.GetPredicate())
                .ToArray();

            bool CheckPoint(Point point) => constraintPredicates
                .All(predicate => predicate(new[] { point.First, point.Second }));

            var intersectionPoints = _configuration.ConstraintFunctions
                .SelectMany(firstFunction => _configuration.ConstraintFunctions
                    .Select(secondFunction => GetIntersection(firstFunction, secondFunction)))
                .Where(point => point is not null && CheckPoint(point))
                .ToArray();

            var resultPoint = _configuration.Type switch
            {
                OptimizationType.ToMax => intersectionPoints
                    .OrderByDescending(point => _configuration.TargetFunction.GetResult(new[] { point.First, point.Second }))
                    .First(),
                OptimizationType.ToMin => intersectionPoints
                    .OrderBy(point => _configuration.TargetFunction.GetResult(new[] { point.First, point.Second }))
                    .First(),
                _ => throw new NotImplementedException()
            };

            return resultPoint;
        }

        private static Point GetIntersection(LinearFunction first, LinearFunction second)
        {
            (decimal, decimal) GetFunctionCoefficients(LinearFunction function)
            {
                var c1 = function.Coefficients.First();
                var c2 = function.Coefficients.Last();

                if (c1 is 0)
                    return (0, 0);

                var k = -1 * c2 / c1;
                var b = function.ConstantPart!.Value / c1;

                return (k, b);
            }

            var (k1, b1) = GetFunctionCoefficients(first);
            var (k2, b2) = GetFunctionCoefficients(second);

            if (k1 == k2)
                return null;

            var x = (b2 - b1) / (k1 - k2);
            var y = (k1 * x) + b1;
            var point = new Point(x, y);

            return point;
        }

        private static void EnsureIsValidConfiguration(TwoDimensionalTaskSolverConfiguration configuration)
        {
            if (configuration.ConstraintFunctions
                .Append(configuration.TargetFunction)
                .Any(linearFunction => linearFunction.Dimension is not 2))
                throw new Exception("The dimension of one of the functions is not equal to two.");
        }
    }

    public class TwoDimensionalTaskSolverConfiguration
    {
        public IReadOnlyCollection<LinearFunction> ConstraintFunctions { get; init; }

        public LinearFunction TargetFunction { get; init; }

        public OptimizationType Type { get; init; }
    }

    public enum OptimizationType
    {
        ToMin,
        ToMax
    }
}
