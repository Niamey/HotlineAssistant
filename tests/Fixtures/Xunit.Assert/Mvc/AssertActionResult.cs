using System;
using Microsoft.AspNetCore.Mvc;

namespace Xunit.Mvc
{
	public class AssertActionResult
	{
		private readonly IActionResult _result;

		internal AssertActionResult(IActionResult result)
		{
			_result = result;
		}

		public AssertActionResult IsCreatedAt()
		{
			Assert.IsType<CreatedAtActionResult>(_result);

			return this;
		}

		public AssertActionResult IsCreatedAt(Func<CreatedAtActionResult, bool> isExpected)
		{
			var actionResult = Assert.IsType<CreatedAtActionResult>(_result);

			Assert.True(isExpected(actionResult));

			return this;
		}

		public AssertActionResult IsCreatedAt(object id, string actionName = "Get")
		{
			var actionResult = Assert.IsType<CreatedAtActionResult>(_result);
			Assert.True(actionName == actionResult.ActionName, $"Expected \"{actionName}\" action name, instead got \"{actionResult.ActionName}\".");
			Assert.True(actionResult.RouteValues.ContainsKey("id"), "Expected route value \"id\".");
			Assert.True(id.Equals(actionResult.RouteValues["id"]), $"Expected \"{id}\" route value, instead got \"{actionResult.RouteValues["id"]}\".");
			Assert.True(id.Equals(actionResult.Value), $"Expected \"{id}\" action result value, instead got \"{actionResult.Value}\".");

			return this;
		}

		public AssertActionResult IsJson(object expected)
		{
			var jsonResult = _result as JsonResult;

			Assert.True(jsonResult != null, "Expected JsonResult.");
			Assert.True(expected.Equals(jsonResult.Value), $"Unexpected JsonResult value.");

			return this;
		}
    }
}