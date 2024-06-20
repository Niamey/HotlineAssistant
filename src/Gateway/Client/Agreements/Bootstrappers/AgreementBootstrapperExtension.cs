using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Agreements.Mapper;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.Services;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Bootstrappers
{
	public static class AgreementBootstrapperExtension
	{
		public static void AddAgreementsRules(this IServiceCollection services)
		{
			services.AddTransient<AgreementMapper>();
			services.AddTransient<AgreementService>();
			services.AddTransient<AgreementHistoryService>();
			services.AddTransient<AgreementTariffService>();
			services.AddTransient<MoneyBoxService>();


			services.AddTransient<IRequestHandler<AgreementDetailQuery, AgreementViewModel>, AgreementDetailQueryHandler>();
			services.AddTransient<IRequestHandler<AgreementListQuery, SearchRowsResponseRowModel<AgreementViewModel>>, AgreementListQueryHandler>();
			services.AddTransient<IRequestHandler<AgreementHistoryStatusQuery, AgreementHistoryStatusViewModel>, AgreementHistoryStatusQueryHandler>();

			services.AddTransient<IRequestHandler<AgreementTariffDetailQuery, AgreementTariffViewModel>, AgreementTariffDetailQueryHandler>();
			services.AddTransient<IRequestHandler<AgreementTariffListQuery, SearchRowsResponseRowModel<AgreementTariffViewModel>>, AgreementTariffListQueryHandler>();
			services.AddTransient<IRequestHandler<AgreementTariffSendEmailCommand, StatusCommandViewModel>, AgreementTariffSendEmailCommandHandler>();
			services.AddTransient<IRequestHandler<AgreementTariffSendViberCommand, StatusCommandViewModel>, AgreementTariffSendViberCommandHandler>();

			services.AddTransient<AgreementBalanceMapper>();
			services.AddTransient<AgreementMoneyBoxMapper>();
			services.AddTransient<IRequestHandler<AgreementBalanceQuery, AgreementBalanceViewModel>, AgreementBalanceQueryHandler>();
			services.AddTransient<IRequestHandler<AgreementMoneyBoxListQuery, SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>>, AgreementMoneyBoxListQueryHandler>();
		}
	}
}
