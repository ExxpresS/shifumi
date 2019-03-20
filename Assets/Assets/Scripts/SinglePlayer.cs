using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SinglePlayer : MonoBehaviour
{
    public int random;
    public int[] pattern;
    public int[] playerSign;
    public float difficulty;
    public int patternPosition = 0;
    public bool rockClicked; bool paperClicked; bool ScissorsClicked;
    public GameObject draw; public GameObject win; public GameObject lose;
    void Awake()
    {
        rockClicked = false;
        paperClicked = false;
        ScissorsClicked = false;
        draw.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
    }
    void Start()
    {
        patternPosition = 0;
        pattern = patternGenerator(5);
        playerSign = new int[pattern.Length];
    }
    public void Exit()
    {
        Application.LoadLevel("MainMenu");
    }
    void Draw()
    {
        StartCoroutine(drw());
    }
    IEnumerator drw()
    {
        draw.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }
    void Update()
    {
        //Check if pattern have been found
        if (patternPosition == pattern.Length)
        {
            patternPosition = 0;
            if (patternFind(pattern, playerSign, difficulty))
            {
                pattern = patternGenerator(pattern.Length + 1);
                playerSign = new int[pattern.Length];
            }
        }

    }
    void P1Win()
    {
        StartCoroutine(Winp1());
    }
    IEnumerator Winp1()
    {
        win.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }
    void P2Win()
    {
        StartCoroutine(Winp2());
    }
    IEnumerator Winp2()
    {
        lose.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        Awake();
    }
    /// <summary>
    /// Rock function trigger click.
    ///Save player sign played and check for win
    /// </summary>
    public void Rock()
    {
        playerSign[patternPosition] = 1;
        isWin(1, pattern[patternPosition], true);
        patternPosition++;
        rockClicked = true;
    }
    /// <summary>
    /// Paper function trigger click.
    ///Save player sign played and check for win
    /// </summary>
    public void Paper()
    {
        playerSign[patternPosition] = 2;
        isWin(2, pattern[patternPosition], true);
        patternPosition++;
        paperClicked = true;
    }
    /// <summary>
    /// Scissors function trigger click.
    ///Save player sign played and check for win
    /// </summary>
    public void Scissors()
    {
        playerSign[patternPosition] = 3;
        isWin(3, pattern[patternPosition], true);
        patternPosition++;
        ScissorsClicked = true;
    }

    /// <summary>
    /// Returns 1 if pattern find or -1 if false.
    /// </summary>
    /// <param name="pattern">pattern used to play with generate with patternGenerator()</param>
    /// <param name="playerSigns">list signs played</param>
    /// <returns>Returns 1 if pattern find or -1 if false.</returns>
    public int[] patternGenerator(int taille)
    {
        int[] pattern = new int[taille];
        for (int i = 0; i < taille; i++)
        {
            pattern[i] = Random.Range(1, 4);
        }
        Debug.Log(pattern);
        return pattern;
    }

    /// <summary>
    /// Returns 1 if pattern find or -1 if false.
    /// </summary>
    /// <param name="pattern">pattern used to play with generate with patternGenerator()</param>
    /// <param name="playerSigns">list signs played</param>
    /// <param name="averageWin">Select the difficulty, the average win to change pattern</param>
    /// <returns>Returns 1 if pattern find or -1 if false.</returns>
    public bool patternFind(int[] pattern, int[] playerSigns, float averageWin)
    {
        int size = pattern.Length;
        float perc = 0;
        for (int i = 0; i < size; i++)
        {
            if (isWin(playerSigns[i], pattern[i], false))
            {
                perc++;
            }
        }
        Debug.Log((perc / size));
        if ((perc / size) >= averageWin)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns 1 if player win  if false. Use Draw() function if player draw
    /// </summary>
    /// <param name="playerSign">Sign player by the player</param>
    /// <param name="botSign">bobt sign</param>
    /// <param name="verif">verif is a flag lunch coroutine Draw() / P1Win() /  P2Win(); to print win or lose  </param>
    /// <returns>Returns true if palyer win else return false.</returns>
    public bool isWin(int playerSign, int botSign, bool verif)
    {
        if (playerSign == botSign)
        {
            if (verif)
            {
                Draw();
            }
            return false;
        }
        else if (playerSign == 1 && botSign == 3)
        {
            if (verif)
            {
                P1Win();
            }
            return true;
        }
        else if (playerSign == 2 && botSign == 1)
        {
            if (verif)
            {
                P1Win();
            }
            return true;
        }
        else if (playerSign == 3 && botSign == 2)
        {
            if (verif)
            {
                P1Win();
            }
            return true;
        }
        else
        {
            if (verif)
            {
                P2Win();
            }
            return false;
        }
    }


    /// <summary>
    /// set the average value to find pattern depend on difficulty chose
    /// </summary>
    /// <param name="dropDown">Dropdown GameObject to work with</param>
    /// <returns>set average difficulty</returns>
    public void OnDropDownChanged(Dropdown dropDown)
    {
        switch (dropDown.value)
        {
            case 0:
                difficulty = 0.6f;
                break;
            case 1:
                difficulty = 0.8f;
                break;
            case 2:
                difficulty = 1f;
                break;
            default:

                difficulty = 0.8f;
                break;
        }
    }
}
