using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;


public class SinglePlayer : MonoBehaviour
{
    public RawImage rawImage;
    public RawImage rawImageFight;
    public VideoPlayer videoPlayer;
    public GameObject canvasCheat;
    public int random;
    public int[] pattern;
    public int[] playerSign;
    public float difficulty;
    public int patternPosition = 0;
    public bool rockClicked;
    bool isActive = false;
    bool paperClicked;
    bool scissorsClicked;
    public GameObject draw;
    public GameObject win;
    public GameObject lose;
    public Text cheatText;
    public bool used = false;

    //videoClip to play
    public VideoClip videoRR;
    public VideoClip videoSS;
    public VideoClip videoPP;
    public VideoClip videoRP;
    public VideoClip videoRS;
    public VideoClip videoPR;
    public VideoClip videoPS;
    public VideoClip videoSP;
    public VideoClip videoSR;
    public VideoClip videoIdle;

    void Awake()
    {
        used = false;
        rockClicked = false;
        paperClicked = false;
        scissorsClicked = false;
        rawImageFight.enabled = false;
        draw.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
    }

    void Start()
    {
        rawImageFight.enabled = false;
        patternPosition = 0;
        pattern = PatternGenerator(5);
        playerSign = new int[pattern.Length];
        videoPlayer.clip = videoIdle;
        StartCoroutine(PlayVideo());
    }
    void Update()
    {

        //Check if pattern have been found
        if (patternPosition == pattern.Length)
        {
            patternPosition = 0;
            if (PatternFind(pattern, playerSign, difficulty))
            {
                pattern = PatternGenerator(pattern.Length + 1);
                playerSign = new int[pattern.Length];
            }
        }
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
        yield return new WaitForSeconds(3);
        draw.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        while (!videoPlayer.isPrepared)
        {
            rawImageFight.enabled = true;
            yield return waitForSeconds;

            break;
        }
        if (videoPlayer.isPrepared)
        {
            rawImageFight.enabled = false;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    void P1Win()
    {
        StartCoroutine(Winp1());
    }
    IEnumerator Winp1()
    {
        yield return new WaitForSeconds(3);
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
        yield return new WaitForSeconds(3);
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
        if (!used)
        {
            playerSign[patternPosition] = 1;
            IsWin(1, pattern[patternPosition], true);
            patternPosition++;
            rockClicked = true;
            used = true;
        }
    }

    /// <summary>
    /// Paper function trigger click.
    ///Save player sign played and check for win
    /// </summary>
    public void Paper()
    {
        if (!used)
        {
            playerSign[patternPosition] = 2;
            IsWin(2, pattern[patternPosition], true);
            patternPosition++;
            paperClicked = true;
            used = true;
        }
    }

    /// <summary>
    /// Scissors function trigger click.
    ///Save player sign played and check for win
    /// </summary>
    public void Scissors()
    {
        if (!used)
        {
            playerSign[patternPosition] = 3;
            IsWin(3, pattern[patternPosition], true);
            patternPosition++;
            scissorsClicked = true;
            used = true;
        }

    }

    /// <summary>
    /// Returns 1 if pattern find or -1 if false.
    /// </summary>
    /// <param name="pattern">pattern used to play with generate with patternGenerator()</param>
    /// <param name="playerSigns">list signs played</param>
    /// <returns>Returns 1 if pattern find or -1 if false.</returns>
    public int[] PatternGenerator(int taille)
    {
        int[] pattern = new int[taille];
        for (var i = 0; i < taille; i++)
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
    public bool PatternFind(int[] pattern, int[] playerSigns, float averageWin)
    {
        int size = pattern.Length;
        float perc = 0;
        for (var i = 0; i < size; i++)
        {
            if (IsWin(playerSigns[i], pattern[i], false))
            {
                perc++;
            }
        }
        if ((perc / size) >= averageWin)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns 1 if player win  if false. Use Draw() function if player draw
    /// </summary>
    /// <param name="playerSign">Sign player by the player</param>
    /// <param name="botSign">Bot sign</param>
    /// <param name="verif">Verif is a flag lunch coroutine Draw() / P1Win() /  P2Win(); to print win or lose </param>
    /// <returns>Returns true if palyer win else return false.</returns>
    public bool IsWin(int playerSign, int botSign, bool verif)
    {
        if (playerSign == botSign)
        {
            if (verif)
            {
                if (playerSign == 1)
                {
                    videoPlayer.clip = videoRR;
                    StartCoroutine(PlayVideo());
                }
                else if (playerSign == 2)
                {
                    videoPlayer.clip = videoPP;
                    StartCoroutine(PlayVideo());
                }
                else
                {
                    videoPlayer.clip = videoSS;
                    StartCoroutine(PlayVideo());
                }
                Draw();
            }
            return false;
        }
        else if (playerSign == 1 && botSign == 3)
        {
            if (verif)
            {
                videoPlayer.clip = videoRS;
                StartCoroutine(PlayVideo());
                P1Win();
            }
            return true;
        }
        else if (playerSign == 2 && botSign == 1)
        {
            if (verif)
            {
                videoPlayer.clip = videoPR;
                StartCoroutine(PlayVideo());
                P1Win();
            }
            return true;
        }
        else if (playerSign == 3 && botSign == 2)
        {
            if (verif)
            {
                videoPlayer.clip = videoSP;
                StartCoroutine(PlayVideo());
                P1Win();
            }
            return true;
        }
        else if (playerSign == 2 && botSign == 3)
        {
            if (verif)
            {
                videoPlayer.clip = videoPS;
                StartCoroutine(PlayVideo());
                P2Win();
            }
            return false;
        }
        else if (playerSign == 1 && botSign == 2)
        {
            if (verif)
            {
                videoPlayer.clip = videoRP;
                StartCoroutine(PlayVideo());
                P2Win();
            }
            return false;
        }
        else if (playerSign == 3 && botSign == 1)
        {
            if (verif)
            {
                videoPlayer.clip = videoSR;
                StartCoroutine(PlayVideo());
                P2Win();
            }
            return false;
        }
        else
            return false;
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

    /// <summary>
    /// set the Cheat text
    /// </summary>
    /// <param name="pattern">Patern played by IA</param>
    /// <returns> set the Cheat text</returns>
    public string SetCheatText(int[] pattern)
    {
        string text = "";
        foreach (int item in pattern)
        {
            if (item == 1)
            {
                text = text + "Pierre \n";
            }
            else if (item == 2)
            {
                text = text + "Feuille \n";
            }
            else if (item == 3)
            {
                text = text + "ciseaux \n";
            }
        }
        return text;
    }

    /// <summary>
    /// OnClick cheat button
    /// </summary>

    public void TaskOnClickCheat()
    {
        isActive = !isActive;
        canvasCheat.GetComponentInChildren<Text>().text = SetCheatText(pattern);
        if (isActive == true)
        {
            
            canvasCheat.SetActive(false);
        }
        else
        {
            canvasCheat.SetActive(true);
        }
    }
}