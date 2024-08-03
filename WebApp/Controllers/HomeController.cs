using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Data.Services;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly UserService service;

        //public HomeController(UserService service) => this.service = service;
        public HomeController(ILogger<HomeController> logger, UserService service)
		{
			this.service = service;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			var data = await service.Get();

			return View(data);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}