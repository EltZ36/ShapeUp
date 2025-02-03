using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "ShapeRecipesScriptableObject",
    menuName = "Shapes/ShapeRecipesScriptableObject"
)]
public class ShapeRecipes : ScriptableObject
{
    [SerializeField]
    private List<RecipeInfo> shapeRecipesList;
    public Dictionary<HashSet<ShapeType>, ShapeType> ShapeRecipesDict { get; private set; }
    public Dictionary<ShapeType, HashSet<ShapeType>> ShapeRecipesDictInverse { get; private set; }

    //This assumes that there is only 1 recipe for each  shape
    public void OnEnable()
    {
        if (shapeRecipesList != null && ShapeRecipesDict == null && ShapeRecipesDictInverse == null)
        {
            ShapeRecipesDict = new Dictionary<HashSet<ShapeType>, ShapeType>(
                new ShapePairComparer()
            );
            ShapeRecipesDictInverse = new Dictionary<ShapeType, HashSet<ShapeType>>();
            foreach (RecipeInfo recipe in shapeRecipesList)
            {
                HashSet<ShapeType> pair = new HashSet<ShapeType> { recipe.shapeA, recipe.shapeB };
                if (!ShapeRecipesDict.ContainsKey(pair))
                {
                    ShapeRecipesDict.Add(pair, recipe.shapeResult);
                }
                if (!ShapeRecipesDictInverse.ContainsKey(recipe.shapeResult))
                {
                    ShapeRecipesDictInverse.Add(recipe.shapeResult, pair);
                }
            }
        }
    }

    private class ShapePairComparer : IEqualityComparer<HashSet<ShapeType>>
    {
        public bool Equals(HashSet<ShapeType> x, HashSet<ShapeType> y)
        {
            return x.SetEquals(y);
        }

        public int GetHashCode(HashSet<ShapeType> pair)
        {
            int hash = 0;
            foreach (ShapeType shape in pair)
            {
                hash ^= shape.GetHashCode();
            }
            return hash;
        }
    }
}
