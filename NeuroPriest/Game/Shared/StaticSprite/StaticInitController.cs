using NeuroPriest.Audio;
using NeuroPriest.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NeuroPriest.Shared
{
    internal class StaticInitController
    {
        private StyleProvider StyleProvider { get; }
        public delegate StaticInit Instance(params object[] args);
        private Dictionary<string, Instance> InitLookup { get; }
        private HashSet<string> LoadList { get; }
        private AudioPlayer AudioPlayer { get; }

        public StaticInitController(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            StyleProvider = styleProvider;
            InitLookup = new Dictionary<string, Instance>();
            LoadList = new HashSet<string>();
            AudioPlayer = audioPlayer;
            foreach (
                Type type in Assembly
                    .GetAssembly(typeof(StaticInit))
                    .GetTypes()
                    .Where(
                        testType =>
                            testType.IsClass
                            && !testType.IsAbstract
                            && testType.IsSubclassOf(typeof(StaticInit))
                    )
            )
            {
                StaticInit sprite = (StaticInit)Activator.CreateInstance(type);
                InitLookup.Add(
                    sprite.Id,
                    (param) => (StaticInit)Activator.CreateInstance(type, args: param)
                );
            }
        }

        public void Add(string id)
        {
            LoadList.Add(id);
        }

        public Instance Lookup(string id)
        {
            return InitLookup.ContainsKey(id) ? InitLookup[id] : null;
        }

        public void InitAll()
        {
            foreach (var s in LoadList)
            {
                if (InitLookup.ContainsKey(s))
                {
                    InitLookup[s]().Init(StyleProvider, AudioPlayer);
                }
            }
        }
    }
}
