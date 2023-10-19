using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KermansUtility.Systems.SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        // Stores the current slot index where the player's progress is saved
        public int CurrentSelectedSlot { get; private set; }

        [SerializeField] private SaveLoadSettings _saveLoadSettings;
        [SerializeField] private int _selectedSlot;

        private void Awake()
        {
            CurrentSelectedSlot = _selectedSlot;
        }

        // Saves a class of the given type, which should implement ISaveable.
        public void SaveData<T>(T data) where T : ISaveable
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file;

            string savePath = Application.persistentDataPath + "/saveSlot" + CurrentSelectedSlot;
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath); // Create the slot folder if it doesn't exist
            }

            savePath = Path.Combine(savePath, data.SavePath + ".dat"); // Combine the file path

            if (File.Exists(savePath))
            {
                file = File.Open(savePath, FileMode.Open);
            }
            else
            {
                file = File.Create(savePath);
            }

            formatter.Serialize(file, data);
            file.Close();
        }

        // Finds and loads a file of the given type, or creates a new one with default values if it doesn't exist.
        public T LoadData<T>() where T : ISaveable, new()
        {
            string loadPath = Application.persistentDataPath + "/saveSlot" + CurrentSelectedSlot;
            if (!Directory.Exists(loadPath))
            {
                Directory.CreateDirectory(loadPath); // Create the slot folder if it doesn't exist
            }

            loadPath = Path.Combine(loadPath, new T().SavePath + ".dat"); // Combine the file path

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file;

            if (File.Exists(loadPath))
            {
                file = File.Open(loadPath, FileMode.Open);
                T loadedData = (T)formatter.Deserialize(file);
                file.Close();
                return loadedData;
            }
            else
            {
                T data = new();
                data.SetDefaultValues();

                SaveData(data);
                return data;
            }
        }

        // Deletes data for the specified class type, with an optional path.
        public void DeleteData<T>(string optionalPath = null) where T : ISaveable, new()
        {
            T willDeleteData = new();

            string path;

            if (optionalPath == null)
                path = willDeleteData.SavePath;
            else
                path = optionalPath;

            path = Application.persistentDataPath + "/saveSlot" + CurrentSelectedSlot + "/" + path + ".dat"; // Create the file path

            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("Data deleted: " + path);
            }
            else
            {
                Debug.LogWarning("No file at this path: " + path);
            }
        }

        // Deletes data for the specified slot index.
        public void DeleteSlot(int slotIndex)
        {
            string slotPath = Application.persistentDataPath + "/saveSlot" + slotIndex;
            if (Directory.Exists(slotPath))
            {
                Directory.Delete(slotPath, true); // Delete the folder and its contents
                Debug.Log("Slot " + slotIndex + " deleted successfully.");
            }
            else
            {
                Debug.LogWarning("Slot folder not found: " + slotPath);
            }
        }

        // Deletes all saved data to reset to factory settings.
        public void DeleteAllData()
        {
            string persistentDataPath = Application.persistentDataPath;
            if (Directory.Exists(persistentDataPath))
            {
                Directory.Delete(persistentDataPath, true); // Delete everything inside
                Debug.Log("Persistent data deleted.");
            }
            else
            {
                Debug.LogWarning("Persistent data folder not found: " + persistentDataPath);
            }
        }
    }

}
