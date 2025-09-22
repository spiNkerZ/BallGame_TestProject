using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GetRandomArrayIndexNoReplay
{
    public static T GetRandomElement<T>(this T[] _array) { return _array[Random.Range(0, _array.Length)];}
    public static T GetRandomElementList<T>(this List<T> _array) { return _array[Random.Range(0, _array.Count)];}
}
