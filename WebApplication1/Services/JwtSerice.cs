using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Services;

public class JwtService
{   
    private readonly string _key;
    private readonly string _issuer;    
    private readonly string _audience;
    public JwtService(IConfiguration configuration)
    {
        _key  = configuration["Jwt:key"];
        _issuer = configuration["Jwt:Issuer"];  
        _audience = configuration["Jwt:Audience"];
    }

    public string GenerateSecurityToken(string email,List<string> listaRaluri)
    {

        var securityKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),

            //ar putea sa vina din BD + altele...
            new Claim("Date", DateTime.Now.ToString()),
            new Claim(JwtRegisteredClaimNames.FamilyName, "zzzzzzzzzz")
        };
        foreach (var rol in listaRaluri)
        {
            claims.Add(new Claim(ClaimTypes.Role, rol));
        }
       

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

