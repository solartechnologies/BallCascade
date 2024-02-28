using BallCascade.Visitors;

namespace BallCascade;
public static class BallCascadeProcess
{
    public static void CascadeForDepth(int depth)
    {
        var factory = new BallCascadeFactory();
        var root = factory.BuildCascadeAtDepth(depth);
        var ballCount = factory.CalculateAvailableBalls(depth);

        var predictionAlgorithm = new ContainerPredictionVisitor(ballCount);
        root.Visit(predictionAlgorithm);

        var predictedContainer = predictionAlgorithm.GetPredictedContainer();
        Console.WriteLine($"The predicted empty container is number {predictedContainer?.ContainerNumber}");

        foreach (var ball in factory.AcquireBalls(ballCount))
        {
            root.PassBall(ball);
        }

        var emptyContainerFinder = new FindEmptyContainerVisitor();
        root.Visit(emptyContainerFinder);

        var emptyContainer = emptyContainerFinder.GetEmptyContainer();

        Console.WriteLine($"The actual empty container is number {emptyContainer?.ContainerNumber}");
    }
}
