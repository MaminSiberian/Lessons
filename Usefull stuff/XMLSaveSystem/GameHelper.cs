using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;

public class GameHelper : MonoBehaviour
{
    private string path;

    public List<SaveableObject> objects = new List<SaveableObject>();

    private void Awake()
    {
        path = Application.persistentDataPath + "/testsave.xml";
    }
    private void Start()
    {
        Load();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Load();
        }
    }
    public void Save()
    {
        XElement root = new XElement("root");

        objects.ForEach(obj => root.Add(obj.GetElement()));

        root.AddFirst(new XElement("score", Data.score));

        XDocument saveDoc = new XDocument(root);

        File.WriteAllText(path, saveDoc.ToString());
        Debug.Log(path);
    }
    public void Load()
    {
        XElement root = null;

        if (!File.Exists(path))
        {
            Debug.Log("Save data not found");
            if (File.Exists(Application.persistentDataPath + "/level.xml"))
            {
                root = XDocument.Parse(File.ReadAllText(Application.persistentDataPath + "/level.xml")).Element("root");
            }
        }
        else
        {
            root = XDocument.Parse(File.ReadAllText(path)).Element("root");
        }

        if (root == null) 
        {
            Debug.Log("Level load failed");
            return;
        } 

        GenerateScene(root);
    }

    private void GenerateScene(XElement root)
    {
        objects.ForEach(obj => obj.DestroySelf());

        foreach (XElement instance in root.Elements("instance")) 
        {
            Vector3 position = Vector3.zero;

            position.x = float.Parse(instance.Attribute("x").Value);
            position.y = float.Parse(instance.Attribute("y").Value);
            position.z = float.Parse(instance.Attribute("z").Value);

            Instantiate(Resources.Load<GameObject>(instance.Value), position, Quaternion.identity);
        }
    }
}
