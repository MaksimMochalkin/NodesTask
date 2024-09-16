﻿namespace NodesTask.SqlCommandHandlers
{
    using System.Linq.Expressions;

    public class NotEqualSqlCommandHandler<T> : SqlCommandHandler<T> where T : class
    {
        public override string Command => "notequal";

        public override IQueryable<T> Execute(IQueryable<T> query, string propertyName, string value, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "c");
            var property = Expression.Property(parameter, propertyName);

            object constantValue;
            if (property.Type == typeof(Guid))
            {
                constantValue = Guid.Parse(value);
            }
            else
            {
                constantValue = Convert.ChangeType(value, property.Type);
            }

            var constant = Expression.Constant(Convert.ChangeType(constantValue, property.Type));
            var expression = Expression.NotEqual(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, parameter);
            return query.Where(lambda);
        }
    }
}
