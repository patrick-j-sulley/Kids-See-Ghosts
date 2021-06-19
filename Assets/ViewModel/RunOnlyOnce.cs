using UnityEngine;

/// <summary>
/// The RunOnlyOnce class handles functions that are to only be run once related to the game object instanstiation 
/// </summary>
public class RunOnlyOnce : MonoBehaviour
{
    /// <summary>
    /// The unique instance of the RunOnlyOnce class
    /// </summary>
    public static RunOnlyOnce instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
}