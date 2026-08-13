using UnityEngine;

namespace BaldiPlusNineNineFramework.Extensions
{
    public static class UnityExtensions
    {
        public static void SetCenterAnchor(this RectTransform transform)
        {
            transform.anchorMin = transform.anchorMax = Vector3.one / 2f;
            transform.anchoredPosition = Vector3.zero;
        }
    }
}
