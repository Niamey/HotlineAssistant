using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Moq;

namespace Xunit.MediatR.Mvc
{
    public abstract partial class AssertActionBase
    {
        private readonly List<Task> _tests;

        protected readonly Mock<IMediator> Mediator;
        protected readonly Mock<IAuthorizationService> AuthorizationService;
        protected readonly ActionInvoker Invoker;
        
        protected AssertActionBase(Type controllerType)
        {
            _tests = new List<Task>();

            Mediator = new Mock<IMediator>();
            AuthorizationService = new Mock<IAuthorizationService>();
            AuthorizationService.Setup(s => s.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object>(),
                    It.IsAny<IEnumerable<IAuthorizationRequirement>>()))
                .ReturnsAsync(AuthorizationResult.Success);

            Invoker = new ActionInvoker(controllerType, Mediator.Object, AuthorizationService.Object);
        }

        protected void Schedule(Task test)
        {
            _tests.Add(test);
        }

        public async Task Run()
        {
            foreach (var test in _tests)
            {
                await test;
            }
        }
    }
}