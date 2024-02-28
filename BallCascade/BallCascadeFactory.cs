using BallCascade.Nodes;

namespace BallCascade;
public class BallCascadeFactory
{
    private int containerSequence;
    private Random random;

    public BallCascadeFactory() 
    { 
        containerSequence = 0;
        random = new Random();
    }

    public IEnumerable<Ball> AcquireBalls(int ballCount)
    {
        for (int i = 0; i < ballCount; i++)
        {
            yield return new Ball();
        }
    }

    public int CalculateAvailableBalls(int depth)
    {
        validateDepth(depth);

        return (int)Math.Pow(2, depth) - 1;
    }

    public IBranchNode BuildCascadeAtDepth(int depth)
    {
        validateDepth(depth);

        var root = createIntersection();
        for (int i = 1; i <= depth; i++)
        {
            var nodeCount = Math.Pow(2, i);
            Func<IBranchNode> nodeBuilder = i == depth ? createContainer : createIntersection;

            for (int j = 0; j < nodeCount; j++)
            {
                root.AddNode(nodeBuilder());
            }
        }

        return root;
    }

    private void validateDepth(int depth)
    {
        if (depth == 0)
        {
            throw new ArgumentException("Depth must be greater than 0");
        }
    }

    private IBranchNode createIntersection()
    {
        var gateDirection = random.NextDouble() > 0.5 ? GateDirection.LEFT : GateDirection.RIGHT;
        return new Intersection(gateDirection);
    }

    private IBranchNode createContainer()
    {
        return new Container(containerSequence++);
    }
}
