using System;

public static class AnalyticsEventSystem
{
    public static event Action OnTimeTravel;
    public static event Action OnTreeChop;
    public static event Action<bool> OnPetCollect;
    public static event Action<bool> OnOpenDoor;
    public static event Action<bool> OnPassingHardPuzzle; 

    public static void TriggerOnTimeTravel()
    {
        OnTimeTravel?.Invoke();
    }

    public static void TriggerOnTreeChop()
    {
        OnTreeChop?.Invoke();
    }

    public static void TriggerOnPetCollect(bool isCollected)
    {
        OnPetCollect?.Invoke(isCollected);
    }

    public static void TriggerOnOpenDoor(bool isDoorOpen)
    {
        OnOpenDoor?.Invoke(isDoorOpen);
    }

    public static void TriggerOnPassingPuzzle(bool isPuzzlePassed)
    {
        OnPassingHardPuzzle?.Invoke(isPuzzlePassed);
    }
}
