using YourProfExpert.Core.Tests;
using YourProfExpert.Core.Tests.Builders;

namespace YourProfExpert.KlimovTest;

public class KlimovTest : Test
{
    private readonly Random _random;

    public KlimovTest(Random? random = null) : base(KlimovTestData.KLIMOV_TITLE, KlimovTestData.KLIMOV_DESCRIPTION)
    {
        _random = random ?? new Random();
    }

    public KlimovTest(IList<Question> questions, Random? random = null) : base(KlimovTestData.KLIMOV_TITLE, KlimovTestData.KLIMOV_DESCRIPTION)
    {
        _random = random ?? new Random();
    }

    public override TestExecutor CreateExecutor()
    {
        return new KlimovTestExecutor( new KlimovTest(_random) );
    }
}

internal enum TypeKlimov
{
    Nature,
    Technique,
    SignSystem,
    ArtisticImage,
    Man
}