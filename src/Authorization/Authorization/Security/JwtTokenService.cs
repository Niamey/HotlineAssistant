using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Vostok.Hotline.Core.Common.Configurations;

namespace Vostok.Hotline.Authorization.Security
{
	public class JwtTokenService
	{
		private HotlineEnvironment _env;

		private const string _issuer = "Vostok";
		private const string _audience = "Hotline";
		private const int _lifetimeInMinutes = 60;

		public JwtTokenService(HotlineEnvironment env)
		{
			_env = env;
		}

		public (string JwtToken, DateTime ExpiresAt) GenerateToken(IEnumerable<Claim> claims, TimeSpan? lifeTime = null)
		{
			lifeTime = lifeTime ?? TimeSpan.FromMinutes(_lifetimeInMinutes);

			var expiresAt = DateTime.UtcNow.Add(lifeTime.Value);

			var tokeOptions = new JwtSecurityToken(
			  issuer: _issuer,
			  audience: _audience,
			  claims: claims,
			  expires: expiresAt,
			  signingCredentials: GetSigningCredentials()
			);

			var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

			return (token, expiresAt);
		}

		public TokenValidationParameters GetTokenValidationParameters()
		{
			return new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = _issuer,
				ValidAudience = _audience,
				IssuerSigningKey = GetSecurityKey(),
				ClockSkew = TimeSpan.Zero
			};
		}

		private SecurityKey GetSecurityKey()
		{
			/*
			var thumbprint = _env.GetEnvironmentVariable("Authorization.JWT.Key");

			X509Certificate2 cert = CertificateHelper.FindByThumbprint(thumbprint, StoreName.My, StoreLocation.LocalMachine);

			SecurityKey key = new X509SecurityKey(cert);

			return key;*/

			var key = _env.GetEnvironmentVariable("Authorization.JWT.Key");
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
		}

		private SigningCredentials GetSigningCredentials()
		{
			/*
			var thumbprint = _env.GetEnvironmentVariable("Authorization.JWT.Key");

			X509Certificate2 cert = CertificateHelper.FindByThumbprint(thumbprint, StoreName.My, StoreLocation.LocalMachine);

			return new X509SigningCredentials(cert);*/

			var key = _env.GetEnvironmentVariable("Authorization.JWT.Key");
			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

			return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);			
		}
	}
}
