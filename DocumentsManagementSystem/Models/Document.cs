using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentsManagementSystem.DataStructures;
using DocumentsManagementSystem.Utilities;

namespace DocumentsManagementSystem.Models
{
    public class Document
    {
        //data members
        public string MainPermutation { get; set; } // Key
        public int TotalQuantity { get; set; } // how many documents copies
        public BST<Permutations> Permutations { get; set; }
        public int TotalPermutationTypesCount; // how many different permutations kinds
        private Doubly<TimeData>.Node _refNode;
        
        //c'tor
        public Document(string key, string documentContent, Doubly<TimeData>.Node refNode)
        {
            Permutations = new BST<Permutations>();
            TotalQuantity = 0;
            TotalPermutationTypesCount = 0;
            _refNode = refNode;
            AddPermutation(documentContent, refNode);
            MainPermutation = key;
        }

        //functions
        internal void AddPermutation(string cleanPermutation, Doubly<TimeData>.Node refNode)
        {
            Permutations newPermutation = new Permutations(cleanPermutation, refNode);
            Permutations.Add(newPermutation);
            TotalPermutationTypesCount++;
            TotalQuantity++;
        }

        public bool IsPermutationExist(string cleanPermutation, out Permutations foundItem)
        {
            Permutations temp = new Permutations(cleanPermutation, null);
            bool isFound = Permutations.Search(temp, out foundItem);

            if (isFound)
            {
                TotalQuantity++;
                foundItem.Count++;
                return true;
            }
            return false;
        }
    }
}
