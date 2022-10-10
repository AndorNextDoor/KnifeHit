using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;


public static class SaveData
{
    public static void SaveToJSON<T>(List<T> toSave, string FileName)
    {
        string content = JsonHelper.ToJSoN<T>(toSave.ToArray());
        WriteFile(GetPath(FileName), content);
    }
    public static List<T> ReadFromJSON<T>(string FileName)
    {
        string content = ReadFile(GetPath(FileName));
        if(string.IsNullOrEmpty(content) || content == "{}") 
        {
            return new List<T>();
        }
        List<T> res = JsonHelper.FromJson<T>(content).ToList();

        return res;
    }
    private static string GetPath(string FileName)
    {
        return Application.persistentDataPath + "/" + FileName;
    }
    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }
    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T> (string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }
       
    public static string ToJSoN<T> (T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJSON<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    
    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}
