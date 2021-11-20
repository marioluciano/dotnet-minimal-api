public class AuthenticationHelper
{
    ConfigurationManager _configuration;
    public AuthenticationHelper(ConfigurationManager configuration)
    {
        _configuration = configuration;
    }

    public string Login(Login login)
    {
        if (login.username == "admin" && login.password == "password")
            return GenerateJWT();
        else
            return "";
    }

    private string GenerateJWT()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:ValidIssuer"],
        audience: _configuration["Jwt:ValidAudience"],
        claims: null,
        expires: System.DateTime.Now.AddMinutes(120),
        signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}