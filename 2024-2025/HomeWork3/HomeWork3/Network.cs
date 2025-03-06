using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    internal class Network : IEnumerable<Network>
    {        
        // Code relating to Tree Node data structure
        private readonly Dictionary<string, Network> _children = new Dictionary<string, Network>();
        public string ID;
        public Network Parent { get; private set; }
        public void Add(Network network)
        {
            if (network.Parent != null)
            {
                network.Parent._children.Remove(network.ID);
            }

            network.Parent = this;
            this._children.Add(network.ID, network);
        }
        public IEnumerator<Network> GetEnumerator()
        {
            return this._children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get { return this._children.Count; }
        }

        // Code relating to game mechanics
        public string name;
        public int currentMoney;
        public int securityLevel; // Will go from 0-100

        Random rnd = new Random();
        int getServerSecurityLevel()
        {
            return this.securityLevel;
        }
        void weaken()
        {
            this.securityLevel -= rnd.Next(1, 10) * 1; // 1 is to be replaced with the players level
        }
        void grow()
        {
            this.currentMoney += rnd.Next(1, 100);
        }
        int hack()
        {
            int hackedMoney = rnd.Next(1, 100);
            this.currentMoney -= hackedMoney;
            return hackedMoney;
        }
        public Network(string name, int currentMoney, int securityLevel)
        {
            this.name = name;
            this.currentMoney = currentMoney;
            this.securityLevel = securityLevel;
        }
    }
}
