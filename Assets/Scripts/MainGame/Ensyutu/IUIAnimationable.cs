public interface IUIAnimationable
{
    /// <summary>
    /// 何かしらの情報を初期化してやる際にStartで呼び出すべきなやつ
    /// </summary>
    void Initialize();

    /// <summary>
    /// 何かしらアニメーションを実行する際に呼び出してもらうといいやつ
    /// </summary>
    void StartAnimation();

}
