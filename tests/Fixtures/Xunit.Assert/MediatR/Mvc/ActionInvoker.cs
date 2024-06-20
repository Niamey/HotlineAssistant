using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Xunit.MediatR.Mvc
{
    public abstract partial class AssertActionBase
    {
        protected class ActionInvoker
        {
            private readonly Controller _controller;

            public ActionInvoker(Type controllerType, IMediator mediator, IAuthorizationService authorizationService)
            {
                _controller = Activator.CreateInstance(controllerType, new object[] {mediator, authorizationService}, null) as Controller;
            }

            public Task<IActionResult> Send<TResponse>(Type commandType, IRequest<TResponse> command)
            {
                var methodInfo = _controller.GetType()
                    .GetMethods()
                    .SingleOrDefault(m => m.GetParameters().Any(p => p.ParameterType == commandType));

                return InvokeMethod(methodInfo, command);
            }

            public Task<IActionResult> Send(string methodName)
            {
                var methodInfo = _controller.GetType()
                    .GetMethods()
                    .SingleOrDefault(m => m.Name == methodName && !m.GetParameters().Any());

                return InvokeMethod(methodInfo);
            }

            private Task<IActionResult> InvokeMethod(MethodBase methodInfo, params object[] parameters)
            {
                if (methodInfo == null)
                    throw new InvalidOperationException("Failed to locate given method.");

                var act = methodInfo.Invoke(_controller, parameters);
                if (act == null)
                    throw new InvalidOperationException("Failed to execute required controller method.");

                return (Task<IActionResult>) act;
            }
        }
    }
}