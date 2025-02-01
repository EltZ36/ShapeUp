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
    /// should be called to begin the game but this should use the scenemanager instead
    /// </summary>
    bool BeginGame(bool onlineMode, bool ready);

    /// <summary>
    /// handler for quit event booleans for save and exit query
    /// </summary>
    void OnQuitEvent(bool save, bool exit);
}

/// <summary>
/// Interface for the LevelManager Singleton
/// </summary>
interface ILevelManager
{
    /// <summary>
    /// Initializes the LevelManager with level information
    /// </summary>
    void Init();

    /// <summary>
    /// Marks level as complete and invokes OnLevelComplete event.
    /// </summary>
    /// <param name="levelID">Based on LevelManager Dict, not build number</param>
    void OnLevelCompleteEvent(int levelID);

    /// <summary>
    /// Moves to the scene named LevelSelect
    /// </summary>
    void LoadLevelSelect();

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
}
