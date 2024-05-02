using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
#region Settings
    public List<float> classesProbabilityWeights = new List<float>
    {
        0.0f, // 0: person
        0.0f, // 1: bicycle
        2.0f, // 2: car
        1.0f, // 3: motorcycle
        0.0f, // 4: airplane
        0.0f, // 5: bus
        0.0f, // 6: train
        1.0f, // 7: truck
        0.0f, // 8: cone
    };
#endregion

    private static GlobalSettings instance;

    public static GlobalSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameController.Instance.GetComponent<GlobalSettings>();
                if (instance == null)
                {
                    GameController.Instance.gameObject.AddComponent<GlobalSettings>();
                }
            }
            return instance;
        }
    }
}
