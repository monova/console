using System;
using FluentAssertions;
using Xunit;

namespace Monova.Web.Tests
{
	public class EnvironmentVariableTest
	{
		[Fact]
		public void ConnectionStringMustBeInEnvironmentVariable()
		{
			var myCustomValue = "ABCDEFGH";
			Environment.SetEnvironmentVariable(Keys.ConnectionString, myCustomValue);
			var value = Environment.GetEnvironmentVariable(Keys.ConnectionString);
			value.Should().NotBeNullOrEmpty();
			value.Should().Be(myCustomValue);
		}

		[Fact]
		public void StripePublicKeyMustBeInEnvironmentVariable()
		{
			var myCustomValue = "XYZ";
			Environment.SetEnvironmentVariable(Keys.StripePublicKey, myCustomValue);
			var value = Environment.GetEnvironmentVariable(Keys.StripePublicKey);
			value.Should().NotBeNullOrEmpty();
			value.Should().Be(myCustomValue);
		}

		[Fact]
		public void StripeApiKeyMustBeInEnvironmentVariable()
		{
			var myCustomValue = "TEST";
			Environment.SetEnvironmentVariable(Keys.StripeApiKey, myCustomValue);
			var value = Environment.GetEnvironmentVariable(Keys.StripeApiKey);
			value.Should().NotBeNullOrEmpty();
			value.Should().Be(myCustomValue);
		}
	}
}