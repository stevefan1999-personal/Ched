﻿namespace Ched.UI.Operations
{
    /// <summary>
    /// ユーザーの操作を表すインタフェースです。
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// この操作の説明を取得します。
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 操作を元に戻します。
        /// </summary>
        void Undo();

        /// <summary>
        /// 操作をやり直します。
        /// </summary>
        void Redo();
    }
}
