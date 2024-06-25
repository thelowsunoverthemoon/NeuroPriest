using FMOD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NeuroPriest.Audio
{
    internal class AudioPlayer
    {
        private FMOD.System Fmod { get; }
        private ChannelGroup TrackGroup { get; }
        private ChannelGroup EffectGroup { get; }
        private Dictionary<string, Sound> SoundDict { get; }
        private ulong SampleRate { get; }

        public AudioPlayer()
        {
            CheckErr(Factory.System_Create(out var fmod));
            Fmod = fmod;
            CheckErr(Fmod.init(50, INITFLAGS.NORMAL, IntPtr.Zero));

            CheckErr(Fmod.createChannelGroup("effect", out var effectGroup));
            EffectGroup = effectGroup;

            CheckErr(Fmod.createChannelGroup("track", out var trackGroup));
            TrackGroup = trackGroup;

            CheckErr(Fmod.getSoftwareFormat(out var sampleRate, out _, out _));
            SampleRate = (ulong)sampleRate;

            SoundDict = new Dictionary<string, Sound>();
        }

        public void Update()
        {
            CheckErr(Fmod.update());
        }

        public void RegisterEffect(string name, string file)
        {
            Register(name, file, MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF | MODE._2D);
        }

        public void RegisterTrack(string name, string file)
        {
            Register(name, file, MODE.CREATESTREAM | MODE.LOOP_NORMAL | MODE._2D);
        }

        public void PlayEffect(string name)
        {
            CheckErr(Fmod.playSound(SoundDict[name], EffectGroup, false, out _));
        }

        public Channel FadeInTrack(string name, ulong length)
        {
            CheckErr(Fmod.playSound(SoundDict[name], TrackGroup, true, out var chan));
            CheckErr(chan.getDSPClock(out _, out var parentClock));
            CheckErr(chan.addFadePoint(parentClock, 0.0f));
            CheckErr(chan.addFadePoint(parentClock + (SampleRate * length), 1.0f));
            CheckErr(chan.setPaused(false));
            return chan;
        }

        public void FadeOutTrack(Channel chan, ulong length)
        {
            CheckErr(chan.getDSPClock(out _, out var parentClock));
            CheckErr(chan.addFadePoint(parentClock, 1.0f));
            CheckErr(chan.addFadePoint(parentClock + (SampleRate * length), 0.0f));
            CheckErr(chan.setDelay(0, parentClock + (SampleRate * length), true));
        }

        public void Stop()
        {
            TrackGroup.stop();
            TrackGroup.release();
            EffectGroup.stop();
            EffectGroup.release();
            foreach (var sound in SoundDict.Values)
            {
                sound.release();
            }
            Fmod.release();
        }

        private void Register(string name, string file, MODE mode)
        {
            if (SoundDict.ContainsKey(name))
            {
                return;
            }
            CheckErr(
                Fmod.createSound(
                    Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        "Data",
                        "Music",
                        file
                    ),
                    mode,
                    out var sound
                )
            );
            SoundDict.Add(name, sound);
        }

        private void CheckErr(RESULT result) // throw exception because err likely cannot be fixed programmatically
        {
            if (result != RESULT.OK)
            {
                throw new InvalidOperationException($"FMOD Error {result}");
            }
        }
    }
}
