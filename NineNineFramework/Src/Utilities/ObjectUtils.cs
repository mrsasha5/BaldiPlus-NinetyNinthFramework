using System;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Utilities
{
    [Flags]
    public enum AudioManagerSources
    {
        Sfx,
        Vfx,
        Music
    }

    public class ObjectUtils
    {
        public static T CreateSingleton<T>() where T : Singleton<T>
        {
            GameObject singletonObject = new GameObject(typeof(T).Name);
            GameObject.DontDestroyOnLoad(singletonObject);
            singletonObject.SetActive(false); // Prevent Awake() from being invoked before initialized correctly
            T component = singletonObject.AddComponent<T>();
            Singleton<T>.m_Instance = component;
            singletonObject.SetActive(true);
            return component;
        }

        public static AudioManager CreateAudioManager(bool distanceIndependent, AudioManagerSources sources = AudioManagerSources.Sfx, 
            bool subtitles = true, Color? subtitleColor = null)
        {
            return AddAudioManager(new GameObject("AudioManager"), distanceIndependent, sources, subtitles, subtitleColor);
        }

        public static AudioManager AddAudioManager(GameObject gameObject, bool distanceIndependent,
            AudioManagerSources sources = AudioManagerSources.Sfx, bool subtitles = true, Color? subtitleColor = null)
        {
            bool active = gameObject.activeSelf;
            gameObject.gameObject.SetActive(false); // Awake() throws an exception since audioDevice is null
                                                    // So using this approach for avoiding it
            AudioManager audMan = gameObject.AddComponent<AudioManager>();
            AudioSource audSrc = gameObject.AddComponent<AudioSource>(); // To avoid crash on Awake()
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

            audMan.disableSubtitles = !subtitles;
            audMan.usesSfx = sources.HasFlag(AudioManagerSources.Sfx);
            audMan.usesVfx = sources.HasFlag(AudioManagerSources.Vfx);
            audMan.usesMusic = sources.HasFlag(AudioManagerSources.Music);
            if (subtitles && subtitleColor != null)
            {
                audMan.subtitleColor = subtitleColor.Value;
            }

            gameObject.gameObject.SetActive(active);
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
            obj.layer = LayerUtils.UI;
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
            obj.AddComponent<PlaneDistance>().planeDistance = planeDistance;
            canvas.planeDistance = planeDistance;

            if (setGlobalCam) canvas.worldCamera = GlobalCam.Instance.Cam;

            return canvas;
        }
    }
}
