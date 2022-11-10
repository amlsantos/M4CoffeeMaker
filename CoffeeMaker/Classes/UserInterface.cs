namespace CoffeeMaker.Classes;

public abstract class UserInterface
{
    private HotWaterSource hotWaterSource;
    private ContainmentVessel containmentVessel;

    protected bool isComplete;

    public UserInterface()
    {
        isComplete = true;
    }

    public void Init(HotWaterSource hws, ContainmentVessel cv)
    {
        hotWaterSource = hws;
        containmentVessel = cv;
    }

    public void Complete()
    {
        isComplete = true;
        CompleteCycle();
    }

    protected void StartBrewing()
    {
        if (hotWaterSource.IsReady() && containmentVessel.IsReady())
        {
            isComplete = false;

            hotWaterSource.Start();
            containmentVessel.Start();
        }
    }

    public abstract void Done();
    public abstract void CompleteCycle();
}