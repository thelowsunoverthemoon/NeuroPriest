using NeuroPriest.Shared;
using System.Collections.Generic;
using System.IO;

namespace NeuroPriest.Maps
{
    internal class LevelData
    {
        public Coord PlayerPos { get; }
        public int PlayerRoom { get; }
        public List<RoomData> Rooms { get; }
        public Dictionary<Coord, int> Doors { get; }

        public LevelData(string path, StaticInitController staticInitController)
        {
            Rooms = new List<RoomData>();
            Doors = new Dictionary<Coord, int>();
            var lines = File.ReadAllLines(path);

            var playerData = lines[0].Split(';');
            PlayerPos = new Coord(int.Parse(playerData[0]), int.Parse(playerData[1]));
            PlayerRoom = int.Parse(playerData[2]);

            for (var i = 1; i < lines.Length; i++)
            {
                var data = lines[i].Split('#');
                Rooms.Add(new RoomData(Doors, i - 1, data[0], data[1], staticInitController));
            }
        }
    }
}
