using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Xunit.MediatR.Mvc
{
    public class AssertActionQuery<TResponse> : AssertActionBase
    {
        private readonly Type _commandType;

        private readonly TResponse _positive;
        private readonly IRequest<TResponse> _positiveCommand;
        private readonly IRequest<TResponse> _negativeCommand;

        public AssertActionQuery(Type controllerType, Type commandType) : base(controllerType)
        {
            _commandType = commandType;

            var fixture = new Fixture();
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _positive = fixture.Create<TResponse>();

            _positiveCommand = Activator.CreateInstance(_commandType) as IRequest<TResponse>;
            _negativeCommand = Activator.CreateInstance(_commandType) as IRequest<TResponse>;

            Mediator.Setup(m => m.Send(_positiveCommand, CancellationToken.None))
                .ReturnsAsync(_positive);

            Mediator.Setup(m => m.Send(null as IRequest<TResponse>, CancellationToken.None))
                .ReturnsAsync(_positive);

            Mediator.Setup(m => m.Send(_negativeCommand, CancellationToken.None))
                .ReturnsAsync(default(TResponse));
        }
        
        public AssertActionQuery<TResponse> IsOk()
        {
            Schedule(TestIsOk());

            return this;
        }

        private async Task TestIsOk()
        {
            //Act
            var result = await InvokePositiveTestCase();
            
            //Assert
            Assert.IsType<OkResult>(result);
        }

        public AssertActionQuery<TResponse> IsCreated()
        {
            Schedule(TestIsCreated());

            return this;
        }

        public AssertActionQuery<TResponse> IsCreated(Func<CreatedAtActionResult, bool> isExpected)
        {
            Schedule(TestIsCreated(isExpected));

            return this;
        }

        private async Task TestIsCreated(Func<CreatedAtActionResult, bool> isExpected)
        {
            //Act
            var result = await InvokePositiveTestCase();

            //Assert
            Assert.ActionResult(result).IsCreatedAt(isExpected);
        }

        private async Task TestIsCreated()
        {
            //Act
            var result = await InvokePositiveTestCase();

            //Assert
            Assert.ActionResult(result).IsCreatedAt(_positive);
        }

        public AssertActionQuery<TResponse> IsJson()
        {
            Schedule(TestIsJson());

            return this;
        }

        private async Task TestIsJson()
        {
            //Act
            var result = await InvokePositiveTestCase();

            //Assert
            Assert.ActionResult(result).IsJson(_positive);
        }

        public AssertActionQuery<TResponse> IsBadRequestOnNull()
        {
            Schedule(TestIsBadRequestOnNull());

            return this;
        }

        private async Task TestIsBadRequestOnNull()
        {
            //Act
            var result = await Invoker.Send<TResponse>(_commandType, null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        public AssertActionQuery<TResponse> IsNotFoundWhenFailed()
        {
            Schedule(TestIsNotFoundWhenFailed());

            return this;
        }

        private async Task TestIsNotFoundWhenFailed()
        {
            //Act
            var result = await Invoker.Send(_commandType, _negativeCommand);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private async Task<IActionResult> InvokePositiveTestCase()
        {
            var result = await Invoker.Send(_commandType, _positiveCommand);

            Mediator.Verify(m => m.Send(_positiveCommand, It.IsAny<CancellationToken>()), Times.Once, "Command was not sent.");

            return result;
        }
    }
}