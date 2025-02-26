using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// static classes are classes that cannot be instantiated
public static class SaveSystem
{
    public static void SaveBattleshipData(BattleShip battleShip)
    {
        // create a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // path to save the data
        string path = Application.persistentDataPath + "/battleship.data";
        // create a file stream
        FileStream stream = new FileStream(path, FileMode.Create);
        // create a new battleship data object
        BattleshipData data = new BattleshipData(battleShip);
        // serialize the data
        formatter.Serialize(stream, data);
        // close the stream
        stream.Close();
    }


    public static BattleshipData LoadBattleshipData()
    {
        // path to save the data
        string path = Application.persistentDataPath + "/battleship.data";
        // if the file exists
        if (File.Exists(path))
        {
            // create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            // create a file stream
            FileStream stream = new FileStream(path, FileMode.Open);
            // deserialize the data
            BattleshipData data = formatter.Deserialize(stream) as BattleshipData;
            // close the stream
            stream.Close();
            // return the data
            return data;
        }
        // if the file does not exist
        else
        {
            // log an error
            Debug.LogError("Save file not found in " + path);
            // return null
            return null;
        }
    }
}
