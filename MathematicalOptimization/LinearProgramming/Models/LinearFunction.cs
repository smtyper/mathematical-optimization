using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearProgramming.Models
{
    public class LinearFunction
    {
        public IReadOnlyCollection<decimal> Coefficients { get; init; }

        public decimal? ConstantPart { get; init; }

        public ConstraintType ConstraintType { get; init; }

        public int Dimension => Coefficients.Count;

        public Func<IReadOnlyCollection<decimal>, bool> GetPredicate() =>
            ConstantPart is null ?
                throw new Exception("The predicate cannot be created because the ConstantPart is null.") :
                variableValues => ConstraintType switch
                {
                    ConstraintType.Equal => GetResult(variableValues) == ConstantPart!.Value,
                    ConstraintType.NotEqual => GetResult(variableValues) != ConstantPart!.Value,
                    ConstraintType.GreaterThan => GetResult(variableValues) > ConstantPart,
                    ConstraintType.LessThan => GetResult(variableValues) < ConstantPart,
                    ConstraintType.GreaterThanOrEqual => GetResult(variableValues) >= ConstantPart,
                    ConstraintType.LessThanOrEqual => GetResult(variableValues) <= ConstantPart,
                    _ => throw new NotImplementedException()
                };

        public decimal GetResult(IReadOnlyCollection<decimal> variableValues) =>
            variableValues.Count == Coefficients.Count ?
                Coefficients.Zip(variableValues).Sum(pair => pair.First * pair.Second) :
                throw new Exception("Coefficients count doesn't match variables count.");
    }

    public enum ConstraintType
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }
}
