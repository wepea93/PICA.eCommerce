using System.Linq.Expressions;

namespace Products.Infraestructure.DbHelpers
{
    internal  class ReplaceParameterHelper : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }

        public ReplaceParameterHelper(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}
