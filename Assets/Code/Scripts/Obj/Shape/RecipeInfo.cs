using System;
using System.Collections;

public enum RecipeType
{
    Combine,
    Separate,
    Both,
}

[Serializable]
public class RecipeInfo
{
    public string shapeA;
    public string shapeB;
    public string shapeC;
    public RecipeType recipeType;
}
