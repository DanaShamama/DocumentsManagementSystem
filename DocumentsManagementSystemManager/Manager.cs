using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentsManagementSystem;

namespace DocumentsManagementSystemManager
{
    public class Manager
    {
        Dictionary<string,Document> _documentsManagementSystem = new Dictionary<string, Document>(); 

        public static bool IsDuplicated(string documentContent)
        {
            string mainPermutationOfDocument = Descriptor.MainPermutation(documentContent);



            return true;
        }
    }
}
