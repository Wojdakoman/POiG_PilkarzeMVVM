namespace PiłkarzeMVVM.Model
{
    class Player
    {
        public string Name { get; private set; }
        public string Lastname { get; private set; }
        public int Age { get; private set; }

        public Player(string name, string lastname, int age)
        {
            Name = name;
            Lastname = lastname;
            Age = age;
        }

        public void Edit(string name, string lastname, int age)
        {
            Name = name;
            Lastname = lastname;
            Age = age;
        }

        public bool SameAs(Player p)
        {
            if (Name != p.Name) return false;
            if (Lastname != p.Lastname) return false;
            if (Age != p.Age) return false;
            return true;
        }

        public override string ToString()
        {
            return $"{Name} {Lastname} {Age}";
        }
    }
}
