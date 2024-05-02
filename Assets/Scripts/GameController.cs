using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Public variables
    [HideInInspector] public List<List<GameObject>> prefabLists;
    [HideInInspector] public SortedList<int, string> foundClassesList;

    // Singleton
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameController>();
                    singletonObject.name = "GameController";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InitializeForegroundObjects();
    }

    void InitializeForegroundObjects()
    {
        prefabLists = new List<List<GameObject>>();
        foundClassesList = new SortedList<int, string>();
        string sep = System.IO.Path.DirectorySeparatorChar.ToString();
        string[] subdirectories = System.IO.Directory.GetDirectories($"Assets{sep}Objects{sep}Foreground");
        foreach (string subdirectory in subdirectories)
        {
            if (!System.IO.Directory.Exists(subdirectory + $"{sep}Prefabs"))
                continue;
            else
            {
                string subdir = subdirectory.Substring(subdirectory.LastIndexOf(sep) + 1);
                foundClassesList.Add(int.Parse(subdir.Split('_')[0]), subdir.Split('_')[1]);
            }
            Debug.Log($"Loading prefabs of class '{foundClassesList.Values.Last()}' from {subdirectory}");
            string[] prefabPaths = System.IO.Directory.GetFiles(subdirectory + $"{sep}Prefabs", "*.prefab");
            List<GameObject> prefabList = new List<GameObject>();
            foreach (string prefabPath in prefabPaths)
            {
                GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                prefabList.Add(prefab);
            }
            prefabLists.Add(prefabList);
            foreach (GameObject prefab in prefabList)
                Debug.Log($"Loaded {prefab.name}");
        }
    }
}
