//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using System.Collections.Generic;
namespace MeteTurkay{
	public abstract class StackManager : MonoBehaviour
	{
        public Transform itemHolderTransform;
        public float stackSpacing=0.25f;
        public Transform player;
        [Header("Debug")]
        public int numberOfItemsHolding = 0;
        public List<GameObject> collectedStacks = new List<GameObject>();
        public bool moving = false;
        public Vector2 joystickInput;
        public bool isLookingFoward = false;
        public bool isLookingLeft = false;
        public Vector2 stackFloatingValue = new Vector2(0.1f, -0.1f);
        private void Start()
        {
            SubStackUnit();
        }
        abstract public void SubStackUnit();
        abstract public void AddNewItem(Transform stackUnit);
        abstract public void MoveStackObjects(bool moving);
        private void Update()
        {
            MoveStackObjects(moving);
        }

        //we call this from joystick change event on scene 0, in UIVirtualJoystick.cs
        public void GetRotation(Vector2 vector2)
        {
            joystickInput = vector2;
            moving = vector2 != Vector2.zero;
            if (moving)
            {
                isLookingFoward = vector2.y > 0.5f;
                isLookingLeft = vector2.x < -0.5f;
            }
        }
    }
}
