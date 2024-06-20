namespace Vostok.Hotline.Core.Common.Data.Abstractions
{
	public interface IDbConnectionConfig
	{
		string ConnectionString { get; }
		int CommandTimeout { get; }
	}
}
