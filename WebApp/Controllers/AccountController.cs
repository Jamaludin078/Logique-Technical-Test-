using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Data.Models;
using Web.Data.Services;

namespace WebApp.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		readonly IWebHostEnvironment environment;
		readonly string scheme = CookieAuthenticationDefaults.AuthenticationScheme;

		private readonly UserService service;

		public AccountController(UserService service) => this.service = service;

		private bool Validate(UserLogin request)
		{
			return request.Email == "test@gmail.com" && request.Password == "secret";
		}

		public IActionResult Login([FromQuery] string ReturnUrl = null)
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}

			TempData["q"] = ReturnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLogin user)
		{

			var valid = await service.Get(user.Email, user.Password);

			if (valid == null && !Validate(user))
			{
				return View();
			}


			var claims = new List<Claim>();


			if (valid != null)
			{
				var userDetail = await service.Get(valid.UserId);


				claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, userDetail.FirstName != null ? userDetail.FirstName : userDetail.LastName, ClaimValueTypes.String, null),
					new Claim(ClaimTypes.NameIdentifier, userDetail.UserId, ClaimValueTypes.String, null),
				};
			}
			else
			{
				claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, "Administrator", ClaimValueTypes.String, null),
					new Claim(ClaimTypes.NameIdentifier, "", ClaimValueTypes.String, null),
				};
			}
			int hours = 10;

			var userIdentity = new ClaimsIdentity(claims, "AuthenticationTypes.Federation");
			await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(userIdentity),
			new AuthenticationProperties
			{
				ExpiresUtc = DateTime.UtcNow.AddMinutes(hours),
				IsPersistent = true,
				AllowRefresh = true
			});

			string returnUrl = Convert.ToString(TempData["q"]);

			if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("index", "home");
			}
		}

		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(scheme);
			HttpContext.Session.Clear();

			return RedirectToAction("index", "home");
		}
	}
}
