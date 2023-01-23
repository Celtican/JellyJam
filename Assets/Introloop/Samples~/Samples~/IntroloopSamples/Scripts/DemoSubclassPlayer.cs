using E7.Introloop;

namespace E7.IntroloopDemo
{
    /// <summary>
    ///     <para>
    ///         This is an example usage of <see cref="IntroloopPlayer{T}"/>. The point is that you liked the singleton
    ///         instance feature of <see cref="IntroloopPlayer"/> (By using <see cref="IntroloopPlayer.Instance"/>.), but
    ///         you want more unique instance of it that you can name as you like.
    ///     </para>
    ///     <para>
    ///         By doing this (Subclass, then putting itself into T of <see cref="IntroloopPlayer{T}"/>), you can
    ///         now do <see cref="DemoSubclassPlayer.Instance"/> to get convenient singleton instance, but not the same
    ///         instance as <see cref="IntroloopPlayer.Instance"/>.
    ///     </para>
    /// </summary>
    internal class DemoSubclassPlayer : IntroloopPlayer<DemoSubclassPlayer>
    {
    }
}