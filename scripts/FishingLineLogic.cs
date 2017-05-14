using UnityEngine;
using System;
using NewtonVR;

namespace fishingLineLogic
{
    public class FishingLineLogic : MonoBehaviour
    {
        public UltimateRope Rope;
        public Rigidbody FishingRod;
        public float castingSpeed;

        static float m_fRopeExtension;

        void Start()
        {
            m_fRopeExtension = Rope != null ? Rope.m_fCurrentExtension : 0.0f;
        }

        // Called every frame
        void Update()
        {            
            bool casting = false;
            bool reelHand = false; // True is right, false is left

            int mag = (int)Math.Round(FishingRod.velocity.magnitude);
            if (mag > 1)
            {
                castingSpeed = mag * 2;
            }
            else
            {
                if (castingSpeed > 0)
                {
                    castingSpeed -= 0.1f;
                }
            }

            if (castingSpeed > 0)
            {
                Debug.Log(castingSpeed);
            }

            // The reel hand is set to the opposite hand of the one that is holding the fishing rod. 
            // The user is casting, when the touchpad of the hand that is holding the rod is being pressed.
            if (NVRPlayer.Instance.LeftHand.IsInteracting)
            {
                casting = NVRPlayer.Instance.LeftHand.Inputs[NVRButtons.Touchpad].IsPressed;
                reelHand = true;
            }
            else if (NVRPlayer.Instance.RightHand.IsInteracting)
            {
                casting = NVRPlayer.Instance.RightHand.Inputs[NVRButtons.Touchpad].IsPressed;
                reelHand = false;
            }

            // If the user is casting, set the extension speed
            if (casting)
            {
                m_fRopeExtension += Time.deltaTime * castingSpeed;
            }

            // Find the reel in speed by getting the position of the users thumb on the touchpad.
            if (NVRPlayer.Instance.LeftHand.Inputs[NVRButtons.Touchpad].IsTouched && reelHand == false)
            {
                Vector2 leftAxis = NVRPlayer.Instance.LeftHand.Inputs[NVRButtons.Touchpad].Axis;
                reelIn(leftAxis);
            }
            else if (NVRPlayer.Instance.RightHand.Inputs[NVRButtons.Touchpad].IsTouched && reelHand == true)
            {
                Vector2 rightAxis = NVRPlayer.Instance.RightHand.Inputs[NVRButtons.Touchpad].Axis;

                reelIn(rightAxis);
            }

            // Extend the rope
            if (Rope != null)
            {
                m_fRopeExtension = Mathf.Clamp(m_fRopeExtension, 0.0f, Rope.ExtensibleLength);
                Rope.ExtendRope(UltimateRope.ERopeExtensionMode.LinearExtensionIncrement, m_fRopeExtension - Rope.m_fCurrentExtension);
            }
        }

        // This function sets the speed to reel in the rope, based on the user's position on the touchpad.
        public static void reelIn(Vector2 axis)
        {
            float reelingSpeed;
            if (axis.y > -1 & axis.y < -0.33)
            {
                reelingSpeed = 0.25f;
            }
            else if (axis.y > -0.33 && axis.y < 0.33)
            {
                reelingSpeed = 0.5f;
            }
            else
            {
                reelingSpeed = 0.75f;
            }
            m_fRopeExtension -= Time.deltaTime * reelingSpeed;
        }
    }
}
