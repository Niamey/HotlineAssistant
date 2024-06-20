using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Xunit.MediatR
{
    public class AssertRequestHandler
    {
        private readonly object _handler;

        internal AssertRequestHandler(object handler)
        {
            _handler = handler;
        }
        
        public Task ThrowsOnNull() => ThrowsOnNull(null);
        
        public async Task ThrowsOnNull(Type queryType)
        {
            var methodInfo = GetHandleInfo(queryType);
            var parameterInfo = methodInfo.GetParameters().First();

            var e = await Assert.ThrowsAnyAsync<Exception>(
                () => methodInfo.Invoke(_handler, new object[] {null, CancellationToken.None}) as Task);

            var inner = (e is ArgumentNullException ? e : e.InnerException) as ArgumentNullException;
            Assert.True(inner != null, "Expected ArgumentNullException.");
            Assert.True(parameterInfo.Name == inner.ParamName, $"Expected parameter name {parameterInfo.Name}, instead was {inner.ParamName}.");
        }

        private MethodInfo GetHandleInfo(Type queryType)
        {
            var methodInfo = _handler.GetType()
                .GetMethods()
                .SingleOrDefault(
                    m => m.Name == "Handle" && m.GetParameters().Any(
                        p => queryType == null || p.ParameterType == queryType));

            if (methodInfo == null)
                throw new InvalidOperationException("Failed to locate method for given request type.");

            return methodInfo;
        }
    }
}