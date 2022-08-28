using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class XmlSaveSystem : MonoBehaviour
{
    public static T ImportXml<T>(string path)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var imported = (T)serializer.Deserialize(stream);
                stream.Close();
                return imported;
            }
        }
        catch
        {
            Debug.LogWarning("[Exception importing] \n (" + path + ") xml file is not '" + typeof(T) + "'");
            return default;
        }
    }

    public static void ExportXml() // arreglar firma
    {
        // IMPLEMENTAR
    }

}
 