using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController Instance;
    public Text PlayerMana, EnemyMana;
    public Text PlayerMana1, EnemyMana1;
    public Text PlayerHP, EnemyHP;

    public GameObject ResultGO;
    public Text ResultText;

    public Text TurnTime, TurnTime1;

    public Button EndTurnBtn;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }
    public void StartGame()
    {
       // EndTurnBtn.interactable = true;
        if (ResultGO!=null)
        ResultGO.SetActive(false);
      
        UpdateHPAndMana();
    }

    public void UpdateHPAndMana()
    {
        PlayerMana.text = GameManagerScr.Instance.CurrentGame.Player.Mana.ToString();
        PlayerMana1.text = GameManagerScr.Instance.CurrentGame.Player.Mana.ToString();
        EnemyMana.text = GameManagerScr.Instance.CurrentGame.Enemy.Mana.ToString();
        EnemyMana1.text = GameManagerScr.Instance.CurrentGame.Enemy.Mana.ToString();

        PlayerHP.text = GameManagerScr.Instance.CurrentGame.Player.HP.ToString();
        EnemyHP.text = GameManagerScr.Instance.CurrentGame.Enemy.HP.ToString();
    }

    public void ShowResult()
    {
        ResultGO.SetActive(true);

        if (GameManagerScr.Instance.CurrentGame.Enemy.HP == 0)
        {
            ResultText.text = "Победа";

            Schet.Wins++;
        }
        else
        {
            ResultText.text = "Проигрыш";
            Schet.Losing++;
        }
    }

    public void UpdateTurnTime(int time)
    {
        TurnTime.text = "00:"+time.ToString();
        TurnTime1.text = "00:" + time.ToString();
    }

    public void DisableTurnBtn()
    {
        EndTurnBtn.interactable = GameManagerScr.Instance.IsPlayerTurn;
    }
}
