using UnityEngine;
using System;
using System.IO;


public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private readonly string encryptionCode = "dwearetheyolomonkeysswordrunasdfsdfafmikewazawski";
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public SaveData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        SaveData data = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string datatoload = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        datatoload = reader.ReadToEnd();
                    }
                }
                datatoload = EncryptDecrypt(datatoload);
                data = JsonUtility.FromJson<SaveData>(datatoload);
            }
            catch (Exception e)
            {
                Debug.LogError("Error shit: " + e);
            }
        }
        return data;
    }

    public void Save(SaveData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            string json = JsonUtility.ToJson(data);
            json = EncryptDecrypt(json);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error fk : " + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCode[i % encryptionCode.Length]);
        }
        return modifiedData;
    }
}
