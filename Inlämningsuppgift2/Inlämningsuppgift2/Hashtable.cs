using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820
namespace Inlämningsuppgift2
{
    class Hashtable
    {
        private LinkedList<object> insertionOrder = new LinkedList<object>();
        private LinkedList<Entry>[] table;

        public int Count
        {
            get
            {
                return insertionOrder.Count;
            }
        }

        public Hashtable(int size)
        {
            table = new LinkedList<Entry>[size];

            for (int i = 0; i < size; i++)
            {
                table[i] = new LinkedList<Entry>();
            }
        }

        private int HashIndex(object key)
        {
            int hashCode = key.GetHashCode();
            hashCode %= table.Length;

            return (hashCode < 0) ? -hashCode : hashCode;
        }

        public object Get(object key)
        {
            int hashIndex = HashIndex(key);

            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = table[hashIndex].Find(new Entry(key, null)).Value;
                return entry.value;
            }

            return "Error: Key: " + key + " not found.";
        }

        public void Put(object key, object value)
        {
            int hashIndex = HashIndex(key);

            if (!table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = new Entry(key, value);

                table[hashIndex].AddLast(entry);
                insertionOrder.AddLast(value);

                Console.WriteLine("The key " + key + " has been added with the value " + value);
            }
            else
            {
                Console.WriteLine("Key already exist, you dummy!");
            }
        }

        public void Remove(object key)
        {
            int hashIndex = HashIndex(key);

            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = table[hashIndex].Find(new Entry(key, null)).Value;

                table[hashIndex].RemoveLast();
                insertionOrder.Remove(entry.value);

                Console.WriteLine("Entry removed: " + key + ", " + entry.value);
            }
            else
            {
                Console.WriteLine("Key does not exist, you dummy!");
            }
        }

        //WTF?!?!?!?!?!?!
        public LinkedList<object> GetInstertionOrder()
        {
            return insertionOrder;
        }
    }
}
