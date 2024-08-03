using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Models;
using Web.Data.Services;

namespace WebApp.Controllers
{
	[AllowAnonymous]
	public class RegisterController : Controller
	{
		//readonly IWebHostEnvironment environment;
		//readonly string scheme = CookieAuthenticationDefaults.AuthenticationScheme;
		private readonly UserService service;

		public RegisterController(UserService service) => this.service = service;
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> RegisterMember(User TEntity)
		{
			try
			{
				await service.AddAsync(TEntity);

				return RedirectToAction("Login", "Account");
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index");
			}

		}

		[HttpPost]
		public async Task<JsonResult> Add(User TEntity)
		{
			try
			{
				var res = await service.AddAsync(TEntity);

				//await service.AddAsync(TEntity);
				return Json(new { Success = true, Message = "Success", Data = res });
			}
			catch (Exception ex)
			{
				return Json(new { Success = false, Message = "", Data = "" });
			}
		}
	}
}
