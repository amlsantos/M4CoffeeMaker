using CoffeeMaker.Interfaces;
using Enums;

namespace CoffeeMaker.Classes
{
    public class M4UserInterface : UserInterface, Pollable
    {
        private readonly ICoffeeMaker _api;

        public M4UserInterface(ICoffeeMaker cm)
        {
            _api = cm;
        }

        private void CheckButton()
        {
            var status = _api.GetBrewButtonStatus();
            if (status == BrewButtonStatus.PUSHED)
            {
                base.StartBrewing();
            }
        }

        public void Poll()
        {
            var status = _api.GetBrewButtonStatus();
            if (status == BrewButtonStatus.PUSHED)
            {
                base.StartBrewing();
            }
        }

        public override void Done()
        {
            _api.SetIndicatorState(IndicatorState.ON);
        }

        public override void CompleteCycle()
        {
            _api.SetIndicatorState(IndicatorState.OFF);
        }
    }
}
