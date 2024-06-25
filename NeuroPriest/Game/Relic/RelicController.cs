using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NeuroPriest.Relics
{
    internal class RelicController
    {
        public List<Relic> ArmaChristiList { get; }
        public List<Relic> BlessingList { get; }
        public List<Relic> PenanceList { get; }
        public List<Relic> ArmaChristiAll { get; }
        public List<Relic> BlessingAll { get; }
        public List<Relic> PenanceAll { get; }
        public List<List<Relic>> RandType { get; }
        private Random Random { get; }

        public RelicController()
        {
            Random = new Random();

            ArmaChristiAll = new List<Relic>();
            BlessingAll = new List<Relic>();
            PenanceAll = new List<Relic>();

            ArmaChristiList = new List<Relic>();
            BlessingList = new List<Relic>();
            PenanceList = new List<Relic>();

            RandType = new List<List<Relic>>() { ArmaChristiAll, PenanceAll, BlessingAll };
            foreach ( // bruh consolidate into JSON or smth its gross af
                Type type in Assembly
                    .GetAssembly(typeof(Relic))
                    .GetTypes()
                    .Where(
                        testType =>
                            typeof(Relic).IsAssignableFrom(testType)
                            && !testType.IsAbstract
                            && testType.IsSubclassOf(typeof(Relic))
                    )
            )
            {
                Relic relic = (Relic)Activator.CreateInstance(type);
                if (type.BaseType.Name == "ArmaChristi")
                {
                    ArmaChristiAll.Add(relic);
                }
                else if (type.BaseType.Name == "Blessing")
                {
                    BlessingAll.Add(relic);
                }
                else
                {
                    PenanceAll.Add(relic);
                }
            }
            // all three unlocked at beginning
            Add(ArmaChristiAll, ArmaChristiList);
            Add(ArmaChristiAll, ArmaChristiList);
            Add(ArmaChristiAll, ArmaChristiList);
            Add(BlessingAll, BlessingList);
            Add(PenanceAll, PenanceList);
        }

        public Relic Add()
        {
            List<Relic> type = RandType[Random.Next(RandType.Count)];
            if (type == ArmaChristiAll)
            {
                return Add(ArmaChristiAll, ArmaChristiList);
            }
            else if (type == BlessingAll)
            {
                return Add(BlessingAll, BlessingList);
            }
            else
            {
                return Add(PenanceAll, PenanceList);
            }
        }

        public Relic Add(List<Relic> all, List<Relic> player)
        {
            Relic add = all[Random.Next(all.Count)];

            all.Remove(add);
            if (all.Count == 0)
            {
                RandType.Remove(all);
            }

            player.Add(add);
            return add;
        }
    }
}
