using BallCascade.Nodes;

namespace BallCascade.Visitors;
public class FindEmptyContainerVisitor: INodeVisitor
{
    private Container? emptyContainer;

    public FindEmptyContainerVisitor() { }

    public void VisitIntersection(Intersection intersection)
    {
        if (emptyContainer == null)
        {
            intersection.GetBranch(isLeft: true)?.Visit(this);
        }

        if (emptyContainer == null) 
        {
            intersection.GetBranch(isLeft: false)?.Visit(this);
        }
    }

    public void VisitContainer(Container container) 
    {
        if (container.GetBallCount() == 0)
        {
            emptyContainer = container;
        }
    }

    public Container? GetEmptyContainer()
    {
        return emptyContainer;
    }
}
