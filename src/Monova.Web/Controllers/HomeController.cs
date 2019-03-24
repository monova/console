using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monova.Entity;

namespace Monova.Web.Controllers
{
	public class HomeController : SecureController
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
