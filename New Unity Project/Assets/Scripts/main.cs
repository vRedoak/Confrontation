using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main : MonoBehaviour {

    public Text WinCount, LosingCount ;
	public void LoadScene(int scena)
    {
        SceneManager.LoadScene(scena);

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Start()
    {
        WinCount.text = Schet.Wins.ToString();
        LosingCount.text = Schet.Losing.ToString();
    }

    public void Pause()
    {
        AudioListener aaa= GetComponent<AudioListener>();
       // aaa.pause = true;
    }
}

public static class Schet
    {
    public static int Wins = 0;
    public static int Losing = 0;
}

public static class Kolods
{
    public static List<Card> AllCards = new List<Card>();
    public static List<Card> MyCards = new List<Card>();
    public static bool ReadyColod ;
    public static Card SelectedCard;
}
