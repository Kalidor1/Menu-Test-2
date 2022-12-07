using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class LayerExtensions
    {
        public static bool Test(this LayerMask mask, int index)
        {
            return (mask & (1 << index)) != 0;
        }

        public static bool Test(this LayerMask mask, GameObject obj)
        {
            return Test(mask, obj.layer);
        }

        public static bool Test(this LayerMask mask, Component comp)
        {
            return Test(mask, comp.gameObject.layer);
        }

        public static int[] GetSortingLayerMask(params string[] layerNames)
        {
            return layerNames.Select(SortingLayer.NameToID).ToArray();
        }
    }
}