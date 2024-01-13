using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitAllNodes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] children = { 2, 3, 4 };
            Node Node1 = new Node(1);
            Node Node2 = new Node(2);
            Node Node3 = new Node(3);
            Node Node4 = new Node(4);
            Node Node5 = new Node(5);
            Node Node6 = new Node(6);
            Node Node7 = new Node(7);
            Node Node8 = new Node(8);
            Node Node9 = new Node(9);
            Node Node10 = new Node(10);
        }
    }
    class Node
    {
        public List<Node> children = new List<Node>();
        public Node parent = null;
        public int index;
        public Node(int index)
        {
            this.index = index;
        }
        public void AddChild(Node child) 
        { 
            children.Add(child); 
        }
        public void SetParent(Node parent)
        {
            this.parent = parent;
        }
    }
}
