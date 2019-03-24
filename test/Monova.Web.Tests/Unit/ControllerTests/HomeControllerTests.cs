using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Monova.Web.Controllers;
using Moq;
using Xunit;

namespace Monova.Web.Tests.Unit
{
	public class HomeControllerTests
	{
		[Fact]
		public void HomeIndexShouldReturnView()
		{
			var controller = Helper.GetController<HomeController>();
			var result = controller.Index();
			result.Should().BeOfType<ViewResult>();
		}

		[Fact]
		public void HomeErrorShouldReturnView()
		{
			var controller = Helper.GetController<HomeController>();
			var result = controller.Error();
			result.Should().BeOfType<ViewResult>();
		}
	}
}