using BallCascade.Visitors;

namespace BallCascade.Nodes;


/// <summary>
/// Sentinal values for Gate directions
/// </summary>
public enum GateDirection
{
    LEFT = 0,
    RIGHT = 1
}

/// <summary>
/// Represents and intersection in the ball cascade
/// </summary>
public class Intersection : IBranchNode
{
    public GateDirection GateDirection;
    private IBranchNode? branchLeft;
    private IBranchNode? branchRight;
    private bool isLeft;

    public Intersection(GateDirection? gateDirection = null)
    {
        GateDirection = gateDirection ?? GateDirection.LEFT;
        isLeft = true;
    }

    public void PassBall(Ball ball)
    {
        if (branchLeft == null || branchRight == null)
        {
            throw new Exception("Intersection does not have two branches");
        }

        if (GateDirection == GateDirection.LEFT)
        {
            branchLeft.PassBall(ball);
        }
        else
        {
            branchRight.PassBall(ball);
        }

        GateDirection = GateDirection == GateDirection.LEFT ? GateDirection.RIGHT : GateDirection.LEFT;
    }

    public virtual void AddNode(IBranchNode node)
    {
        ref IBranchNode? branch = ref branchLeft;
        if (!isLeft)
        {
            branch = ref branchRight;
        }

        if (branch == null)
        {
            branch = node;
        }
        else
        {
            branch.AddNode(node);
        }

        isLeft = !isLeft;
    }

    public void Visit(INodeVisitor visitor)
    {
        visitor.VisitIntersection(this);
    }

    /// <summary>
    /// method to allow testing of private branch fields
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public IBranchNode? GetBranch(bool isLeft)
    {
        return isLeft ? branchLeft : branchRight;
    }
}
