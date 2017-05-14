using NewtonVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNVRInteractable : NVRInteractableItem {

    public FishLogic fishLogic;
    public override void BeginInteraction(NVRHand hand)
    {
        base.BeginInteraction(hand);

        // Our addition
        Animation fishAnimation = this.GetComponentInChildren<Animation>();
        Rigidbody fishRigid = this.GetComponent<Rigidbody>();
        CharacterJoint fishJoint = this.GetComponent<CharacterJoint>();
        Destroy(fishJoint);
        fishRigid.mass = 5;
        fishRigid.useGravity = true;

        fishAnimation.Stop();
        fishLogic.caught = false;
        fishLogic.fishDead = true;
        // End of our addition

        StartingDrag = Rigidbody.drag;
        StartingAngularDrag = Rigidbody.angularDrag;
        Rigidbody.drag = 0;
        Rigidbody.angularDrag = 0.05f;

        if (DisablePhysicalMaterialsOnAttach == true)
        {
            DisablePhysicalMaterials();
        }

        PickupTransform = new GameObject(string.Format("[{0}] NVRPickupTransform", this.gameObject.name)).transform;
        PickupTransform.parent = hand.transform;
        PickupTransform.position = this.transform.position;
        PickupTransform.rotation = this.transform.rotation;

        ResetVelocityHistory();

        if (OnBeginInteraction != null)
        {
            OnBeginInteraction.Invoke();
        }
    }
}
