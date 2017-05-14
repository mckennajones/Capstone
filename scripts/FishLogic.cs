using NewtonVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLogic : MonoBehaviour
{

    public GameObject hook;
    public GameObject lineEnd;
    public bool caught = false;
    public bool userIsFishing = false;
    public GameObject FishParent;
    public bool fishDead = false;

    Vector3 initialPosition;
    FishLogic[] fishList;
    bool otherFishCaught;
    // Use this for initialization
    void Start()
    {
        fishList = FishParent.GetComponentsInChildren<FishLogic>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if another fish is currently on the hook
        otherFishCaught = false;
        foreach (FishLogic fish in fishList)
        {
            if (fish.caught == true)
            {
                otherFishCaught = true;
            }
        }

        // Check if fish has not been caught, the user is fishing (hook in water), 
        // another fish in not currently on the hook, and the fish isn't dead
        if (!caught && userIsFishing && !fishDead && !otherFishCaught)
        {
            // If hook is 50 units from fish, fish will begin to follow
            if (Vector3.Distance(hook.transform.position, this.transform.position) < 50)
            {
                Vector3 direction = hook.transform.position - this.transform.position;

                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .2f * Time.deltaTime);

                if (direction.magnitude > 5)
                {
                    this.transform.Translate(0, 0, 0.1f);
                }

                // When hook is close enough, lock onto hook, with a character joint
                // if another fish is not currently not on the hook.
                else if (direction.magnitude <= 5 && !otherFishCaught)
                {
                    caught = true;
                    this.gameObject.AddComponent<CharacterJoint>();
                    CharacterJoint joint = this.GetComponent<CharacterJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = new Vector3(0, 0, 3f);

                    Rigidbody lineEndRigid = lineEnd.GetComponent<Rigidbody>();

                    this.transform.position = lineEnd.transform.position;
                    joint.connectedBody = hook.GetComponent<Rigidbody>();

                    Rigidbody fishRigid = this.GetComponent<Rigidbody>();
                    fishRigid.isKinematic = false;

                    // Move every other fish back to their initial position
                    foreach (FishLogic fish in fishList)
                    {
                        if (fish.gameObject.name != this.name && !fish.fishDead)
                        {
                            fish.gameObject.transform.position = fish.initialPosition;
                        }
                    }
                }
            }

            // If there is a character joint on the fish, it is currently attatched to the hook.
            // Vibrate the controller, while it is.
            if (this.GetComponent<CharacterJoint>())
            {
                if (NVRPlayer.Instance.LeftHand.IsInteracting)
                {
                    NVRPlayer.Instance.LeftHand.TriggerHapticPulse(1500, NVRButtons.Touchpad);
                }
                else if (NVRPlayer.Instance.RightHand.IsInteracting)
                {
                    NVRPlayer.Instance.RightHand.TriggerHapticPulse(1500, NVRButtons.Touchpad);
                }
                return;
            }
        }
    }
}
