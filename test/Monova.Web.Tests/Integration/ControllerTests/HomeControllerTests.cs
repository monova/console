using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Monova.Web.Tests.Integration
{
	public class HomeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly WebApplicationFactory<Startup> _factory;
		private readonly HttpClient _client;

		public HomeControllerTests(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
			_client = Helper.GetClient(_factory);
		}

		[Fact]
		public async Task HomeIndexShouldRedirectToLogin()
		{
			var response = await _client.GetAsync("/Home/Index");
			response.IsSuccessStatusCode.Should().BeFalse();
			response.Headers.Location.AbsoluteUri.Should().StartWith("http://localhost/Identity/Account/Login");
			response.StatusCode.Should().Be(302);
		}
	}
}