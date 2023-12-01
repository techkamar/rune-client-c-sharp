using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPP.lib
{
    public class HashTableUtil
    {
        public static String getStringFromVal(Hashtable hashTable, String key)
        {
            return (string)hashTable[key];
        }
    }
}
