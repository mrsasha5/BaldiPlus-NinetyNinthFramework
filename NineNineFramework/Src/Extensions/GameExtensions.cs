namespace BaldiPlusNineNineFramework.Extensions
{
    public static class GameExtensions
    {
        public static void PlaySoundEffect(this MusicManager instance, SoundObject sound, float volumeScale)
        {
            instance.soundSource.PlaySingle(sound, volumeScale);
        }
    }
}
