using BallCascade.Nodes;
namespace BallCascade.Visitors;
public class ContainerPredictionVisitor: INodeVisitor
{
    private readonly int ballCount;
    private Container? predictedContainer;

    public ContainerPredictionVisitor(int ballCount)
    {
        this.ballCount = ballCount;
    }

    public void VisitIntersection(Intersection intersection)
    {
        var emptyNodeisLeft = intersection.GateDirection != GateDirection.LEFT;
        var nextIntersection = intersection.GetBranch(isLeft: emptyNodeisLeft);
        if (nextIntersection == null)
        {
            throw new Exception("Unexpected null intersection");
        }
        
        nextIntersection.Visit(this);
    }

    public void VisitContainer(Container container)
    {
        predictedContainer = container;
    }

    public Container? GetPredictedContainer()
    {
        return predictedContainer;
    }
}
