namespace Vostok.Hotline.Core.Common.ConstantNames
{
	public static class EnvironmentConstant
	{
		public static class ApiEndpoint
		{
			public static class StorefrontApi
			{
				public const string CounterpartUrl = "ApiEndpoint.StorefrontApi.CounterpartUrl";
				public const string CardsUrl = "ApiEndpoint.StorefrontApi.CardsUrl";
				public const string AgreementsUrl = "ApiEndpoint.StorefrontApi.AgreementsUrl";
				public const string AccountsUrl = "ApiEndpoint.StorefrontApi.AccountsUrl";
				public const string TransactionsUrl = "ApiEndpoint.StorefrontApi.TransactionsUrl";
				public const string ModulesUrl = "ApiEndpoint.StorefrontApi.ModulesUrl";
				public const string UserInterfaceUrl = "ApiEndpoint.StorefrontApi.UserInterfaceUrl";				
				public const string FeedBackEmail = "ApiEndpoint.StorefrontApi.FeedBackEmail";
				public const string ReportExceptionsEmail = "ApiEndpoint.StorefrontApi.ReportExceptionsEmail";
				public const string TravelsUrl = "ApiEndpoint.StorefrontApi.TravelsUrl";
				public const string CountriesUrl = "ApiEndpoint.StorefrontApi.CountriesUrl";
				public const string StatementUrl = "ApiEndpoint.StorefrontApi.StatementUrl";
			}

			public static class FzBankAuthorization
			{
				public const string LoginUrl = "ApiEndpoint.FzBankAuthorization.LoginUrl";
				public const string LogoffUrl = "ApiEndpoint.FzBankAuthorization.LogoffUrl";
				public const string VerifyUrl = "ApiEndpoint.FzBankAuthorization.VerifyUrl";
			}
			public static class Crm
			{
				public const string CounterpartUrl = "ApiEndpoint.Crm.CounterpartUrl";
				public const string CounterpartCountUrl = "ApiEndpoint.Crm.CounterpartCountUrl";
			}
			public static class RetailApi
			{
				public const string CardsUrl = "ApiEndpoint.RetailApi.CardsUrl";
				public const string ClassifiersUrl = "ApiEndpoint.RetailApi.ClassifiersUrl";
				public const string SetClassifierUrl = "ApiEndpoint.RetailApi.SetClassifierUrl";
				public const string AgreementsUrl = "ApiEndpoint.RetailApi.AgreementsUrl";
				public const string AccountsUrl = "ApiEndpoint.RetailApi.AccountsUrl";
				public const string MoneyBoxesUrl = "ApiEndpoint.RetailApi.MoneyBoxesUrl";
				public const string BalancesUrl = "ApiEndpoint.RetailApi.BalancesUrl";
				public const string TransactionsUrl = "ApiEndpoint.RetailApi.TransactionsUrl";
				public const string CardsHistoryStatusUrl = "ApiEndpoint.RetailApi.CardsHistoryStatusUrl";
				public const string AgreementsHistoryStatusUrl = "ApiEndpoint.RetailApi.AgreementsHistoryStatusUrl";
				public const string CardBlockingEmail = "ApiEndpoint.RetailApi.CardBlockingEmail";
				public const string CardLimitEmail = "ApiEndpoint.RetailApi.CardLimitEmail";
				public const string CardLimitsUrl = "ApiEndpoint.RetailApi.CardLimitsUrl";
				public const string CardsChangeStatusUrl = "ApiEndpoint.RetailApi.CardsChangeStatusUrl";
				public const string CardSetLimitUrl = "ApiEndpoint.RetailApi.CardSetLimitUrl";

				public const string KeyCloakServiceUrl = "KeyCloak.RetailSystem.ServiceUrl";

				public const string KeyCloakRetailSystemPassword = "KeyCloak.RetailSystem.Password";

				public const string KeyCloakRetailSystemCardShirtSystem = "KeyCloak.RetailSystem.CardShirt.Gateway.WebApi.System";
				public const string KeyCloakRetailSystemCardShirtUserName = "KeyCloak.RetailSystem.CardShirt.Gateway.WebApi.UserName";
				public const string KeyCloakRetailSystemCardShirtService = "KeyCloak.RetailSystem.CardShirt.Gateway.WebApi.Service";
				public const string KeyCloakRetailSystemCardShirtServiceSecret = "KeyCloak.RetailSystem.CardShirt.Gateway.WebApi.ServiceSecret";

				public const string KeyCloakRetailSystemManagementSystem = "KeyCloak.RetailSystem.Management.Gateway.WebApi.System";
				public const string KeyCloakRetailSystemManagementUserName = "KeyCloak.RetailSystem.Management.Gateway.WebApi.UserName";
				public const string KeyCloakRetailSystemManagementService = "KeyCloak.RetailSystem.Management.Gateway.WebApi.Service";
				public const string KeyCloakRetailSystemManagementServiceSecret = "KeyCloak.RetailSystem.Management.Gateway.WebApi.ServiceSecret";

