using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static T GetRandomElement<T>(this HashSet<T> hashSet)
    {
        if (hashSet == null || hashSet.Count == 0)
            throw new System.ArgumentException("HashSet is null or empty");

        var index = Random.Range(0, hashSet.Count);
        var i = 0;

        foreach (var element in hashSet)
        {
            if (i == index)
                return element;
            i++;
        }

        // This should never be reached
        throw new System.Exception("Failed to get random element from HashSet");
    }
}