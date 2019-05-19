using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.DataAccess.Helpers
{
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public TestAsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }
        public T Current
        {
            get
            {
                return _enumerator.Current;
            }
        }
        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_enumerator.MoveNext());
        }
        public void Dispose()
        {
            _enumerator.Dispose();
        }
    }
}
