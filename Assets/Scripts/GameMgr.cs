using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameProgress
{
    Start = 0,
    Early = 1,
    Mid = 2,
    Late = 3,
    LAST = 4

}
public class GameMgr : MonoBehaviour
{
    public static GameProgress GameState;

    public Slider VisibilitySlider;

    private void Start()
    {
        GameEventSystem.ProgressGameEvent += ProgressGame;
        GameEventSystem.WinGameEvent += WinGame;
        GameEventSystem.LoseGameEvent += LoseGame;
        GameState = GameProgress.Start;
        VisibilitySlider.maxValue = (float)GameProgress.LAST;

        List<string> messages = new List<string>();
        messages.Add("Piliffo is lost on this remnote island, you've got to help him!");
        messages.Add("Explore the island and gather materials to build the signal you need to be rescued!");
        messages.Add("This is done in some steps, first of all you shoukld gather some woods and stones");
        messages.Add("So what are you waiting for? Start your journey, but be carefu....");
        messages.Add("you may not be alone here...");

        GameEventSystem.ShowDialogPopup(messages, "Got it!");
    }

    void ProgressGame()
    {
        GameState++;
        VisibilitySlider.value = (float)( GameProgress.LAST - (GameProgress.LAST - GameState));
        if (GameState == GameProgress.LAST)
        {
            GameEventSystem.WinGame();
            return;
        }
    }
    void LoseGame()
    {
        SceneManager.LoadScene(2);
    }
    void WinGame()
    {
        Debug.Log("u win");
    }
}
