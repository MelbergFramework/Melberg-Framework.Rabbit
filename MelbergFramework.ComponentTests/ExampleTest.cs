using LightBDD.Framework.Scenarios;
using LightBDD.MsTest3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MelbergFramework.ComponentTests;

[TestClass]
public class ExampleTest : BaseTest
{
    [Scenario]
    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    [DataRow(5)]
    public async Task DemonstrateFunctionality(int value)
    {
        await Runner.RunScenarioAsync(
                _ => Setup_message(value),
                _ => Consume_message(),
                _ => Verify_value(value)
                );

    }

}
