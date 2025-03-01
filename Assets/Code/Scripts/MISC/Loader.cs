using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }
}
