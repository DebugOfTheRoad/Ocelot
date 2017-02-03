using System.Collections.Generic;
using Ocelot.ServiceDiscovery;
using Ocelot.Values;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.ServiceDiscovery
{
    public class ConfigurationServiceProviderTests
    {
        private ConfigurationServiceProvider _serviceProvider;
        private HostAndPort _hostAndPort;
        private List<Service> _result;
        private List<Service> _expected;

        [Fact]
        public void should_return_services()
        {
            var hostAndPort = new HostAndPort("127.0.0.1", 80);

            var services = new List<Service>
            {
                new Service("product", hostAndPort)
            };

            this.Given(x => x.GivenServices(services))
                .When(x => x.WhenIGetTheService())
                .Then(x => x.ThenTheFollowingIsReturned(services))
                .BDDfy();
        }

        private void GivenServices(List<Service> services)
        {
            _expected = services;
        }

        private void WhenIGetTheService()
        {
            _serviceProvider = new ConfigurationServiceProvider(_expected);
            _result = _serviceProvider.Get();
        }

        private void ThenTheFollowingIsReturned(List<Service> services)
        {
            _result[0].HostAndPort.DownstreamHost.ShouldBe(services[0].HostAndPort.DownstreamHost);

            _result[0].HostAndPort.DownstreamPort.ShouldBe(services[0].HostAndPort.DownstreamPort);

            _result[0].Name.ShouldBe(services[0].Name);
        }
    }
}