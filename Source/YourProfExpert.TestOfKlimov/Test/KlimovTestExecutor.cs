#nullable disable

using YourProfExpert.Core.Tests;

namespace YourProfExpert.KlimovTest;

public class KlimovTestExecutor : TestExecutor
{
    public KlimovTestExecutor(KlimovTest test) : base(test)
    {
        
    }

    public override int GetResult()
    {
        int[] scores = (Test as KlimovTest).Scores;

        return Array.IndexOf(scores, scores.Max()) + 1;
    }
}