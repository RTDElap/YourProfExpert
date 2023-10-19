using YourProfExpert.Core.Tests;

namespace YourProfExpert.TestOfKlimov;

public class KlimovTest : FunctionalTest
{
    private readonly Random _random;

    public KlimovTest(Random? random = null) : base(KlimovTestData.KLIMOV_TITLE, KlimovTestData.KLIMOV_DESCRIPTION)
    {
        _random = random ?? new Random();
    }

    public override TestExecutor CreateExecutor()
    {
        return new KlimovTestExecutor( this, _random );
    }
}