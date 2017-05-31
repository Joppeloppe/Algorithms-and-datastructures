using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820 
namespace Inlämningsuppgift2
{
    class Program
    {
        static bool quit = false;

        static void Main(string[] args)
        {        
            UppgiftA();
            UppgiftB();
        }

        public static void UppgiftA()
        {
            Console.WriteLine("Uppgift A\n");
            Hashtable hashTable = new Hashtable(13);

            hashTable.Put(1, "Hello");
            hashTable.Put(1, "Hej");

            Console.WriteLine("Key 1 has value: " + hashTable.Get(1));
            Console.WriteLine("Table count is: " + hashTable.Count);
            hashTable.Remove(1);

            Console.WriteLine("\n\nPress enter to exit.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void UppgiftB()
        {
            Hashtable hashTable = new Hashtable(15);

            AddStuff(hashTable);
            Console.Clear();

            while (!quit)
            {
                Menu();

                int input = Convert.ToInt32(Console.ReadLine());

                if (input >= 0 && input <= 4)
                {
                    switch (input)
                    {
                        case 1:
                            {
                                Console.Write("Siffra: ");
                                int number = Convert.ToInt32(Console.ReadLine());
                                string spellNumber = hashTable.Get(number).ToString();

                                Console.WriteLine(number + " skrivs " + spellNumber);
                            }
                            break;
                        case 2:
                            {
                                Console.Write("Skriv siffra: ");
                                int number = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Skriv hur den siffran stavas: ");
                                string spellNumber = Console.ReadLine();

                                hashTable.Put(number, spellNumber);
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Vilket nummer vill du ta bort? ");
                                int number = Convert.ToInt32(Console.ReadLine());

                                hashTable.Remove(number);
                            }
                            break;
                        case 4:
                            {
                                Console.WriteLine("Det finns för tillfället " + hashTable.Count + " nummer.");
                            }
                            break;
                        case 0:
                            {
                                quit = true;
                            }
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine(input + " is an invalid option!");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                
            }
        }

        public static void AddStuff(Hashtable hashTable)
        {
            hashTable.Put(0, "noll");
            hashTable.Put(1, "ett");
            hashTable.Put(2, "två");
            hashTable.Put(3, "tre");
            hashTable.Put(4, "fyra");
            hashTable.Put(5, "fem");
            hashTable.Put(6, "sex");
            hashTable.Put(7, "sju");
            hashTable.Put(8, "åtta");
            hashTable.Put(9, "nio");
        }

        public static void Menu()
        {
            Console.WriteLine("Hej och välkommen!\n");
            Console.WriteLine("1. - Leta upp ett nummer.");
            Console.WriteLine("2. - Lägg till ett nummer.");
            Console.WriteLine("3. - Ta bort ett nummer.");
            Console.WriteLine("4. - Antal nummer i programmet.");
            Console.WriteLine("\n0. - Stäng programmet.");
        }
    }
}
