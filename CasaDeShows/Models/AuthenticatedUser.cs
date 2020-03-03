using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDeShows.Models
{
    public class AuthenticatedUser
{
	private readonly IHttpContextAccessor _accessor;

	public AuthenticatedUser(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public string Email => _accessor.HttpContext.User.Identity.Name;
	public string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

	public IEnumerable<Claim> GetClaimsIdentity()
	{
		return _accessor.HttpContext.User.Claims;
	}
    public void ConfigureServices(IServiceCollection services)
    {
	// ... restante do método ocultado

	services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
	services.AddScoped<AuthenticatedUser>();
    }   
}


}