using System;

public static class Events
{
    public static Action OnShotBadAnimal;
    public static Action OnGoodAnimalDied;
    public static Action OnBadAnimalDied;
    public static Action OnGameOver;
    public static Action<float> SetSpeedMultiplier;
    public static Action Flash;
    public static Action OnFlashMax;
    public static Action<int> AddScore;
}
