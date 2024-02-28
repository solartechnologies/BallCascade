using BallCascade.Nodes;

namespace BallCascade.Visitors;
public interface INodeVisitor
{
    void VisitIntersection(Intersection intersection);
    void VisitContainer(Container container);
}
