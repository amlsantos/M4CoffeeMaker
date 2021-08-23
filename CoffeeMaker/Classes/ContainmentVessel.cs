namespace CoffeeMaker.Classes
{
    public abstract class ContainmentVessel
    {
        private UserInterface userInterface;
        private HotWaterSource hotWaterSource;

        protected bool isBrewing;
        protected bool isComplete;

        public ContainmentVessel()
        {
            isBrewing = false;
            isComplete = false;
        }

        public void Init(UserInterface ui, HotWaterSource hws)
        {
            userInterface = ui;
            hotWaterSource = hws;
        }

        public virtual void Start()
        {
            isBrewing = true;
            isComplete = false;
        }

        public void Done()
        {
            isBrewing = false;
        }

        protected void DeclareComplete()
        {
            isComplete = true;
            userInterface.Complete();
        }

        protected void ContainerAvailable()
        {
            hotWaterSource.Resume();
        }

        protected void ContainerUnavailable()
        {
            hotWaterSource.Pause();
        }

        public abstract bool IsReady();
    }
}
