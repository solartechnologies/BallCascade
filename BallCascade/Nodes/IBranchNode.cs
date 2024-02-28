using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BallCascade.Visitors;

namespace BallCascade.Nodes;

public interface IBranchNode
{
    void PassBall(Ball ball);
    void AddNode(IBranchNode node);
    void Visit(INodeVisitor visitor);
}
