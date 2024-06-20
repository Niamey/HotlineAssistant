using System;

namespace Xunit.MediatR.Mvc
{
    public class AssertQueryAction
    {
        private readonly Type _controllerType;
        private readonly Type _commandType;

        public AssertQueryAction(Type controllerType, Type commandType)
        {
            _controllerType = controllerType;
            _commandType = commandType;
        }
    }
}