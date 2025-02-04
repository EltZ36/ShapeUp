public class LevelCore
{
    public string SceneName;
    public bool IsComplete = false;

    public override string ToString()
    {
        return $"Level: {SceneName}, Status: {(IsComplete ? "Completed" : "Incomplete")}";
    }
}
