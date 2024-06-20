namespace Vostok.Hotline.Core.Common.GlobalContext
{
	public static class GlobalHttpContextAccessor
	{
		private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
		public static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public static bool HasContext
		{
			get 
			{
				return _httpContextAccessor != null && _httpContextAccessor.HttpContext != null;
			}
		}

		public static Microsoft.AspNetCore.Http.HttpContext Current
		{
			get
			{
				return _httpContextAccessor.HttpContext;
			}
		}
	}
}
