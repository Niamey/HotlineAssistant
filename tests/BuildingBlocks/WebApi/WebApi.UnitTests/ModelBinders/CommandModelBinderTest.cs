using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Vostok.HotLineAssistant.WebApi.ModelBinders;
using Xunit;

namespace Vostok.HotLineAssistant.WebApi.UnitTests.ModelBinders
{
	 public class CommandModelBinderTest
    {
        public CommandModelBinderTest()
        {
            _expected = new Model
            {
                FirstRouteValue = 11,
                SecondRouteValue = "second",
                InnerValue = 33
            };

            _context = new Mock<ModelBindingContext> {CallBase = true};
            _context.Setup(c => c.ActionContext)
                .Returns(new ActionContext
                {
                    RouteData = new RouteData(new RouteValueDictionary(new Dictionary<string, object>
                    {
                        {nameof(_expected.FirstRouteValue), _expected.FirstRouteValue.ToString()},
                        {nameof(_expected.SecondRouteValue), _expected.SecondRouteValue},
                        {nameof(_expected.ReadOnlyValue), "1"},
                        {"UnknownValue", "112"}
                    }))
                });

            _innerBinder = new Mock<IModelBinder>();
            _innerBinder.Setup(b => b.BindModelAsync(_context.Object))
                .Returns(Task.CompletedTask);

            _binder = new CommandModelBinder(_innerBinder.Object);
        }

        public class Model
        {
            public int FirstRouteValue { get; set; }

            public string SecondRouteValue { get; set; }

            [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "Required for testing purposes.")]
            public int ReadOnlyValue { get; }

            public int InnerValue { get; set; }
        }

        private readonly Model _expected;

        private readonly Mock<ModelBindingContext> _context;
        private readonly Mock<IModelBinder> _innerBinder;
        private readonly CommandModelBinder _binder;

        [Fact]
        public async Task BindModelAsync_AddsModelError_WhenFailed_ToConvert_RouteValue()
        {
            //Arrange
            var model = new Model();

            var modelState = new ModelStateDictionary();

            _context.SetupGet(c => c.Result)
                .Returns(ModelBindingResult.Success(model));

            _context.SetupGet(c => c.ModelState)
                .Returns(modelState);

            var routeData = new RouteData(new RouteValueDictionary(new Dictionary<string, object>
            {
                {nameof(Model.FirstRouteValue), "invalid"}
            }));

            _context.Setup(c => c.ActionContext)
                .Returns(new ActionContext {RouteData = routeData});

            //Act
            await _binder.BindModelAsync(_context.Object);

            //Assert
            Assert.Contains(nameof(Model.FirstRouteValue), modelState.Keys);
            var error = Assert.Single(modelState[nameof(Model.FirstRouteValue)].Errors);
            if (error != null) 
	            Assert.IsType<FormatException>(error.Exception);
            _context.VerifySet(c => c.Result = ModelBindingResult.Failed());
        }

        [Fact]
        public async Task BindModelAsync_Delegates_ToInnerModelBinder()
        {
            //Act
            await _binder.BindModelAsync(_context.Object);

            //Assert
            _innerBinder.Verify(b => b.BindModelAsync(_context.Object));
        }

        [Fact]
        public async Task BindModelAsync_DoesNot_FillModel_UsingRouteValues_WhenInnerBinder_Failed()
        {
            //Arrange
            var model = new Model();

            _context.SetupGet(c => c.Result)
                .Returns(ModelBindingResult.Failed);

            //Act
            await _binder.BindModelAsync(_context.Object);

            //Assert
            Assert.Equal(default, model.FirstRouteValue);
        }

        [Fact]
        public async Task BindModelAsync_FillsModel_UsingRouteValues()
        {
            //Arrange
            var actual = new Model();

            _context.SetupGet(c => c.Result)
                .Returns(ModelBindingResult.Success(actual));

            _innerBinder.Setup(b => b.BindModelAsync(_context.Object))
                .Callback<ModelBindingContext>(c => actual.InnerValue = _expected.InnerValue)
                .Returns(Task.CompletedTask);

            //Act
            await _binder.BindModelAsync(_context.Object);

            //Assert
            Assert.Equal(_expected.InnerValue, actual.InnerValue);
            Assert.Equal(_expected.FirstRouteValue, actual.FirstRouteValue);
            Assert.Equal(_expected.SecondRouteValue, actual.SecondRouteValue);
            Assert.Equal(default, actual.ReadOnlyValue);
        }

        [Fact]
        public Task BindModelAsync_GivenBindingContext_IsNull_Throws()
        {
            return Assert.ThrowsAsync<ArgumentNullException>("bindingContext", () => _binder.BindModelAsync(null));
        }

        [Fact]
        public void Ctor_GivenInnerModelBinder_IsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new CommandModelBinder(null));
        }
	}
}
