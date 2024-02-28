using BallCascade.Visitors;

namespace BallCascade.Nodes;

public class Container : IBranchNode
{
    public readonly int ContainerNumber;
    private IList<Ball> balls;

    public Container(int containerNumber)
    {
        balls = new List<Ball>();
        ContainerNumber = containerNumber;
    }

    public virtual void PassBall(Ball ball)
    {
        balls.Add(ball);
    }

    public void AddNode(IBranchNode node)
    {
        throw new Exception("This node should only be a leaf");
    }

    public void Visit(INodeVisitor visitor)
    {
        visitor.VisitContainer(this);
    }

    public int GetBallCount() { return balls.Count; }
}
