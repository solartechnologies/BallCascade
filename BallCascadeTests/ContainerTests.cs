using BallCascade;
using BallCascade.Nodes;

namespace BallCascadeTests;
internal class ContainerTests
{
    [Test]
    public void ContainerAddNode_ShouldThrowAnException()
    {
        var sut = new Container(1);

        Assert.Throws<Exception>(() => sut.AddNode(new Container(2)));
    }

    [Test]
    public void ContainerPassBall_ShouldStoreBall()
    {
        var sut = new Container(1);

        sut.PassBall(new Ball());

        Assert.That(sut.GetBallCount(), Is.EqualTo(1));
    }
}
