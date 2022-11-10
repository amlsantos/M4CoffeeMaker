using CoffeeMaker.Enums;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker.Classes;

public class M4HotWaterSource : HotWaterSource, Pollable
{
    private readonly ICoffeeMaker _api;

    public M4HotWaterSource(ICoffeeMaker coffeeMaker)
    {
        _api = coffeeMaker;
    }

    public override bool IsReady()
    {
        var status = _api.GetBoilerStatus();

        return status == BoilerStatus.NOT_EMPTY;
    }

    public override void StartBrewing()
    {
        _api.SetReliefValveState(ReliefValveState.CLOSED);
        _api.SetBoilerState(BoilerState.ON);
    }

    public void Poll()
    {
        var boilerStatus = _api.GetBoilerStatus();

        if (isBrewing && boilerStatus == BoilerStatus.EMPTY)
        {
            _api.SetBoilerState(BoilerState.OFF);
            _api.SetReliefValveState(ReliefValveState.CLOSED);

            DeclareDone();
        }
    }

    public override void Pause()
    {
        _api.SetBoilerState(BoilerState.OFF);
        _api.SetReliefValveState(ReliefValveState.OPEN);
    }

    public override void Resume()
    {
        _api.SetBoilerState(BoilerState.ON);
        _api.SetReliefValveState(ReliefValveState.CLOSED);
    }
}