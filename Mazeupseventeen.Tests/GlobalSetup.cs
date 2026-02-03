namespace Mazeupseventeen.Tests;

[SetUpFixture]
public class GlobalSetup
{
    private static GlobalSetupTeardown _setupTeardown;
    
    [OneTimeSetUp]
    public void SetUp() 
    {
        _setupTeardown = new GlobalSetupTeardown();
        _setupTeardown.SetUp();
    }
    
    [OneTimeTearDown]
    public void TearDown() 
    {
        _setupTeardown.TearDown();
    }
}