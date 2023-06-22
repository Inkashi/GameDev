using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadScript : MonoBehaviour
{
    string FilePath;
    public GameObject Hero;
    private void Start()
    {
        FilePath = Application.persistentDataPath + "/save.data";
    }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(FilePath, FileMode.Create);
        Save save = new Save();
        save.SaveHero(Hero);
        bf.Serialize(fs, save);
        fs.Close();
    }
    public void LoadGame()
    {
        if (!File.Exists(FilePath))
            return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(FilePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        fs.Close();
        Hero.GetComponent<HeroMovement>().LoadHeroData(save.HeroSaves);
    }
}
[Serializable]
public class Save
{
    [Serializable]
    public struct HeroSaveData
    {
        public int Damage, Health, pointsCount;
        public bool DashAccses, DoubleJumpAccses, ThrowAccses;
    }

    public HeroSaveData HeroSaves;
    public void SaveHero(GameObject Hero)
    {
        HeroSaves.Damage = Hero.GetComponent<HeroMovement>().damage;
        HeroSaves.Health = Hero.GetComponent<HeroMovement>().health;
        HeroSaves.pointsCount = Hero.GetComponent<HeroMovement>().pointsCount;
        HeroSaves.DashAccses = Hero.GetComponent<HeroMovement>().DashAccses;
        HeroSaves.DoubleJumpAccses = Hero.GetComponent<HeroMovement>().DoubleJumpAccses;
        HeroSaves.ThrowAccses = Hero.GetComponent<HeroMovement>().ThrowAccses;
    }
}
