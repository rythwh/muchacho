using System;

public static class Events
{
    public static Action OnShotBadAnimal;
    public static Action OnLifeLost;
    public static Action OnGameOver;
    public static Action<float> SetSpeedMultiplier;
    public static Action Flash;
}
