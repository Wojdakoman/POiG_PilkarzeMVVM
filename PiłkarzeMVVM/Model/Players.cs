using System.Collections.Generic;

namespace PiłkarzeMVVM.Model
{
    class Players
    {
        List<Player> players = new List<Player>();
        private string file = "pilkarze.json"; //data file location
        public List<Player> PlayersList { get => players; }

        public Players()
        {
            players = JSON.LoadData(file);
        }

        public void AddPlayer(string name, string lastname, int age)
        {
            players.Add(new Player(name, lastname, age));
        }

        public bool Exists(string name, string lastname, int age)
        {
            Player p = new Player(name, lastname, age);
            foreach (Player x in players)
                if (x.SameAs(p)) return true;
            return false;
        }

        public void EditPlayer(int idx, string name, string lastname, int age)
        {
            players[idx].Edit(name, lastname, age);
        }

        public void Remove(int idx)
        {
            players.RemoveAt(idx);
        }

        public void Save()
        {
            JSON.Save(file, players);
        }
    }
}
