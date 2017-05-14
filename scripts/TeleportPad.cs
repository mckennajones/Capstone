using System.Collections;
using UnityEngine;


/// <summary>
/// This class handles teleporting the user between the campsite and the river
/// </summary>
public class TeleportPad : MonoBehaviour
{
    private SteamVR_Controller.Device leftController { get { return SteamVR_Controller.Input(0); } }
    private SteamVR_Controller.Device rightController { get { return SteamVR_Controller.Input(1); } }

    void OnTriggerEnter(Collider collider)
    {
        GameObject riverPad = GameObject.Find("RiverPad");
        GameObject campPad = GameObject.Find("CampPad");
        GameObject nvrPlayer = GameObject.Find("NVRPlayer");

        // 'trackhat' is the tag assigned to the SteamVR controllers.
        if (collider.gameObject.name == "trackhat")
        {
            // If we are at the river, teleport to the campsite
            if (nvrPlayer.transform.position == riverPad.transform.position)
            {
                nvrPlayer.transform.position = campPad.transform.position;
            }
            // If we are the campsite, go to the river
            else
            {
                nvrPlayer.transform.position = riverPad.transform.position;
            }
        }
    }
}
