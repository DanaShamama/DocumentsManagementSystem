using DocumentsManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager mng = new Manager();

            while (true)
            {
                Console.WriteLine("\n\n\t\t *** This is a Documents Management System *** \n\n Please enter your choice: \n\n*To check if a document exists in the system press 'A'\n If the document does not exist in the system it will be added \n If the document exists in the system but the permutation doesn't exist it will be added \n If the permutation exist - the quantity will be updated \n\n*To show a document's details press 'B'\n\n*To show a permutation's details press 'C'\n\n*To find a permutation or the next closest permutation if there is any - press 'D'\n\n*To exit from the progrem press 'E'\n\n*Any other key will get you to this menu\nEvery 5 minutes the documents that are older than 5 minutes are being erased\n");
                string operation = Console.ReadLine();
                string documentContent;
                string msg;

                switch (operation)
                {
                    case "A":
                        Console.WriteLine("Enter the document which you want to check if exits in the system");
                        documentContent = Console.ReadLine();
                        bool isDuplicated = mng.IsDuplicated(documentContent, out msg);
                        Console.WriteLine(msg);
                        break;




                    case "B":
                        Console.WriteLine("Enter the document which details you want to see");
                        documentContent = Console.ReadLine();
                        Console.WriteLine(mng.ShowDocumentDetails(documentContent));
                        break;

                    case "C":
                        Console.WriteLine("Enter the permutation which details you want to see");
                        string permutation = Console.ReadLine();
                        Console.WriteLine(mng.ShowPermutationDetails(permutation));
                        break;

                    case "D":
                        Console.WriteLine("Enter the permutation which details you want to see, If it doesn't exist, you will see the next one in the alphabetical order, if there is any");
                        string permutationToFind = Console.ReadLine();
                        Console.WriteLine(mng.FindClosestPermutation(permutationToFind));
                        break;

                    case "E":
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
