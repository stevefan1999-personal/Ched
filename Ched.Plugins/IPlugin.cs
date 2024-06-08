namespace Ched.Plugins
{
    /// <summary>
    /// アプリケーションで利用可能なプラグインを表します。
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// プラグインの表示名を取得します。
        /// </summary>
        string DisplayName { get; }
    }
}
