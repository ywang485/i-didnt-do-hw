using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ConceptTarget : MonoBehaviour {

    static public readonly int maxLockedTarget = 4;
    static public readonly float moving_distance = 1000f;
    static public readonly float minDuration = 5.0f;
    static public readonly float maxDuration = 8.0f;

    public bool selected = false;

    private string concept = "None";

    Sequence movingSeq;

    public void setConcept(string nconcept)
    {
        concept = nconcept;
        GetComponentInChildren<Text>().text = nconcept.Replace("_", " ");
    }

    // Use this for initialization
    void Start() {
        initializeMovement();
    }

    void initializeMovement()
    {
        movingSeq = DOTween.Sequence();
        // Decide whether to move vertically or horizontally
        Vector2 dest;
        int drct = Random.Range(0, 4);
        if (drct == 0)
        {
            dest = new Vector2(transform.position.x + moving_distance, transform.position.y);
        }
        else if (drct == 1)
        {
            dest = new Vector2(transform.position.x, transform.position.y + moving_distance);
        }
        else if (drct == 2)
        {
            dest = new Vector2(transform.position.x - moving_distance, transform.position.y);
        }
        else
        {
            dest = new Vector2(transform.position.x, transform.position.y - moving_distance);
        }
        // Decide moving speed
        float duration = Random.Range(minDuration, maxDuration);
        // Activate movement
        movingSeq.Append(transform.DOMove(dest, duration).SetEase(Ease.InOutQuad).OnComplete(Arrive));
    }

    public void onClick()
    {
        if (!selected && GameManager.instance.selectedConcept.Count < maxLockedTarget)
        {
            selected = true;
            movingSeq.Pause();
            Object[] sprites = Resources.LoadAll(ResourceLibrary.path2ConceptTargetSprites);
            GetComponent<Image>().sprite = (Sprite)sprites[3];
            GameManager.instance.selectedConcept.Add(concept);
            GameManager.instance.checkPlanExistence();
        } else
        {
            selected = false;
            movingSeq.Play();
            Object[] sprites = Resources.LoadAll(ResourceLibrary.path2ConceptTargetSprites);
            GetComponent<Image>().sprite = (Sprite)sprites[1];
            GameManager.instance.selectedConcept.Remove(concept);
        }

    }

    void Arrive()
    {
        // Initialize new movement
        Vector2 initialPos = new Vector2(Random.Range(0.0f, GameManager.conceptContainerSize.x), Random.Range(-GameManager.conceptContainerSize.y, 0.0f));
        GetComponent<RectTransform>().anchoredPosition = initialPos;
        initializeMovement();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
