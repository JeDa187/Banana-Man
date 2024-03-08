using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class FileManager 
{
    string fileName="";
    string directoryPath = "";

    public FileManager(string fileName, string directoryPath)
    {
        this.fileName = fileName;
        this.directoryPath = directoryPath;
    }

    //public GameData Load()
    //{
    //   string fullPath = Path.Combine(directoryPath, fileName);
    //    GameData gameData = null;
    //    if (File.Exists(fullPath))
    //    {
    //        try
    //        {
    //            // Ladataan jsno-data tiedostosta
    //            string dataToLoad = "";
    //            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
    //            {
    //                using (StreamReader reader = new StreamReader(stream)) 
    //                {
    //                dataToLoad = reader.ReadToEnd();
    //                }
    //            }
    //            //gameData = JsonUtility.FromJson<GameData>(dataToLoad);
    //            gameData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
    //        }
    //        catch (Exception e) {
    //        Debug.Log($"Cannot load {fullPath}):Exeption{e}};
    //        {
                
    //        }
    //        finally
    //        {

    //        }
    //    }
    //}


}
