using UnityEngine;
using System.Collections;

public class MultiPlayer : MonoBehaviour {
    bool rockClicked1; bool paperClicked1; bool ScissorsClicked1;
    bool rockClicked2; bool paperClicked2; bool ScissorsClicked2;
    public GameObject p1win; public GameObject p2win; public GameObject draw;
    public GameObject p1turn; public GameObject p2turn;
    void Awake()
    {
        rockClicked1 = false;
        paperClicked1 = false;
        ScissorsClicked1 = false;
        rockClicked2 = false;
        paperClicked2 = false;
        ScissorsClicked2 = false;
        p1turn.gameObject.SetActive(true);
        p2turn.gameObject.SetActive(false);
        p1win.gameObject.SetActive(false);
        p2win.gameObject.SetActive(false);
        draw.gameObject.SetActive(false);
    }
    public void Rock1 ()
    {
        p2turn.gameObject.SetActive(true);
        rockClicked1 = true;
        p1turn.gameObject.SetActive(false);
    }
    public void Rock2()
    {
        rockClicked2 = true;
        p1turn.gameObject.SetActive(true);
        p2turn.gameObject.SetActive(false);

    }
    public void Paper1()
    {
        p1turn.gameObject.SetActive(false);

        p2turn.gameObject.SetActive(true);
        paperClicked1 = true;
    }
    public void Paper2()
    {
        paperClicked2 = true;
        p1turn.gameObject.SetActive(true);
        p2turn.gameObject.SetActive(false);

    }
    public void Scissors1()
    {
        p2turn.gameObject.SetActive(true);
        p1turn.gameObject.SetActive(false);

        ScissorsClicked1 = true;
    }
    public void Scissors2()
    {
        p2turn.gameObject.SetActive(false);

        p1turn.gameObject.SetActive(true);
        ScissorsClicked2 = true;
    }

    void Update () {
	    if(rockClicked1 == true && rockClicked2 == true)
        {
            StartCoroutine(Draw());
        }
        if (rockClicked1 == true && paperClicked2 == true)
        {
            StartCoroutine(P2Win());
        }
        if (rockClicked1 == true && ScissorsClicked2 == true)
        {
            StartCoroutine(P1Win());
        }
        if (paperClicked1 == true && paperClicked2 == true)
        {
            StartCoroutine(Draw());
        }
        if (paperClicked1 == true && rockClicked2 == true)
        {
            StartCoroutine(P1Win());
        }
        if (paperClicked1 == true && ScissorsClicked2 == true)
        {
            StartCoroutine(P2Win());
        }
        if (ScissorsClicked1 == true && ScissorsClicked2 == true)
        {
            StartCoroutine(Draw());
        }
        if (ScissorsClicked1 == true && paperClicked2 == true)
        {
            StartCoroutine(P1Win());
        }
        if (ScissorsClicked1 == true && rockClicked2 == true)
        {
            StartCoroutine(P2Win());
        }


    }

    IEnumerator P1Win()
    {
        p1win.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }
    IEnumerator P2Win()
    {
        p2win.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }
    IEnumerator Draw()
    {
        draw.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }
    public void Exit ()
    {
        Application.LoadLevel("MainMenu");
    }
}