				public const string KeyCloakRetailSystemInformationSystem = "KeyCloak.RetailSystem.Information.Gateway.WebApi.System";
				public const string KeyCloakRetailSystemInformationUserName = "KeyCloak.RetailSystem.Information.Gateway.WebApi.UserName";
				public const string KeyCloakRetailSystemInformationService = "KeyCloak.RetailSystem.Information.Gateway.WebApi.Service";
				public const string KeyCloakRetailSystemInformationServiceSecret = "KeyCloak.RetailSystem.Information.Gateway.WebApi.ServiceSecret";
			}

			public static class AddressApi
			{
				public const string GetFullAddressUrl = "ApiEndpoint.AddressApi.GetFullAddressUrl";
			}

			public static class CountryApi
			{
				public const string GetCountryUrl = "ApiEndpoint.CountryApi.GetCountryUrl";
			}

			public static class BvrApi
			{
				public const string GetActiveDeviceUrl = "ApiEndpoint.BvrApi.GetActiveDeviceUrl";
				public const string GetResetPINDeviceUrl = "ApiEndpoint.BvrApi.GetResetPINDeviceUrl";
			}

			public static class ReferenceApi
			{
				public const string GetCurrencyUrl = "ApiEndpoint.ReferenceApi.GetCurrencyUrl";
				public const string GetCountriesUrl = "ApiEndpoint.ReferenceApi.GetCountriesUrl";
				public const string GetDivisionUrl = "ApiEndpoint.ReferenceApi.GetDivisionUrl";
			}

			public static class ApiServices
			{
				public static class SharedLink
				{
					public const string EncodedLinkUrl = "ApiEndpoint.ApiServices.SharedLink.EncodedLinkUrl";
				}
				public static class MDES
				{
					public const string TestCardNumber = "ApiEndpoint.ApiServices.MDES.TestCardNumber";
					public const string GetTokensByPanUrl = "ApiEndpoint.ApiServices.MDES.GetTokensByPanUrl";
					public const string GetHistoryByTokenUrl = "ApiEndpoint.ApiServices.MDES.GetHistoryByTokenUrl";
					public const string TokenDeleteUrl = "ApiEndpoint.ApiServices.MDES.TokenDeleteUrl";
					public const string TokenActivateUrl = "ApiEndpoint.ApiServices.MDES.TokenActivateUrl";
					public const string UserId = "ApiEndpoint.ApiServices.MDES.UserId";
				}
				public static class CardImage
				{
					public const string GetCardShirtUrl = "ApiEndpoint.ApiServices.CardImage.GetCardShirtUrl";
				}

				public static class BVR
				{
					public const string AddPermanentBlockingUrl = "ApiEndpoint.ApiServices.BVR.AddPermanentBlockingUrl";
				}
				public static class Statement
				{
					public const string Host = "ApiEndpoint.ApiServices.Statement.Host";
					public const string LogoutUrl = "ApiEndpoint.ApiServices.Statement.LogoutUrl";
					public const string Username = "ApiEndpoint.ApiServices.Statement.Username";
					public const string Password = "ApiEndpoint.ApiServices.Statement.Password";
					public const string LoginUrl = "ApiEndpoint.ApiServices.Statement.LoginUrl";
					public const string GenerateDocumentUrl = "ApiEndpoint.ApiServices.Statement.GenerateDocumentUrl";
					public const string GetDocumentUrl = "ApiEndpoint.ApiServices.Statement.GetDocumentUrl";
				}
			}

			public static class VostokMessengerProvider
			{
				public const string Url = "ApiEndpoint.VostokMessengerProvider.Url";
				public const string Login = "ApiEndpoint.VostokMessengerProvider.Login";
				public const string Password = "ApiEndpoint.VostokMessengerProvider.Password";
			}

			public static class EmailProvider
			{
				public const string From = "ApiEndpoint.EmailProvider.From";
				public static class Smtp 
				{
					public const string Host = "ApiEndpoint.EmailProvider.Smtp.Host";
					public const string Port = "ApiEndpoint.EmailProvider.Smtp.Port";
					public const string Login = "ApiEndpoint.EmailProvider.Smtp.Login";
					public const string Password = "ApiEndpoint.EmailProvider.Smtp.Password";
					public const string CheckCertRevocation = "ApiEndpoint.EmailProvider.Smtp.CheckCertRevocation";
				}

			}

			public static class CardTariff
			{
				public const string DownloadUrl = "ApiEndpoint.CardTariff.DownloadUrl";
			}

			public static class AgreementTariff
			{
				public const string DownloadUrl = "ApiEndpoint.AgreementTariff.DownloadUrl";
			}
		}

		public static class UiEndpoint
		{
			public static class Storefront
			{
				public const string ClientDetailUrl = "UiEndpoint.Storefront.ClientDetailUrl";
				public const string DetailUrl = "UiEndpoint.Storefront.DetailUrl";
			}
		}

		public static class Authorization
		{
			public static class JWT
			{
				public const string Key = "Authorization.JWT.Key";				
			}
		}
	}
}