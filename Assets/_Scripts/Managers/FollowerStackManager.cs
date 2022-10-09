//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using System.Collections.Generic;
namespace MeteTurkay{
	public class FollowerStackManager :StackManager
	{
        public override void SubStackUnity()
        {
            FollowerStackUnit.PlayerContact += AddNewItem;
        }
        override public void AddNewItem(Transform stackUnit)
        {
            float nextPositionY = stackSpacing * numberOfItemsHolding;
            numberOfItemsHolding++;
            stackUnit.SetParent(itemHolderTransform, false);
            Vector3 pos = Vector3.zero;
            pos.z= nextPositionY + stackSpacing;
            stackUnit.localRotation = Quaternion.identity;
            stackUnit.localPosition = pos;
            collectedStacks.Add(stackUnit.gameObject);        
        }
        public override void MoveStackObjects(bool moving)
        {
            for (int i = 1; i < collectedStacks.Count; i++)
            {
                if (moving)
                {
                    Vector3 pos = collectedStacks[i].transform.localPosition;
                    //stackFloatingValue.x = isLookingLeft ? 0.1f : -0.1f;
                    //stackFloatingValue.x = isLookingFoward ? Mathf.Clamp(joystickInput.y / 100, 0, 0.05f) : Mathf.Clamp(joystickInput.y / 100, -0.05f, 0f);
                    stackFloatingValue.y = !isLookingLeft ? Mathf.Clamp(joystickInput.x / 100, 0, 0.05f) : Mathf.Clamp(joystickInput.x / 100, -0.05f, 0f);
                    //pos.x = (collectedStacks[i - 1].transform.localPosition.x - stackFloatingValue.y) * i / 10;
                    pos.z = (collectedStacks[i - 1].transform.localPosition.z - stackFloatingValue.x) * i / 10;
                    //collectedStacks[i - 1].transform.localPosition.z+stackFloatingValue.x* (Mathf.Abs(joystickInput.y)%10);
                    collectedStacks[i].transform.localPosition = pos;
                }
                else
                {
                    Vector3 pos = collectedStacks[i].transform.localPosition;
                    pos.x = 0;
                    pos.z = 0;
                    collectedStacks[i].transform.localPosition = Vector3.Lerp(collectedStacks[i].transform.localPosition, pos, 1 * Time.deltaTime);
                }

            }
        }
    }
}
