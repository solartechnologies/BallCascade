using BallCascade;
using BallCascade.Nodes;
using Moq;

namespace BallCascadeTests;
internal class IntersectionTests
{
    [Test]
    public void IntersectionAddNode_ShouldSetTheLeftBranchFirst()
    {
        var sut = new Intersection();

        sut.AddNode(new Container(1));

        Assert.NotNull(sut.GetBranch(isLeft: true));
    }

    [Test]
    public void IntersectionAddNode_ShouldSetTheRightBranchSecond()
    {
        var sut = new Intersection();

        sut.AddNode(new Container(1));
        sut.AddNode(new Container(2));

        Assert.NotNull(sut.GetBranch(isLeft: false));
    }

    [Test] 
    public void IntersectionAddNode_ShouldAddToLeftBranchFirst()
    {
        var sut = new Intersection();
        var moqContainer = new Mock<Intersection>(GateDirection.LEFT);
        var moqContainer2 = new Mock<Intersection>(GateDirection.LEFT);
        sut.AddNode(moqContainer.Object);
        sut.AddNode(moqContainer2.Object);
        sut.AddNode(new Intersection());


        moqContainer.Verify(x => x.AddNode(It.IsAny<IBranchNode>()), Times.Once());
        moqContainer2.Verify(x => x.AddNode(It.IsAny<IBranchNode>()), Times.Never());
    }

    [Test]
    public void IntersectionPassBall_ShouldPassToABranch()
    {
        var sut = new Intersection();
        var moqContainer = new Mock<Container>(1);
        var moqContainer2 = new Mock<Container>(2);
        sut.AddNode(moqContainer.Object);
        sut.AddNode(moqContainer2.Object);
        var originalGateDirection = sut.GateDirection;

        sut.PassBall(new Ball());

        if (originalGateDirection == GateDirection.LEFT)
        {
            moqContainer.Verify(x => x.PassBall(It.IsAny<Ball>()), Times.Once());
            moqContainer2.Verify(x => x.PassBall(It.IsAny<Ball>()), Times.Never());
        }
        else
        {
            moqContainer.Verify(x => x.PassBall(It.IsAny<Ball>()), Times.Never());
            moqContainer2.Verify(x => x.PassBall(It.IsAny<Ball>()), Times.Once());
        }
    }

    [Test]
    public void IntersectionPassBall_ShouldChangeGateDirection()
    {
        var sut = new Intersection();

        sut.AddNode(new Container(1));
        sut.AddNode(new Container(2));

        var gateDirection = sut.GateDirection;

        sut.PassBall(new Ball());

        Assert.That(sut.GateDirection, Is.Not.EqualTo(gateDirection));
    }
}
