using NewtonVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles hiding and showing menus
/// </summary>
public class menuHide : MonoBehaviour {

    bool fishingMenu = false;
    public GameObject fishMenu;
    public GameObject nvrPlayer;
    public GameObject fishPad;
	// Use this for initialization
	void Start () {
        fishingMenu = false;
        nvrPlayer.transform.position = fishPad.transform.position;
    }

    // Update is called once per frame
    void Update() {
        // Check if the user is at the fishing location
        if (nvrPlayer.transform.position == fishPad.transform.position)
        {
            // If the user touches the menu button, handle hiding and showing the fishing menu
            if ((NVRPlayer.Instance.LeftHand.Inputs[NVRButtons.ApplicationMenu].PressDown || NVRPlayer.Instance.RightHand.Inputs[NVRButtons.ApplicationMenu].PressDown) && fishingMenu == true)
            {
                fishMenu.SetActive(false);
                fishingMenu = false;
            }
            else if ((NVRPlayer.Instance.LeftHand.Inputs[NVRButtons.ApplicationMenu].PressDown || NVRPlayer.Instance.RightHand.Inputs[NVRButtons.ApplicationMenu].PressDown) && fishingMenu == false)
            {
                fishMenu.SetActive(true);
                fishingMenu = true;
            }
        }
    }
}
