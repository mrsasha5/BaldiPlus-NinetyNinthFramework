using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Utilities
{
    /// <summary>
    /// Contains a lot of helper methods for creating objects.
    /// </summary>
    public class ObjectUtils
    {
        private static FieldInfo _planeDistance = AccessTools.Field(typeof(PlaneDistance), "planeDistance");

        public static AudioManager CreateAudioManager(bool distanceIndependent)
        {
            return InitAudioManager(new GameObject("AudioManager"), distanceIndependent);
        }

        public static AudioManager InitAudioManager(GameObject obj, bool distanceIndependent)
        {
            bool active = obj.activeSelf;
            obj.gameObject.SetActive(false); // Awake() throws an exception since audioDevice is null
                                             // So using this approach for avoiding it
            AudioManager audMan = obj.AddComponent<AudioManager>();
            AudioSource audSrc = obj.AddComponent<AudioSource>(); // To avoid crash on Awake()
            audMan.audioDevice = audSrc;
            audSrc.rolloffMode = AudioRolloffMode.Custom;
            if (distanceIndependent)
            {
                audMan.audioDevice.spatialBlend = 0f;
                audMan.positional = false;
            }
            else
            {
                audMan.audioDevice.spatialBlend = 1f;
                audMan.audioDevice.maxDistance = 100f;
            }
            obj.gameObject.SetActive(active);
            return audMan;
        }

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
