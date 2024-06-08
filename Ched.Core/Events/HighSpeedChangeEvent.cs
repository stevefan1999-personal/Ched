﻿using System.Diagnostics;

namespace Ched.Core.Events
{
    /// <summary>
    /// ハイスピードの変更を表すクラスです。
    /// </summary>
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    [DebuggerDisplay("Tick = {Tick}, Value = {SpeedRatio}")]
    public class HighSpeedChangeEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty]
        private decimal speedRatio;

        /// <summary>
        /// 1を基準とする速度比を設定します。
        /// </summary>
        public decimal SpeedRatio
        {
            get { return speedRatio; }
            set { speedRatio = value; }
        }
    }
}
