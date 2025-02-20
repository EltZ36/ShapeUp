using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisableIfIncomplete : MonoBehaviour
{
    [SerializeField]
    string[] names;

    [SerializeField]
    GameObject[] shapes;

    void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            Check(names[i], shapes[i]);
        }
    }

    void Check(string sublevelname, GameObject shape)
    {
        LevelInfo levelInfo = LevelManager.Instance.Levels[LevelManager.Instance.currentLevelID];
        SubLevelInfo sublevel = levelInfo.SubLevels.FirstOrDefault(sublevel =>
            sublevel.SceneName == sublevelname
        );
        if (sublevel.IsComplete == false)
        {
            Destroy(shape);
        }
    }
}
