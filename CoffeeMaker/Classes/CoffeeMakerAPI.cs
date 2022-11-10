using CoffeeMaker.Enums;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker.Classes;

public class CoffeeMakerAPI : ICoffeeMaker
{
    public bool buttonPressed;
    public bool lightOn;
    public bool boilerOn;
    public bool valveClosed;
    public bool plateOn;
    public bool boilerEmpty;
    public bool potPresent;
    public bool potNotEmpty;

    public CoffeeMakerAPI()
    {
        buttonPressed = false;
        lightOn = false;
        boilerOn = false;
        valveClosed = true;
        plateOn = false;
        boilerEmpty = true;
        potPresent = true;
        potNotEmpty = false;
    }

    public WarmerPlateStatus GetWarmerPlateStatus()
    {
        if (!potPresent)
            return WarmerPlateStatus.WARMER_EMPTY;
        else if (potNotEmpty)
            return WarmerPlateStatus.POT_NOT_EMPTY;
        else
            return WarmerPlateStatus.POT_EMPTY;
    }
    public BoilerStatus GetBoilerStatus()
    {
        return boilerEmpty ?
            BoilerStatus.EMPTY : BoilerStatus.NOT_EMPTY;
    }
    public BrewButtonStatus GetBrewButtonStatus()
    {
        if (buttonPressed)
        {
            buttonPressed = false;
            return BrewButtonStatus.PUSHED;
        }
        else
        {
            return BrewButtonStatus.NOT_PUSHED;
        }
    }

    public void SetBoilerState(BoilerState boilerState)
    {
        boilerOn = boilerState == BoilerState.ON;
    }

    public void SetWarmerState(WarmerState warmerState)
    {
        plateOn = warmerState == WarmerState.ON;
    }

    public void SetIndicatorState(IndicatorState indicatorState)
    {
        lightOn = indicatorState == IndicatorState.ON;
    }

    public void SetReliefValveState(ReliefValveState reliefValveState)
    {
        valveClosed = reliefValveState == ReliefValveState.CLOSED;
    }
}