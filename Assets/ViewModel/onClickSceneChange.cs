using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The onClickSceneChange class handles the changing of a scene upon clicking of a button within the game
/// </summary>
public class onClickSceneChange : MonoBehaviour
{
    public void LoadScene(string pAreaName)
    {
        SceneManager.LoadScene(pAreaName); //Loads the specified area
    }

    public void quitGame()
    {
        Application.Quit(); //Quits the game
    }
}