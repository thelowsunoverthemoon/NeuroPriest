using NeuroPriest.Characters;
using NeuroPriest.Shared;
using System;
using System.Collections.Generic;

namespace NeuroPriest.Maps
{
    internal class RoomData
    {
        public List<Enemy> Enems { get; }

        public RoomData(
            Dictionary<Coord, int> doorDict,
            int index,
            string doors,
            string enems,
            StaticInitController staticInitController
        )
        {
            Enems = new List<Enemy>();
            SetDoors(doorDict, index, doors);
            if (enems[0] != '0')
            {
                SetEnems(enems, staticInitController);
            }
        }

        private void SetDoors(Dictionary<Coord, int> doorDict, int index, string doors)
        {
            var doorData = doors.Split(' ');
            foreach (var door in doorData)
            {
                var data = door.Split(';');
                doorDict.Add(new Coord(int.Parse(data[0]), int.Parse(data[1])), index);
            }
        }

        private void SetEnems(string enems, StaticInitController staticInitController)
        {
            foreach (var enem in enems.Split(' '))
            {
                var data = enem.Split(';');
                staticInitController.Add(data[0]);
                Type type = Type.GetType(typeof(Character).Namespace + "." + data[0]);
                Enems.Add( // CHANGEEEE LATERRRRRRR SOOOOOOOOOOOOOOOOOOOOO SOOOOOOOOO gross
                    (Enemy)
                        Activator.CreateInstance(
                            type,
                            new Coord(Int32.Parse(data[1]), Int32.Parse(data[2]))
                        )
                );
            }
        }
    }
}
