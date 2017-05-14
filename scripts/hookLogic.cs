using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fishingLineLogic;

public class hookLogic : MonoBehaviour
{
    public GameObject FishingLine;
    public UltimateRope Rope;
    public GameObject LineEnd;
    public GameObject LineStart;
	public AudioSource splashClip;
    public GameObject FishParent;
    FishLogic[] fishList;
    List<GameObject> fishLineSegments = new List<GameObject>();

    float m_fRopeExtension;
    // Use this for initialization
    void Start()
    {
        m_fRopeExtension = Rope != null ? Rope.m_fCurrentExtension : 0.0f;
        fishList = FishParent.GetComponentsInChildren<FishLogic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Rigidbody hookParent = GameObject.Find("hookParent").GetComponent<Rigidbody>();

        foreach (Transform child in FishingLine.transform)
        {
            if(!fishLineSegments.Contains(child.gameObject))
                fishLineSegments.Add(child.gameObject);
        }

        if (col.gameObject.name == "hook")
        {
            if (!splashClip.isPlaying)
            {
                splashClip.Play();
            }
            foreach (FishLogic fish in fishList)
            {
                fish.userIsFishing = true;
            }
            hookParent.drag = 80;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Rigidbody hookParent = GameObject.Find("hookParent").GetComponent<Rigidbody>();

        if (col.gameObject.name == "hook")
        {
            hookParent.drag = 0;
            foreach (FishLogic fish in fishList)
            {
                fish.userIsFishing = false;
            }
        }
    }
}
