using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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

        public void addListEntry(String key, ArrayList value)
        {
            hashtable.Add(key, value);
        }
        public void addIntegerEntry(String key, Int32 value)
        {
            hashtable.Add(key, value);
        }

        public String getParsedHashTable(Hashtable map)
        {
            if (map.Count == 0)
            {
                return "{}";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            foreach (DictionaryEntry element in map)
            {
                if (element.Value.GetType().ToString() == "System.String")
                {
                    sb.Append(String.Format(" \"{0}\":\"{1}\",", element.Key, element.Value));
                }
                else
                {
                    sb.Append(String.Format(" \"{0}\":{1},", element.Key, element.Value));
                }

            }
            return sb.ToString().Substring(0, sb.Length - 1)+"}";
        }
        public String getParsedList(ArrayList value)
        {
            StringBuilder sb = new StringBuilder();


            if(value.Count == 0)
            {
                return "";
            }

            for(int index=0; index<value.Count; index++)
            {
                if (value[index].GetType().ToString() == "System.String")
                {
                    sb.Append(String.Format("\"{0}\",", value[index].ToString()));
                }
                else
                if (value[index].GetType().ToString() == "System.Collections.Hashtable")
                {
                    sb.Append(getParsedHashTable((Hashtable)value[index]) +",");
                }
            }

            return sb.ToString().Substring(0, sb.Length - 1);
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
                if(element.Value.GetType().ToString() == "System.Collections.ArrayList")
                {
                    output += String.Format(" \"{0}\":[{1}],", element.Key, getParsedList((ArrayList)element.Value));
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
