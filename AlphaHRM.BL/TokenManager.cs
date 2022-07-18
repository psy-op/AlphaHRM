using AlphaHRM.DAL;
using AlphaHRM.Intereface;
using AlphaHRM.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.BL
{
    public class TokenManager : ITokenManager
    {
		private readonly IConfiguration iconfiguration;
		private readonly EFContext dbcontext;

		public TokenManager(EFContext dbcontext, IConfiguration iconfiguration)
		{
			this.dbcontext = dbcontext;
			this.iconfiguration = iconfiguration;
		}
		public Tokens Authenticate(LoginRequest users)
		{
			var userentity =  dbcontext.User.FirstOrDefault(user => user.Email == users.Email);

			if (userentity == null) { return null; }


			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, users.Email)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
	}
}
