using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Xunit.MediatR.Mvc
{
    public class AssertActionMethod : AssertActionBase
    {
        private readonly string _methodName;

        public AssertActionMethod(Type controllerType, string methodName) : base(controllerType)
        {
            _methodName = methodName;
            Mediator.Setup(m => m.Send(It.IsAny<IRequest>(), CancellationToken.None))
                .ReturnsAsync(Unit.Value);
        }

        public AssertActionMethod IsOk()
        {
            Schedule(TestIsOk());

            return this;
        }

        private async Task TestIsOk()
        {
            //Act
            var result = await Invoker.Send(_methodName);

            //Assert
            Assert.IsType<OkResult>(Assert.IsType<OkResult>(result));
        }
    }
}