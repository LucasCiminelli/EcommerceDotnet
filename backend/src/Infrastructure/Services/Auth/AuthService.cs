using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Models.Token;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {

        public JwtSettings _jwtSettings { get; }
        private readonly IHttpContextAccessor _httpContextAccessor; //canal de comunicación que recibe el request del cliente.

        public AuthService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor)
        {
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }


        public string CreateToken(Usuario usuario, IList<string>? roles)
        {
            var claims = new List<Claim>
                { //el claim es similir dictionary, key value.
                    new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName!), //el nameId representa al NameIdentifier que luego usare en el metodo GetSessionUser
                    new Claim("userId", usuario.Id), //crear un custom Claim para userId
                    new Claim("email", usuario.Email!) //crear un custom Claim para email
                };

            foreach (var rol in roles!)
            {
                var claim = new Claim(ClaimTypes.Role, rol); //por cada valor enviado en la lista de roles por parametro, creo un claim de tipo rol con el valor de ese rol.
                claims.Add(claim); //lo agrego al listado de claims
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!)); //key para desencriptar el token, viene de jwtSettings.Key codificado en UTF8.
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //creo credenciales de Signin pasandole el key y el algoritmo para desencriptar.

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), //los claims van en el Subject.
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime), //cuando expira
                SigningCredentials = credentials //firma digital. Viene de la variable credentials.
            };

            var tokenHandler = new JwtSecurityTokenHandler(); //crear el handler para crear el token. Convertir el token en una representación de strings.
            var token = tokenHandler.CreateToken(tokenDescription);



            return tokenHandler.WriteToken(token); //devolverlo como si fuese un string en secuencia hexadecimal en formato string. Contiene toda la data y seguridad.
        }

        public string GetSessionUser()
        {
            var username = _httpContextAccessor.HttpContext!.User?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            //cada propiedad dentro del token es un claim. Busco dentro ed este listado de claims, el claimType del username que es NameIdentifier

            return username!;
        }
    }
}