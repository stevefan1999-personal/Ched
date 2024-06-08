﻿using System;

namespace Ched.Core.Notes
{
    public interface ILongNote
    {
        /// <summary>
        /// ノートの開始位置を表すTickを設定します。
        /// </summary>
        int StartTick { get; }

        /// <summary>
        /// ノートの長さを表すTickを取得します。
        /// </summary>
        int GetDuration();
    }

    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public abstract class MovableLongNoteBase : ILongNote
    {
        [Newtonsoft.Json.JsonProperty]
        private int startTick;

        /// <summary>
        /// ノートの開始位置を表すTickを設定します。
        /// </summary>
        public int StartTick
        {
            get { return startTick; }
            set
            {
                if (startTick == value) return;
                if (value < 0) throw new ArgumentOutOfRangeException("value", "value must not be negative.");
                startTick = value;
            }
        }

        /// <summary>
        /// ノートの長さを表すTickを取得します。
        /// </summary>
        public abstract int GetDuration();
    }

    public abstract class LongNoteTapBase : IAirable
    {
        public abstract bool IsTap { get; }
        public abstract int LaneIndex { get; }
        public abstract int Tick { get; }
        public abstract int Width { get; }
    }
}
