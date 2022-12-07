using UnityEngine;

namespace Extensions
{
    public static class EnumerableExtensions
    {
        public static T RandomEntry<T>(this T[] arr)
        {
            if (arr.Length == 0)
            {
                return default;
            }

            var index = Random.Range(0, arr.Length);
            return arr[index];
        }
    }
}