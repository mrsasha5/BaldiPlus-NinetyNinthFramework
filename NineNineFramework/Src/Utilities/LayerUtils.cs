using UnityEngine;

namespace BaldiPlusNineNineFramework.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class LayerUtils
    {
        private static bool warningNotifSpawned;

        public static LayerMask Windows { get; private set; }
        public static LayerMask IgnoreRaycast { get; private set; }
        public static LayerMask IgnoreRaycastB { get; private set; }
        public static LayerMask Npcs { get; private set; }
        public static LayerMask Player { get; private set; }
        public static LayerMask StandardEntities { get; private set; }
        public static LayerMask ClickableEntities { get; private set; }
        public static LayerMask CollidableEntities { get; private set; }
        public static LayerMask ClickableCollidableEntities { get; private set; }
        public static LayerMask NoInterCollisionEntities { get; private set; }
        public static LayerMask Billboard { get; private set; }
        public static LayerMask UI { get; private set; }
        public static LayerMask TakenBalloonLayer { get; private set; }
        public static LayerMask GumCollisionMask { get; private set; }
        public static LayerMask EntityCollisionMask { get; private set; }

        internal static void Initialize()
        {
            Windows = LayerFromName("Windows");
            StandardEntities = LayerFromName("StandardEntities");
            ClickableEntities = LayerFromName("ClickableEntities");
            CollidableEntities = LayerFromName("CollidableEntities");
            ClickableCollidableEntities = LayerFromName("ClickableCollidableEntities");
            NoInterCollisionEntities = LayerFromName("NoInterCollisionEntities"); //Used by students from event
            IgnoreRaycast = LayerFromName("Ignore Raycast");
            IgnoreRaycastB = LayerFromName("Ignore Raycast B");
            Billboard = LayerFromName("Billboard");
            UI = LayerFromName("UI");
            Npcs = LayerFromName("NPCs");
            Player = LayerFromName("Player");
#warning TODO: take values directly from prefabs
            GumCollisionMask = 2113537;
            EntityCollisionMask = 2113541;
            TakenBalloonLayer = 29;
        }

        private static LayerMask LayerFromName(string name)
        {
            LayerMask layer = LayerMask.NameToLayer(name);
#warning return notification
            /*if (layer < 0 && !warningNotifSpawned)
            {
                warningNotifSpawned = true;
                NotificationManager.Instance.Queue(
                    "SchoolLib_Notif_LayersError", null, isForced: true);
            }
            NineNineCore.Logging.LogWarning($"{new StackFrame(1).GetFileName()} is attempted to get non-existing layer \"{name}\".");*/
            return layer;
        }
    }
}
