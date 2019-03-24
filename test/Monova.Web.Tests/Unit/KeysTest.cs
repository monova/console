using System;
using FluentAssertions;
using Xunit;

namespace Monova.Web.Tests
{
	public class KeysTest
	{
		[Fact]
		public void ConnectionStringKeyMustEqual()
		{
			Monova.Web.Keys.ConnectionString.Should().Be("MONOVA_CONNECTIONSTRING");
		}

		[Fact]
		public void StripeApiKeyMustEqual()
		{
			Monova.Web.Keys.StripeApiKey.Should().Be("STRIPE_API_KEY");
		}

		[Fact]
		public void StripePublicKeyMustEqual()
		{
			Monova.Web.Keys.StripePublicKey.Should().Be("STRIPE_PUBLIC_KEY");
		}
	}
}
