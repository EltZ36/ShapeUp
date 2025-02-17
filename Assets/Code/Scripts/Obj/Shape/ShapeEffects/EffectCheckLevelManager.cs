using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectCheckLevelManager : MonoBehaviour
{
    [SerializeField]
    string sublevelname;

    public void DestroyIfIncomplete(EventInfo eventInfo)
    {
        LevelInfo levelInfo = LevelManager.Instance.Levels[LevelManager.Instance.currentLevelID];
        SubLevelInfo sublevel = levelInfo.SubLevels.FirstOrDefault(sublevel =>
            sublevel.SceneName == sublevelname
        );
        if (sublevel.IsComplete == false)
        {
            Destroy(eventInfo.TargetObject);
        }
    }
}
