using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAPP.lib
{
    public class JSONHolder
    {
        public Hashtable hashtable { get; set; }
        public JSONHolder() {
            hashtable = new Hashtable();
        }

        public void addStringEntry(String key, String value)
        {
            hashtable.Add(key,value);
        }

        public void addIntegerEntry(String key, Int16 value)
        {
            hashtable.Add(key, value);
        }

        public String getParsedJSONString()
        {
            String output = "{";
            
            foreach (DictionaryEntry element in hashtable)
            {
                if (element.Value.GetType().ToString() == "System.String")
                {
                    output += String.Format(" \"{0}\":\"{1}\",", element.Key, element.Value);
                }
                else
                {
                    output += String.Format(" \"{0}\":{1},", element.Key, element.Value);
                }
            }
            output = output.Substring(0, output.Length-1); // Remove trailing COMMA at the end
            output += "}";
            return output;
        }

        public Hashtable getMappedObjectFromString(String JSONString)
        {
            Hashtable myMap = new Hashtable();

            // removed { and } at begining and end
            JSONString = JSONString.Substring(1,JSONString.Length-2);

            string[] tokens = JSONString.Split(',');

            foreach (string curr_token in tokens)
            {
                int doubleQuoteCount = curr_token.Split('\"').Length - 1;
                string reqd_token = curr_token.Replace("\"", "");

                if (doubleQuoteCount == 4)
                {
                    // Which means its String
                    string[] token_array = reqd_token.Split(':');
                    myMap.Add(token_array[0], token_array[1]);
                }
            }
            return myMap;
        }


    }
}
