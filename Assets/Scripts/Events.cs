using System;
using UnityEngine;

public static class Events
{
    public static Action OnShotBadAnimal;
    public static Action OnGoodAnimalDied;
    public static Action OnBadAnimalDied;
    public static Action OnGameOver;
    public static Action<float> SetSpeedMultiplier;
    public static Action<Color> Flash;
    public static Action OnFlashMax;
    public static Action<int> AddScore;
}
