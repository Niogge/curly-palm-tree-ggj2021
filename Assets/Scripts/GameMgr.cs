using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameProgress
{
    Start = 0,
    Early = 1,
    Mid = 2,
    Late = 3,
    LAST = 4,

}
public class GameMgr : MonoBehaviour
{
    public static GameProgress GameState;

    public Slider VisibilitySlider;

    private void Start()
    {
        GameEventSystem.ProgressGameEvent += ProgressGame;
        GameEventSystem.WinGameEvent += WinGame;

        VisibilitySlider.maxValue = (float)GameProgress.LAST;
    }

    void ProgressGame()
    {
        GameState++;
        VisibilitySlider.value =(float)( GameProgress.LAST - (GameProgress.LAST - GameState));
        if (GameState > GameProgress.Late)
        {
            GameEventSystem.WinGame();
            return;
        }
    }

    void WinGame()
    {
        Debug.Log("u win");
    }
}
