using System.Collections.Generic;
using UnityEngine;

public static class DictionaryLoader
{
    public static HashSet<string> Words;

    public static void LoadWords()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("wordlist");
        string[] lines = wordFile.text.Split('\n');
        Words = new HashSet<string>();
        foreach (var line in lines)
        {
            string w = line.Trim().ToLower();
            if (w.Length > 0) Words.Add(w);
        }
    }
}

