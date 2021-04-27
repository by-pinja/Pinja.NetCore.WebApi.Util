using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pinja.NetCore.WebApi.Util.Query
{
    public class SubstringExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> Subst { get; } = new();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (Subst.TryGetValue(node, out var newValue))
            {
                return newValue;
            }
            return node;
        }
    }
}