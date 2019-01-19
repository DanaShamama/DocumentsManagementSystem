using DocumentsManagementSystem.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsManagementSystem.Models
{
    public class Permutations : IComparable<Permutations>
    {
        // data members
        public int Count { get; set; }
        public string Permutation { get; set; }
        public Doubly<TimeData>.Node Ref { get; set; }

        //c'tor
        public Permutations(string permutation, Doubly<TimeData>.Node refNode)
        {
            Permutation = permutation;
            Ref = refNode;
            if (refNode != null)
            {
                Count++;
            }
        }

        //functions
        public int CompareTo(Permutations other)
        {
            return (this.Permutation).CompareTo(other.Permutation);
        }
    }
}
