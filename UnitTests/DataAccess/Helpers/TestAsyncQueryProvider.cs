using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.DataAccess.Helpers
{
    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _queryProvider;

        internal TestAsyncQueryProvider(IQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
        }      
        public object Execute(Expression expression)
        {
            return _queryProvider.Execute(expression);
        }
        public TResult Execute<TResult>(Expression expression)
        {
            return _queryProvider.Execute<TResult>(expression);
        }
        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }
        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }
    }
}
