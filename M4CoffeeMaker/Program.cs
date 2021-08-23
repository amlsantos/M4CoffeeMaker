using CoffeeMaker.Classes;

namespace M4CoffeeMaker
{
    static class Program
    {
        static void Main(string[] args)
        {
            var api = new CoffeeMakerAPI();
            var ui = new M4UserInterface(api);
            var hws = new M4HotWaterSource(api);
            var cv = new M4ContainmentVessel(api);

            ui.Init(hws, cv);
            hws.Init(ui, cv);
            cv.Init(ui, hws);

            while (true)
            {
                ui.Poll();
                hws.Poll();
                cv.Poll();
            }
        }
    }
}
