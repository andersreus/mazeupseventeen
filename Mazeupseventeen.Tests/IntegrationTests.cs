using MessagePack.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Tests.Common.Testing;
using Umbraco.Cms.Tests.Integration.Testing;

namespace Mazeupseventeen.Tests;

[TestFixture]
[UmbracoTest(Database = UmbracoTestOptions.Database.NewSchemaPerTest)]
public class IntegrationTests : UmbracoIntegrationTest
{
    protected override void CustomTestSetup(IUmbracoBuilder builder)
    {
        //builder.Services.AddTransient<Interface, service>();
    }
}