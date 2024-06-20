using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Xunit.MediatR.Mvc
{
    public class AssertActionCommand : AssertActionBase
    {
        private readonly Type _commandType;

        public AssertActionCommand(Type controllerType, Type commandType) : base(controllerType)
        {
            _commandType = commandType;

            Mediator.Setup(m => m.Send(It.IsAny<IRequest>(), CancellationToken.None))
                .ReturnsAsync(Unit.Value);
        }

        public AssertActionCommand IsOk()
        {
            Schedule(TestIsOk());
            
            return this;
        }

        private async Task TestIsOk()
        {
            //Act
            var result = await SendCommand();

            //Assert
            Assert.IsType<OkResult>(Assert.IsType<OkResult>(result));
        }

        public AssertActionCommand IsBadRequestOnNull()
        {
            Schedule(TestIsBadRequestOnNull());
            
            return this;
        }

        private async Task TestIsBadRequestOnNull()
        {
            //Act
            var result = await Invoker.Send<Unit>(_commandType, null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        private async Task<IActionResult> SendCommand()
        {
            var command = Activator.CreateInstance(_commandType) as IRequest;

            var result = await Invoker.Send(_commandType, command);

            Mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once, "Command was not sent.");

            return result;
        }
    }
}