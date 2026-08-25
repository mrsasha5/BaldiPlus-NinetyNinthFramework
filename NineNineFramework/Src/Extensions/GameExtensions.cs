namespace BaldiPlusNineNineFramework.Extensions
{
    public static class GameExtensions
    {
        public static void PlaySoundEffect(this MusicManager instance, SoundObject sound, float volumeScale)
        {
            instance.soundSource.PlaySingle(sound, volumeScale);
        }

        public static void EnableTransition(this StandardMenuButton button)
        {
            button.transitionOnPress = true;
            button.transitionTime = 0.0167f;//66667f;
            button.transitionType = UiTransition.Dither;
        }
    }
}
