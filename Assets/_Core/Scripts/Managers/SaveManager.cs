using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace KnifeGame.Managers
{
    public static class SaveManager
    {
        public static GameSave CurrentSave;

        private static string FilePath = $"{Application.persistentDataPath}/KnifeData" + (Application.isEditor ? "_DEV" : string.Empty) + ".bytes";

        static SaveManager()
        {
            LoadSave();
        }

        public static void LoadSave()
        {
            if (!File.Exists(FilePath))
            {
                Debug.Log($"Save file does not exsist: {FilePath}");
                CurrentSave = new GameSave();
                return;
            }

            GameSave data = null;
            FileStream fs = new FileStream(FilePath, FileMode.Open);
            fs.Position = 0;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = formatter.Deserialize(fs) as GameSave;
            }
            catch (SerializationException e)
            {
                Debug.LogError($"Failed to deserialize save file: {e.Message}");
            }
            finally
            {
                fs.Close();
            }

            CurrentSave = data ?? new GameSave();

            Debug.Log($"[SaveManager]: Loadded save: {CurrentSave.ToString()}; From: {FilePath}");
        }

        public static void SaveCurrent()
        {
            FileStream fs = new FileStream(FilePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, CurrentSave);
            }
            catch (SerializationException e)
            {
                Debug.LogError($"Failed to serialize save file: {e.Message}");
            }
            finally
            {
                fs.Close();
            }
        }
    }

    [Serializable]
    public class GameSave
    {
        public int coins = 0;
        public Dictionary<GameModes, int> highScore = new Dictionary<GameModes, int>();

        public List<int> ownedItems = new List<int>();
        public int selectedItem;
    }
}
