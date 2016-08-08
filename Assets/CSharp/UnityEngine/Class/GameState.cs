namespace Poi
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// 未定义
        /// </summary>
        UnDefine = 0,
        /// <summary>
        /// 准备完成
        /// </summary>
        Prepared,
        /// <summary>
        /// 转换
        /// </summary>
        Changing,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        /// <summary>
        /// 运行
        /// </summary>
        Running,
        /// <summary>
        /// 后台
        /// </summary>
        Background = 9,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause  =50,
        /// <summary>
        /// 保存
        /// </summary>
        Saving = 80,
        /// <summary>
        /// 退出
        /// </summary>
        Exit = 99,
    }
}
