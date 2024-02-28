using BallCascade;
using BallCascade.Nodes;

namespace BallCascadeTests;

public class BallCascadeFactoryTests
{
    [Test]
    public void WhenBuildCascadeAtDepth_Zero_ThrowArgumentException()
    {
        var sut = new BallCascadeFactory();

        Assert.Throws<ArgumentException>(() => sut.BuildCascadeAtDepth(0));
    }

    [TestCase(1, 2)]
    [TestCase(2, 4)]
    [TestCase(6, 64)]
    public void WhenCalculateAvailableBalls_OneLessThanContainers(int depth, int numberOfContainers)
    {
        var sut = new BallCascadeFactory();

        int ballCount = sut.CalculateAvailableBalls(depth);

        Assert.That(ballCount, Is.EqualTo(numberOfContainers - 1));
    }

    [TestCase(1, 3)]
    [TestCase(2, 7)]
    [TestCase(5, 63)]
    public void ForBuildCascadeAtDepth_NodeCountIsCorrect(int depth, int nodeCount)
    {
        var sut = new BallCascadeFactory();

        // assert correct depths
        var root = sut.BuildCascadeAtDepth(depth);

        var currCount = 0;
        var nodeList = new Queue<IBranchNode> ();
        nodeList.Enqueue (root);
        while(nodeList.Count > 0)
        {
            var node = nodeList.Dequeue();
            currCount++;
            if (node is Intersection intersection) {
                nodeList.Enqueue(intersection.GetBranch(isLeft: true)!);
                nodeList.Enqueue(intersection.GetBranch(isLeft: false)!);
            }
        }

        Assert.That(currCount, Is.EqualTo(nodeCount));
    }
}