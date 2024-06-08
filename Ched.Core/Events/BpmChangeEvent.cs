﻿using System.Diagnostics;

namespace Ched.Core.Events
{
    /// <summary>
    /// BPMの変更イベントを表すクラスです。
    /// </summary>
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    [DebuggerDisplay("Tick = {Tick}, Value = {Bpm}")]
    public class BpmChangeEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty]
        private double bpm;

        public double Bpm
        {
            get { return bpm; }
            set { bpm = value; }
        }
    }
}
