using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820
namespace Inlämningsuppgift2
{
    class Entry
    {
        public object key, value;

        public Entry(object key, object value)
        {
            this.key = key;
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            Entry keyValue = (Entry)obj;

            return key.Equals(keyValue.key);
        }
    }
}
