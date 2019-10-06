using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public string[] conceptSet { get; set; }
    public HashSet<string> selectedConcept;
    public GameObject execusePanel;
    public GameObject replyPanel;

    public static Vector2 conceptContainerSize = new Vector2(500f, 500f);

    public static GameManager instance;

    private int remainingSecond;

    private bool win = false;

    private void Awake()
    {
        instance = this;
        Screen.SetResolution(600, 1000, false);
        selectedConcept = new HashSet<string>();
        remainingSecond = 100;
        Time.timeScale = 1;
    }
    
    public void restartGame()
    {

        SceneManager.LoadScene("GamePlay");

    }

    public void checkPlanExistence()
    {
        string[] concepts = new string[selectedConcept.Count];
        selectedConcept.CopyTo(concepts);
        Atom[] tmp = ClingoInterface.findPlanWithGivenConcepts(concepts);
        if (tmp != null)
        {
            Debug.Log("Plan Found!");
            replyPanel.SetActive(true);
            replyPanel.GetComponentInChildren<Text>().text = plan2reply(tmp);
            execusePanel.SetActive(true);
            execusePanel.GetComponentInChildren<Text>().text = plan2narrative(tmp);
            win = true;
        }
    }

    public void gameover()
    {
        Debug.Log("game over");
        replyPanel.SetActive(true);
        replyPanel.GetComponentInChildren<Text>().text = "Alright. I'm sorry you failed this course.";
        execusePanel.SetActive(true);
        execusePanel.GetComponentInChildren<Text>().text = "(Damn it! I can't come up with anything...!)";
    }

    private string plan2narrative(Atom[] plan)
    {
        string[] sentences = new string[]{ "", "", "", "", ""};

        foreach(Atom atm in plan)
        {
            string action = atm.args[0];
            int step = int.Parse(atm.args[1]);
            sentences[step] = ResourceLibrary.action2sentence[action];
        }

        return string.Join(". ", sentences) + ".";
        
    }

    private string plan2reply(Atom[] plan) { 
    
        HashSet<string> actions = new HashSet<string>() ;
        for (int i=0; i < plan.Length; i ++)
        {
            actions.Add(plan[i].args[0]);
        }

        string argument = "...Okay, be sure to do homework next time.";
        if (actions.Contains("air_pollution"))
        {
            argument = "Hmm...I thought that drug factory was shut down last month?";
        } else if (actions.Contains("laptop_stolen"))
        {
            argument = "Hmm...didn't you tell me last time that you were too poor to buy a new laptop?";
        } else if (actions.Contains("pollen_spread"))
        {
            argument = "Hmm...didn't you tell me last time that your were not allergic to pollen?";
        } else if (actions.Contains("parent_lost_job"))
        {
            argument = "Hmm...didn't you tell me last time that your parents were govornment officers and their jobs were super secure?";
        } else if (actions.Contains("food_poison"))
        {
            argument = "Really? You were food poisoned for a whole week? Did you even go see the doctor?";
        } else if (actions.Contains("grandparent_disease"))
        {
            argument = "Really? I thought your grandfather passed away 5 years ago...";
        } else if (actions.Contains("parents_dont_know_tech"))
        {
            argument = "Hmm... didn't you tell you me last time that your dad were programmer?";
        } else if (actions.Contains("visited_malicious_website"))
        {
            argument = "Hmm... I wonder what kind of silly virus would do that...";
        } 

        return argument;
    }

    private void releaseConceptTarget(string concept)
    {
        Vector2 initialPos = new Vector2(Random.Range(0.0f, conceptContainerSize.x), Random.Range(-conceptContainerSize.y, 0.0f));
        GameObject targetObj = Instantiate(Resources.Load<GameObject>(ResourceLibrary.path2ConceptTargetPrefab), new Vector2(0.0f, 0.0f), Quaternion.identity, GameObject.FindWithTag("ConceptTargetContainer").transform);
        targetObj.GetComponent<RectTransform>().anchoredPosition = initialPos;
        targetObj.GetComponent<ConceptTarget>().setConcept(concept);
    }

    private void releaseAllConceptTarget()
    {
        foreach(string concept in conceptSet)
        {
            releaseConceptTarget(concept);
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("LoseTime");
        ClingoInterface.populateConceptSet();
        releaseAllConceptTarget();

    }
	
	// Update is called once per frame
	void Update () {
        GameObject.FindGameObjectWithTag("Countdown").GetComponent<Text>().text = remainingSecond.ToString();
	}

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            remainingSecond--;
            if (remainingSecond <= 0 && !win)
            {
                gameover();
                break;
            }
        }
    }

}
