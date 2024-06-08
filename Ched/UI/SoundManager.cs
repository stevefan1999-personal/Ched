using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ManagedBass;
using ManagedBass.Fx;

namespace Ched.UI
{
    public class SoundManager : IDisposable
    {
        readonly HashSet<int> playing = new HashSet<int>();
        readonly HashSet<SyncProcedure> syncProcs = new HashSet<SyncProcedure>();
        readonly Dictionary<string, Queue<int>> handles = new Dictionary<string, Queue<int>>();
        readonly Dictionary<string, double> durations = new Dictionary<string, double>();

        public bool IsSupported { get; private set; } = true;

        public event EventHandler ExceptionThrown;

        public void Dispose()
        {
            if (!IsSupported) return;
            Bass.Stop();
            Bass.PluginFree(0);
            Bass.Free();
        }

        public SoundManager()
        {
            // なぜBass.LoadMe()呼び出すとfalseなんでしょうね
            if (!Bass.Init())
            {
                IsSupported = false;
                return;
            }
        }

        public void Register(string path)
        {
            CheckSupported();
            lock (handles)
            {
                if (handles.ContainsKey(path)) return;
                int handle = GetHandle(path);
                long len = Bass.ChannelGetLength(handle);
                handles.Add(path, new Queue<int>());
                lock (durations) durations.Add(path, Bass.ChannelBytes2Seconds(handle, len));
            }
        }

        protected int GetHandle(string filepath)
        {
            int rawHandle = Bass.CreateStream(filepath, 0, 0, BassFlags.Decode);
            if (rawHandle == 0) throw new ArgumentException("cannot create a stream.");
            int tempoHandle = BassFx.TempoCreate(rawHandle, BassFlags.FxFreeSource);
            if (tempoHandle == 0) throw new ArgumentException("cannot create a stream.");
            return tempoHandle;
        }

        public void Play(string path)
        {
            Play(path, 0, 1.0, 1.0);
        }

        public void Play(string path, double offset, double volume = 1.0, double speed = 1.0)
        {
            CheckSupported();
            Task.Run(() => PlayInternal(path, offset, volume, speed))
                .ContinueWith(p =>
                {
                    if (p.Exception != null)
                    {
                        Program.DumpExceptionTo(p.Exception, "sound_exception.json");
                        ExceptionThrown?.Invoke(this, EventArgs.Empty);
                    }
                });
        }

        private void PlayInternal(string path, double offset, double volume, double speed)
        {
            Queue<int> freelist;
            lock (handles)
            {
                if (!handles.ContainsKey(path)) throw new InvalidOperationException("sound source was not registered.");
                freelist = handles[path];
            }

            int handle;
            lock (freelist)
            {
                if (freelist.Count > 0) handle = freelist.Dequeue();
                else
                {
                    handle = GetHandle(path);

                    var proc = new SyncProcedure((h, channel, data, user) =>
                    {
                        lock (freelist) freelist.Enqueue(handle);
                    });

                    int syncHandle;
                    syncHandle = Bass.ChannelSetSync(handle, SyncFlags.End, 0, proc, IntPtr.Zero);
                    if (syncHandle == 0) throw new InvalidOperationException("cannot set sync");
                    lock (syncProcs) syncProcs.Add(proc); // avoid GC
                }
            }

            lock (playing) playing.Add(handle);
            Bass.ChannelSetPosition(handle, Bass.ChannelSeconds2Bytes(handle, offset));
            Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, volume);
            Bass.ChannelSetAttribute(handle, ChannelAttribute.Tempo, (speed - 1.0) * 100);
            Bass.ChannelPlay(handle, false);
        }

        public void StopAll()
        {
            CheckSupported();
            lock (playing)
            {
                foreach (int handle in playing)
                {
                    Bass.ChannelStop(handle);
                }
                playing.Clear();
            }
        }

        public double GetDuration(string path)
        {
            Register(path);
            lock (durations) return durations[path];
        }

        protected void CheckSupported()
        {
            if (IsSupported) return;
            throw new NotSupportedException("The sound engine is not supported.");
        }
    }

    /// <summary>
    /// 音源を表すクラスです。
    /// </summary>
    [Serializable]
    public class SoundSource
    {
        public static readonly IReadOnlyCollection<string> SupportedExtensions = [".wav", ".mp3", ".ogg"];

        /// <summary>
        /// この音源における遅延時間を取得します。
        /// この値は、タイミングよく音声が出力されるまでの秒数です。
        /// </summary>
        public double Latency { get; set; }

        public string FilePath { get; set; }

        private double volume = 1.0;
        public double Volume
        {
            get => volume;
            set
            {
                if (volume < 0 || volume > 1.0)
                    throw new ArgumentOutOfRangeException("value");
                volume = value;
            }
        }

        public SoundSource()
        {
        }

        public SoundSource(string path, double latency)
        {
            FilePath = path;
            Latency = latency;
        }
    }
}
