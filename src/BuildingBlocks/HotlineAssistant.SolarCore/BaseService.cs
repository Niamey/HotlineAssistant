using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.Common.Requests.Base;
using Vostok.HotLineAssistant.Common.Response.Base;
using Vostok.HotLineAssistant.Domain.Exceptions;

namespace Vostok.HotlineAssistant.SolarCore
{
	public abstract class BaseService : IService
	{
		protected readonly ILogger<BaseService> Logger;

		protected BaseService(ILogger<BaseService> logger)
		{
			Logger = logger;
		}

		protected T GetResponse<T>() where T : BaseResponse, new()
		{
			var result = new T();
			return result;
		}

		protected T GetResponse<T>(Action<T> initializer)
			where T : BaseResponse, new()
		{
			var result = GetResponse<T>();
			initializer.Invoke(result);
			return result;
		}

		public virtual async Task<TResp> ProcessRequest<TResp, TReq>(TReq request, Func<TReq, Task<TResp>> func)
			where TResp : BaseResponse, new()
			where TReq : BaseRequest
		{
			try
			{
				var result = await func.Invoke(request);
				return result;
			}
			catch (NullReferenceException ex)
			{
				Logger.LogError("Data not found", ex);
				throw new FailedRequestException("Data not found", ex);
			}
			catch (Exception ex)
			{
				Logger.LogError("Service Invocation Exception", ex);
				throw;
			}
		}

		public virtual TResp ProcessRequest<TResp, TReq>(TReq request, Func<TReq, TResp> func)
			where TResp : BaseResponse, new()
			where TReq : BaseRequest
		{
			try
			{
				var result = func.Invoke(request);
				return result;
			}
			catch (NullReferenceException ex)
			{
				Logger.LogError("Data not found", ex);
				throw new FailedRequestException("Data not found", ex);
			}
			catch (Exception exc)
			{
				Logger.LogError("Service Invocation Exception", exc);
				throw;
			}
		}
	}
}
