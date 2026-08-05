using HarmonyLib;
using MonoMod.Utils;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Utilities
{
    /// <summary>
    /// Contains a lot of helper methods for creating objects.
    /// </summary>
    public class ObjectHelper
    {
        private static FieldInfo _planeDistance = AccessTools.Field(typeof(PlaneDistance), "planeDistance");

        /// <summary>
        /// Creates a new canvas with reference resolution 480x360.
        /// </summary>
        public static Canvas CreateCanvas(bool setGlobalCam, float planeDistance = 0.31f)
        {
            GameObject gameObj = new GameObject("Canvas");
            return CreateCanvas(gameObj, setGlobalCam, planeDistance);
        }

        /// <summary>
        /// Creates a new canvas with reference resolution 480x360.
        /// </summary>
        public static Canvas CreateCanvas(GameObject obj, bool setGlobalCam, float planeDistance = 0.31f)
        {
            obj.layer = LayerUtils.Ui;
            Canvas canvas = obj.AddComponent<Canvas>();
            CanvasScaler canvasScaler = obj.AddComponent<CanvasScaler>();
            obj.AddComponent<GraphicRaycaster>();

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.Normal
                | AdditionalCanvasShaderChannels.Tangent;
            canvasScaler.referencePixelsPerUnit = 1f;
            canvasScaler.referenceResolution = new Vector2(480, 360);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.scaleFactor = 1f;
            _planeDistance.SetValue(obj.AddComponent<PlaneDistance>(), planeDistance);
            canvas.planeDistance = planeDistance;

            if (setGlobalCam) canvas.worldCamera = GlobalCam.Instance.Cam;

            return canvas;
        }
    }
}
