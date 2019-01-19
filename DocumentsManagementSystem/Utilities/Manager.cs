using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentsManagementSystem.DataStructures;
using DocumentsManagementSystem.Models;
using System.Threading;

namespace DocumentsManagementSystem.Utilities
{
    public class Manager
    {
        //data members
        Dictionary<string, Document> _documentsManagementSystem; 
        QueueNode<TimeData> _timeStamps;
        
        //c'tor
        public Manager()
        {
            _documentsManagementSystem = new Dictionary<string, Document>();
            _timeStamps = new QueueNode<TimeData>();
            Timer timerOld = new Timer(DeleteOld, null, 0, 300000);
            
        }

        //functions
        private void DeleteOld(object state = null)
        {
            DeleteOld();
        }

        public bool IsDuplicated(string documentContent, out string msg)
        {
            string cleanPermutation;
            string key;
            bool isContentOk = Descriptor.GetKey(documentContent, out cleanPermutation, out key);
            msg  = "The document content you entered is not legal";

            if (!isContentOk)
            {
                return false;
            }

            Doubly<TimeData>.Node refNode = null;
            if (_documentsManagementSystem.ContainsKey(key)) // key exists, Document is in the system
            {
                Permutations foundItem;
                bool isPermutationExist = _documentsManagementSystem[key].IsPermutationExist(cleanPermutation, out foundItem);

                if (!isPermutationExist)
                {
                    TimeData newDocumentTimeStamp = new TimeData(cleanPermutation);
                    _timeStamps.EnQueue(newDocumentTimeStamp, out refNode);
                    _documentsManagementSystem[key].AddPermutation(cleanPermutation, refNode);
                    msg = "The document was already in the system, but the permutation wasn't in the system. It is now. The quantities were updated";
                }
                else
                {
                    _timeStamps._QueueDoubly.RemoveNode(foundItem.Ref);
                    TimeData newDocumentTimeStamp = new TimeData(cleanPermutation);
                    _timeStamps.EnQueue(newDocumentTimeStamp, out refNode);
                    foundItem.Ref = refNode;
                    msg = "The document and the permutation were already in the system. The quantities were updated.";
                }

                return true;
            }
            else // create new document 
            {
                TimeData newDocumentTimeStamp = new TimeData(cleanPermutation);
                _timeStamps.EnQueue(newDocumentTimeStamp, out refNode);
                Document doc = new Document(key, cleanPermutation, refNode);
                _documentsManagementSystem.Add(key, doc);
                msg = "The document wasn't in the system. It is now. The quantities were updated.";
                return false;
            }
        }

        public string ShowDocumentDetails(string documentContent)
        {
            string cleanPermutation;
            string key;
            bool isContentOk = Descriptor.GetKey(documentContent, out cleanPermutation, out key);

            if (!isContentOk)
            {
                return "The document content you entered is not legal";
            } 

            if (_documentsManagementSystem.ContainsKey(key))
            {
                int numOfDocuments = _documentsManagementSystem[key].TotalQuantity;
                int numOfPermutationTypes = _documentsManagementSystem[key].TotalPermutationTypesCount;

                return string.Format("\nThe number of documents for the content {0} in the system is: {1}, \nand the number of permutations in the system is: {2}", documentContent, numOfDocuments, numOfPermutationTypes);
            }
            return String.Format("\nThe document {0} doesn't exist in the system", documentContent);
        }

        public string ShowPermutationDetails(string documentContent)
        {
            string cleanPermutation;
            string key;
            bool isContentOk = Descriptor.GetKey(documentContent, out cleanPermutation, out key);

            if (!isContentOk)
            {
                return "The document content you entered is not legal";
            }
           
            // key doesn't exist
            string msg = String.Format("\nThe document {0} doesn't exist in the system", documentContent);
            if (!_documentsManagementSystem.ContainsKey(key)) 
            {
                return msg;
            }
 
            // key exist but permutation doesn't exist
            Permutations toSearch = new Permutations(cleanPermutation, null);
            Permutations foundItem;
            bool isPermutationExist = _documentsManagementSystem[key].Permutations.Search(toSearch, out foundItem);
            msg = String.Format("\nThe permutation {0} doesn't exist in the system", cleanPermutation);
            if (!isPermutationExist)
            {
                return msg;
            }

            int numOfPermutationCopies = foundItem.Count;
            DateTime timeStamp = foundItem.Ref.Data.Timestamp;
            return string.Format("\nFor the document permutation of {0}, there are {1} numbers of copies, and the last copy was updated in {2}", documentContent, numOfPermutationCopies, timeStamp);

        }

        public void DeleteOld()
        {
            DateTime now = DateTime.Now;
            DateTime old = now.Subtract(new TimeSpan(0, 5, 0));

            while (true)
            {
                if (_timeStamps == null || _timeStamps._QueueDoubly == null || _timeStamps._QueueDoubly.Start == null || _timeStamps.Counter == 0)
                {
                    break;
                }

                if (old.CompareTo(_timeStamps._QueueDoubly.Start.Data.Timestamp) > 0)
                {
                    string permutation = _timeStamps._QueueDoubly.Start.Data.Permutation;                    
                    string key;
                    string cleanPermutation;
                    Descriptor.GetKey(permutation, out cleanPermutation, out key);

                    Permutations temp = new Permutations(permutation, null);
                    Permutations foundItem;
                    _documentsManagementSystem[key].Permutations.Search(temp, out foundItem);                  
                    int remvoedItemCount = foundItem.Count;
                    Permutations remvoedItem;

                    _documentsManagementSystem[key].Permutations.Remove(foundItem, out remvoedItem);
                    _timeStamps.DeQueue();
                    _documentsManagementSystem[key].TotalQuantity -= remvoedItemCount;                   
                    _documentsManagementSystem[key].TotalPermutationTypesCount--;
                    
                    if (_documentsManagementSystem[key].TotalPermutationTypesCount == 0)
                    {
                        _documentsManagementSystem.Remove(key);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public string FindClosestPermutation(string permutation)
        {            
            string cleanPermutation;
            string key;
            string msg = string.Format("The permutation {0} was not found, and also wasn't found a following permutation", permutation);

            if (!Descriptor.GetKey(permutation, out cleanPermutation, out key))
            {
                return "The document content you entered is not legal"; 
            }

            if (!_documentsManagementSystem.ContainsKey(key))
            {
                return string.Format("The document for the permutation {0}, doesn't exist in the system", permutation);
            }

            Permutations itemToFind = new Permutations(cleanPermutation, null);
            Permutations foundItem;
            bool foundNext = _documentsManagementSystem[key].Permutations.SearchNext(itemToFind, out foundItem);

            if (foundNext)
            {
                return ShowPermutationDetails(foundItem.Permutation);
            }
            return msg;
        }
    }
}
