using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for the GameManager Singleton
/// </summary>
interface IGameManager
{
    /// <summary>
    /// Save a game state
    /// code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/ and https://weeklyhow.com/how-to-save-load-game-in-unity/
    /// </summary>
    void SaveGame();

    /// <summary>
    /// Clear an entire levels progress from game state
    /// </summary>
    /// <param name="lID">level id</param>
    void ClearLevel(int lID);

    /// <summary>
    /// clear a sublevels progress from game state
    /// </summary>
    /// <param name="lID">level id</param>
    /// <param name="slID">sub level id</param>
    void ClearSubLevel(int lID, int slID);

    /// <summary>
    /// handler for quit event booleans for save and exit query
    /// </summary>
    /// <param name="save">save the game?</param>
    /// <param name="slID">exit the game?</param>
    void OnQuitEvent(bool save, bool exit);
}

/// <summary>
/// Interface for the LevelManager Singleton
/// </summary>
interface ILevelManager
{
    /// <summary>
    /// Update the level progress given game data instance TODO game data instance needs to be properly deserailzed and not
    /// just "fromjson'd"
    /// https://docs.unity3d.com/6000.0/Documentation/ScriptReference/JsonUtility.FromJson.html
    /// </summary>
    /// <returns></returns>
    public void SetLevelProgress(GameData gd);

    /// <summary>
    /// Marks the current level complete.
    /// </summary>
    /// <returns></returns>
    void OnCurrentLevelComplete();

    /// <summary>
    /// Marks the current sub level complete.
    /// </summary>
    /// <returns></returns>
    void OnCurrentSubLevelComplete();

    /// <summary>
    /// returns a dictionary where each key represents a level id, and the value list contains the relative sublevel id that is marked as completed.
    /// </summary>
    /// <returns></returns>
    Dictionary<int, List<int>> GetLevelsProgress();

    /// <summary>
    /// Returns true if all the sub levels of the current level is complete
    /// </summary>
    /// <returns></returns>
    bool CheckAllSubLevelsComplete();

    /// <summary>
    /// Moves to the scene defined by levelID
    /// </summary>
    /// <param name="levelID">Based on LevelManager Dict, not build number</param>
    void LoadLevel(int levelID);

    /// <summary>
    /// Loads SubLevel scene onto the current scene at the defined position. Ensure SubLevel has a single root node.
    /// Only one SubLevel can be loaded at a time.
    /// </summary>
    /// <param name="SubLevelID">Based on SubLevels list relative to current Level</param>
    /// <param name="position">Position of new scene in world space</param>
    /// <returns></returns>
    bool InjectSubLevel(int SubLevelID, Vector3 position);

    /// <summary>
    /// Unloads the current SubLevel.
    /// </summary>
    void UnloadCurrentSubLevel();
}

/// <summary>
/// Interface for the ShapeManager Singleton
/// ShapeManager also implements a check every frame in LateUpdate to merge all colliding shapes with corresponding recipe outputs
/// </summary>
interface IShapeManager
{
    /// <summary>
    /// Adds Shape to current scene and returns gameObject. Triggers OnShapeCreate event
    /// </summary>
    /// <param name="shapeType">Enum representing shape type</param>
    /// <param name="position">position in game world</param>
    /// <param name="tags">tags that are applied to the shape, default to use tags set in the database</param>
    /// <returns></returns>
    public GameObject CreateShape(
        ShapeType shapeType,
        Vector3 position,
        Quaternion rotation = default,
        Vector3 scale = default,
        ShapeTags tags = ShapeTags.UseDatabaseDefault
    );

    /// <summary>
    /// Given a shape, destroy the corresponding gameObject. Triggers OnShapeDestroy event
    /// </summary>
    /// <param name="shape"></param>
    public void DestroyShape(Shape shape);

    /// <summary>
    /// Given two ShapeType enums, return the corresponding ShapeType output according the loaded recipe. Returns null if no output is found.
    /// </summary>
    /// <param name="shapeA">The first shape</param>
    /// <param name="shapeB">The second shape</param>
    /// <returns></returns>
    public ShapeType? CombineShapes(ShapeType shapeA, ShapeType shapeB);

    /// <summary>
    /// Returns a HashSet containing the two shapes used to create the shape being taken apart.
    /// </summary>
    /// <param name="shape">Shape being taken apart</param>
    /// <returns></returns>
    public HashSet<ShapeType> TakeApartShape(ShapeType shape);

    /// <summary>
    /// Prevent a shape from being combined for a set amount of time
    /// </summary>
    /// <param name="shape">The shape being blacklisted</param>
    /// <param name="time">Time in seconds</param>
    public void Blacklist(Shape shape, float time);
}
