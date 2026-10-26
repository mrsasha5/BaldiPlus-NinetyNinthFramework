using BepInEx.Configuration;

namespace BaldiPlusNineNineFramework
{
    internal class ConfigManager
    {
        public static ConfigEntry<bool> webContent;
        public static ConfigEntry<bool> welcomeMenu;
        public static ConfigEntry<bool> welcomeMenuAnimations;

        public static void Initialize()
        {
            webContent = NineNineCore.Instance.Config.Bind("General", "Web Content", true, 
                "This setting allows the Ninety-Ninth Framework to send a web requests.");
            welcomeMenu = NineNineCore.Instance.Config.Bind("Welcome Menu", "Welcome Menu", true, 
                "This setting enables \"Welcome Back\" menu that appears before mods loading process.");
            welcomeMenuAnimations = NineNineCore.Instance.Config.Bind("Welcome Menu", "Alive Welcome Menu", true, 
                "This setting enables start animation and alive buttons in the \"Welcome Back\" menu.");
        }
    }
}
