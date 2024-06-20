using System;
using Microsoft.AspNetCore.Mvc;

namespace Xunit
{
	public partial class Assert
	{
		public static Injection.AssertFactory Injection
			=> new Injection.AssertFactory();

		public static Mvc.AssertActionResult ActionResult(IActionResult result)
			=> new Mvc.AssertActionResult(result);

		public static MediatR.AssertRequestHandler RequestHandler(object handler)
			=> new MediatR.AssertRequestHandler(handler);

		public static MediatR.Mvc.AssertActionQuery<TResponse> Action<TResponse>(Type controllerType, Type commandType)
			=> new MediatR.Mvc.AssertActionQuery<TResponse>(controllerType, commandType);

		public static MediatR.Mvc.AssertActionCommand Action(Type controllerType, Type commandType)
			=> new MediatR.Mvc.AssertActionCommand(controllerType, commandType);

		public static MediatR.Mvc.AssertActionMethod Action(Type controllerType, string methodName)
			=> new MediatR.Mvc.AssertActionMethod(controllerType, methodName);
	}
}
