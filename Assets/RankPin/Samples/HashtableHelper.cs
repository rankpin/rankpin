using UnityEngine;
using System.Collections;

public class HashtableHelper
{
	static public void print(string message, Hashtable data)
	{
		string log = "";
		if(string.IsNullOrEmpty(message) == false)
			log += string.Format("[{0}]", message);
		foreach(DictionaryEntry pair in data)
		{
			log += string.Format("{0}={1},", pair.Key, pair.Value);
		}
		Debug.Log(log);
	}
}
