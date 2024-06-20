using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Vostok.Hotline.Authorization.Security.Helpers
{
	public class CertificateHelper
	{
		public static X509Certificate2 FindByThumbprint(string thumbprint, StoreName storeName, StoreLocation storeLocation, bool throwIfNotFound = true)
		{
			using (var certificateStore = new X509Store(storeName, storeLocation))
			{
				certificateStore.Open(OpenFlags.ReadOnly);

				var certificateCollection = certificateStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, true);

				var certificate = certificateCollection.Cast<X509Certificate2>().FirstOrDefault();

				if (certificate == null && throwIfNotFound)
				{
					throw new ArgumentException(
					string.Format("Cannot find certificate with thumbprint {0} in certificate store ", thumbprint));
				}

				return certificate;
			}
		}
	}
}
