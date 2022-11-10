using CoffeeMaker.Enums;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker.Classes;

public class M4ContainmentVessel : ContainmentVessel, Pollable
{
    private readonly ICoffeeMaker _api;
    private WarmerPlateStatus lastPotStatus;

    public M4ContainmentVessel(ICoffeeMaker coffeeMaker)
    {
        _api = coffeeMaker;
    }

    public override bool IsReady()
    {
        var status = _api.GetWarmerPlateStatus();

        return status == WarmerPlateStatus.POT_EMPTY;
    }

    public override void Start()
    {
        isBrewing = true;
    }

    public void Poll()
    {
        var potStatus = _api.GetWarmerPlateStatus();
        if (potStatus != lastPotStatus)
        {
            if (isBrewing)
            {
                HandleBrewingEvent(potStatus);
            }
            else if (!isComplete)
            {
                HandleIncompleteEvent(potStatus);
            }

            lastPotStatus = potStatus;
        }
    }

    private void HandleBrewingEvent(WarmerPlateStatus potStatus)
    {
        if (potStatus == WarmerPlateStatus.POT_NOT_EMPTY)
        {
            ContainerAvailable();
            _api.SetWarmerState(WarmerState.ON);
        }
        else if (potStatus == WarmerPlateStatus.WARMER_EMPTY)
        {
            ContainerUnavailable();
            _api.SetWarmerState(WarmerState.OFF);
        }
        else
        {
            ContainerAvailable();
            _api.SetWarmerState(WarmerState.OFF);
        }
    }

    private void HandleIncompleteEvent(WarmerPlateStatus potStatus)
    {
        if (potStatus == WarmerPlateStatus.POT_NOT_EMPTY)
        {
            _api.SetWarmerState(WarmerState.ON);
        }
        else if (potStatus == WarmerPlateStatus.WARMER_EMPTY)
        {
            _api.SetWarmerState(WarmerState.OFF);
        }
        else
        {
            _api.SetWarmerState(WarmerState.OFF);
            DeclareComplete();
        }
    }
}