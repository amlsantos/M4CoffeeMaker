namespace CoffeeMaker.Classes;

public abstract class HotWaterSource
{
    private UserInterface userInterface;
    private ContainmentVessel containmentVessel;

    protected bool isBrewing;

    public HotWaterSource()
    {
        isBrewing = false;
    }

    public void Init(UserInterface ui, ContainmentVessel cv)
    {
        userInterface = ui;
        containmentVessel = cv;
    }

    public virtual void Start()
    {
        isBrewing = true;
        StartBrewing();
    }

    public void Done()
    {
        isBrewing = false;
    }
    protected void DeclareDone()
    {
        userInterface.Done();
        containmentVessel.Done();

        isBrewing = false;
    }

    public abstract bool IsReady();
    public abstract void StartBrewing();
    public abstract void Pause();
    public abstract void Resume();
}