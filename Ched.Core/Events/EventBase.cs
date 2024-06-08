﻿using System;
using System.Diagnostics;

namespace Ched.Core.Events
{
    /// <summary>
    /// 譜面におけるイベントを表すクラスです。
    /// </summary>
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    [DebuggerDisplay("Tick = {Tick}")]
    public abstract class EventBase
    {
        [Newtonsoft.Json.JsonProperty]
        private int tick;

        /// <summary>
        /// このイベントの位置を表すTick値を取得、設定します。
        /// </summary>
        public int Tick
        {
            get { return tick; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value", "Tick must be greater than or equal to 0.");
                tick = value;
            }
        }
    }
}
