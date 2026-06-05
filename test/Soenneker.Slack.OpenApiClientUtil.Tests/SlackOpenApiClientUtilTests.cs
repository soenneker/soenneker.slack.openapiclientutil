using Soenneker.Slack.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Slack.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class SlackOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ISlackOpenApiClientUtil _openapiclientutil;

    public SlackOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ISlackOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
