using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentsManagementSystem.DataStructures;


namespace DocumentsManagementSystem.Models
{
    public class TimeData
    {
        //data members       
        public string Permutation { get; private set; } 
        public DateTime Timestamp { get;private set; }
        
        //c'tor
        public TimeData(string permutation)
        {
            Permutation = permutation;
            Timestamp = DateTime.Now;
        }
    }
}
