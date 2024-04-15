using MelbergFramework.Infrastructure.Rabbit.Extensions;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests;

[TestClass]
public class MessageExtensionsUnitTests
{
    [TestMethod]
    [DataRow(1)]
    [DataRow(1000)]
    [DataRow(13021231)]
    [DataRow(1212234)]
    public void ConfirmDateHandling(int offset)
    {
        var date = DateTime.UnixEpoch.AddSeconds(offset);
        var message = new Message();
        message.SetTimestamp(date);
        var interpretedDate = message.GetTimestamp();

        Assert.AreEqual(date, interpretedDate);
    }

    [TestMethod]
    public void SetCoidTest()
    {
        var guid = Guid.NewGuid();

        var message = new Message();
        message.SetCoID(guid);

        Assert.AreEqual(guid, message.GetCoID());

    }

}
