using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Bootstrappers
{
	public static class CardBootstrapperExtension
	{
		public static void AddCardsRules(this IServiceCollection services)
		{
			services.AddTransient<CardService>();
			services.AddTransient<CardHistoryService>();
			services.AddTransient<SmsInfoService>();
			services.AddTransient<Secure3DService>();
			services.AddTransient<CardTariffService>();
			services.AddTransient<CardLimitService>();
			services.AddTransient<CardBlockingService>();
			services.AddTransient<CardTokenService>();
			services.AddTransient<CardUnlockingService>();

			services.AddScoped<CardMapper>();
			services.AddTransient<IRequestHandler<CardDetailQuery, CardViewModel>, CardDetailQueryHandler>();
			services.AddTransient<IRequestHandler<CardListQuery, SearchRowsResponseRowModel<CardViewModel>>, CardListQueryHandler>();

			services.AddTransient<IRequestHandler<CardBalanceQuery, CardBalanceViewModel>, CardBalanceQueryHandler>();
			services.AddTransient<IRequestHandler<CardExtendedBalanceQuery, CardExtendedBalanceViewModel>, CardExtendedBalanceQueryHandler>();

			services.AddSingleton<CardBalanceMapper>();
			services.AddTransient<IRequestHandler<CardHistoryStatusQuery, CardHistoryStatusViewModel>, CardHistoryStatusQueryHandler>();
			services.AddTransient<IRequestHandler<CardCurrentStatusQuery, CardCurrentStatusViewModel>, CardCurrentStatusQueryHandler>();

			services.AddTransient<IRequestHandler<SmsInfoQuery, SmsInfoViewModel>, SmsInfoQueryHandler>();
			services.AddTransient<IRequestHandler<SmsInfoChangeStatusTurnOnCommand, SmsInfoChangeStatusViewModel>, SmsInfoChangeStatusTurnCommandHandler>();
			services.AddTransient<IRequestHandler<SmsInfoChangeStatusTurnOffCommand, SmsInfoChangeStatusViewModel>, SmsInfoChangeStatusTurnOffCommandHandler>();
			
			services.AddTransient<IRequestHandler<Secure3DInfoQuery, Secure3DInfoViewModel>, Secure3DInfoQueryHandler>();
			services.AddTransient<IRequestHandler<CardFullBalanceQuery, CardFullBalanceViewModel>, CardFullBalanceQueryHandler>();
			services.AddTransient<IRequestHandler<Secure3DStatusHistoryQuery, Secure3DStatusHistoryViewModel>, Secure3DStatusHistoryQueryHandler>();

			services.AddTransient<IRequestHandler<SmsInfoHistoryStatusQuery, SmsInfoHistoryStatusViewModel>, SmsInfoHistoryStatusQueryHandler>();

			services.AddTransient<IRequestHandler<CardTariffDetailQuery, CardTariffViewModel>, CardTariffDetailQueryHandler>();
			services.AddTransient<IRequestHandler<CardTariffListQuery, SearchRowsResponseRowModel<CardTariffViewModel>>, CardTariffListQueryHandler>();
			services.AddTransient<IRequestHandler<CardTariffSendEmailCommand, StatusCommandViewModel>, CardTariffSendEmailCommandHandler>();
			services.AddTransient<IRequestHandler<CardTariffSendViberCommand, StatusCommandViewModel>, CardTariffSendViberCommandHandler>();

			services.AddTransient<CardLimitMapper>();
			services.AddTransient<IRequestHandler<CardLimitOfCashWithdrawalCommand, StatusCommandViewModel>, CardLimitOfCashWithdrawalCommandHandler>();
			services.AddTransient<IRequestHandler<CardLimitOfCashWithdrawalQuery, CardLimitOfCashWithdrawalViewModel>, CardLimitOfCashWithdrawalQueryHandler>();
			services.AddTransient<IRequestHandler<CardLimitRiskChangeCommand, StatusCommandViewModel>, CardLimitRiskChangeCommandHandler>();

			services.AddTransient<IRequestHandler<CardBlockingCommand, CardBlockingResultViewModel>, CardBlockingCommandHandler>();

			services.AddTransient<IRequestHandler<CardTokenListQuery, SearchRowsResponseRowModel<CardTokenViewModel>>, CardTokenListQueryHandler>();
			services.AddTransient<IRequestHandler<CardTokenLastStatusQuery, CardTokenLastStatusViewModel>, CardTokenLastStatusQueryHandler>();
			services.AddTransient<IRequestHandler<CardTokenDeleteCommand, StatusCommandViewModel>, CardTokenDeleteCommandHandler>();
			services.AddTransient<IRequestHandler<CardTokenActivateCommand, StatusCommandViewModel>, CardTokenActivateCommandHandler>();

			services.AddTransient<IRequestHandler<CardImageUrlQuery, CardImageUrlViewModel>, CardImageUrlQueryHandler>();

			services.AddTransient<IRequestHandler<CardDebtToBankQuery, CardDebtToBankViewModel>, CardDebtToBankQueryHandler>();

			services.AddTransient<IRequestHandler<CardUnlockingFailedCommand, StatusCommandViewModel>, CardUnlockingFailedCommandHandler>();
			services.AddTransient<IRequestHandler<CardUnlockingCommand, StatusCommandViewModel>, CardUnlockingCommandHandler>();
			services.AddTransient<IRequestHandler<CardUnlockingLockCommand, StatusCommandViewModel>, CardUnlockingLockCommandHandler>();
		}
	}
}
