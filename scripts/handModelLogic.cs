using NewtonVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handModelLogic : MonoBehaviour
{
    NVRHand leftHand;
    NVRHand rightHand;
    GameObject leftRenderer;
    GameObject rightRenderer;
    MeshRenderer[] leftMeshes;
    MeshRenderer[] rightMeshes;
    bool rightDisabled = false;
    bool leftDisabled = false;

    // Use this for initialization
    void Start()
    {
        while(rightHand == null || leftHand == null)
        {
            leftHand = NVRPlayer.Instance.LeftHand;
            rightHand = NVRPlayer.Instance.RightHand;
        }
    }

    // Update is called once per frame
    void Update()
    {
        leftRenderer = GameObject.Find("Render Model for LeftHand");
        rightRenderer = GameObject.Find("Render Model for RightHand");
        leftMeshes = leftRenderer.GetComponentsInChildren<MeshRenderer>();
        rightMeshes = rightRenderer.GetComponentsInChildren<MeshRenderer>();

        if (leftHand.IsInteracting)
        {
            if (leftHand.CurrentlyInteracting.name == "FishingRod" && leftRenderer.activeInHierarchy)
            {
                leftDisabled = true;
                foreach (MeshRenderer mesh in leftMeshes)
                {
                    mesh.enabled = false;
                }
            }
        }
        else if (rightHand.IsInteracting)
        {
            rightDisabled = true;
            if (rightHand.CurrentlyInteracting.name == "FishingRod" && rightRenderer.activeInHierarchy)
            {
                foreach (MeshRenderer mesh in rightMeshes)
                {
                    mesh.enabled = false;
                }
            }
        }

        if (!leftHand.IsInteracting && leftDisabled)
        {
            leftDisabled = false;
            foreach (MeshRenderer mesh in leftMeshes)
            {
                mesh.enabled = true;
            }
        }

        if (!rightHand.IsInteracting && rightDisabled)
        {
            rightDisabled = false;
            foreach (MeshRenderer mesh in rightMeshes)
            {
                mesh.enabled = true;
            }
        }
    }
}
