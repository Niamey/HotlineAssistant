using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Authorization.ViewModels;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Security;
using Vostok.Hotline.Data.EF.Entities.Core;
using Vostok.Hotline.Data.Repository.Core.Managers.Base;
using Vostok.Hotline.Data.Repository.Core.Mapper;

namespace Vostok.Hotline.Data.Repository.Core.Managers
{
	public class UserProfileManager : BaseCoreManager
	{
		public UserProfileManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{

		}

		public UserProfileViewModel GetUserProfile(Guid userSessionId)
		{
			var result = DbContext.UserProfiles.Where(x => x.UserSessionId == userSessionId).FirstOrDefault();
			if (result != null)
				return result.ToUserProfileModel();
			else
				return null;
		}

		public async Task<UserProfileViewModel> VerifyLoginAsync(Guid userSessionId, string login, CancellationToken cancellationToken)
		{
			var entity = await DbContext.UserProfiles.Where(x => x.UserSessionId == userSessionId).FirstOrDefaultAsync(cancellationToken);
			if (entity == null || entity.Login != login)
				throw new SessionExpiredRequestException();

			entity.LastDateLogin = DateTime.Now;
			entity.UnSuccessfulLogin = 0;

			using (var transaction = TransactionFactory.Create())
			{				
				var res = await DbContext.SaveChangesAsync(SessionContext, cancellationToken);
				transaction.Complete();
			}

			return entity.ToUserProfileModel();
		}

		public async Task LoginAsync(AuthorizationStateEnum authorizationState, Guid userSessionId, UserProfileModel userProfile, CancellationToken cancellationToken)
		{
			var entity = await DbContext.UserProfiles.Where(x => x.UserSessionId == userSessionId).FirstOrDefaultAsync(cancellationToken);
			if (entity == null)
			{
				entity = await DbContext.UserProfiles.Where(x => x.Login == userProfile.Login).FirstOrDefaultAsync(cancellationToken);
				if (entity == null)
				{
					entity = new UserProfile();
					DbContext.UserProfiles.Add(entity);
				}
			}
			else
			{
				if (entity.Login != userProfile.Login)
					throw new SessionExpiredRequestException();
			}

			entity.UserSessionId = userSessionId;
			entity.Login = userProfile.Login;
			entity.Email = userProfile.Email;
			entity.FullName = userProfile.FullName;
			entity.Position = userProfile.Position;
			entity.Avatar = Converter.FromBase64String(userProfile.AvatarBase64);
			entity.SuccessfulDateLogin = DateTime.Now;

			if (authorizationState == AuthorizationStateEnum.Success)
			{
				entity.LastDateLogin = DateTime.Now;
				entity.UnSuccessfulLogin = 0;
			}
			else 
			{
				entity.UnSuccessfulLogin++;
			}

			using (var transaction = TransactionFactory.Create())
			{				
				var res = await DbContext.SaveChangesAsync(SessionContext, cancellationToken);
				transaction.Complete();
			}
		}

		public async Task LogoutAsync(Guid userSessionId, CancellationToken cancellationToken)
		{
			var entity = await DbContext.UserProfiles.Where(x => x.UserSessionId == userSessionId).FirstOrDefaultAsync(cancellationToken);
			if (entity == null)
				throw new SessionExpiredRequestException();

			entity.UserSessionId = null;

			using (var transaction = TransactionFactory.Create())
			{
				var res = await DbContext.SaveChangesAsync(SessionContext, cancellationToken);
				transaction.Complete();
			}
		}
	}
}
