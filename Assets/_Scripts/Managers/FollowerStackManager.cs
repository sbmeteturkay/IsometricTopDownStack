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
            float nextPositionY = -stackSpacing * numberOfItemsHolding;
            numberOfItemsHolding++;
            stackUnit.SetParent(itemHolderTransform, false);
            Vector3 pos = Vector3.zero;
            pos.z= nextPositionY - stackSpacing;
            stackUnit.localRotation = Quaternion.identity;
            //stackUnit.localPosition = pos;
            collectedStacks.Add(stackUnit.gameObject);        
        }
        public override void MoveStackObjects(bool moving)
        {
            for (int i = 0; i < collectedStacks.Count; i++)
            {
                if (true)
                {
                    if(i!= 0){
                        //Vector3 newPoz = collectedStacks[i-1].transform.localPosition;
                        //newPoz.z = collectedStacks[i].transform.position.z;
                        //Vector3 newPoz = collectedStacks[i - 1].transform.position;
                        //newPoz.z -= stackSpacing;
                        //collectedStacks[i].transform.position = Vector3.MoveTowards(collectedStacks[i].transform.position, collectedStacks[i-1].transform.position,stackSpacing*5);
                        //collectedStacks[i].transform.LookAt(collectedStacks[i - 1].transform);
                        // newPoz.z = collectedStacks[i - 1].transform.localPosition.z - stackSpacing;
                        //collectedStacks[i].transform.localRotation = collectedStacks[i-1].transform.localRotation;
                        //stackFloatingValue.x = isLookingLeft ? 0.1f : -0.1f;
                        //stackFloatingValue.x = isLookingFoward ? Mathf.Clamp(joystickInput.y / 100, 0, 0.05f) : Mathf.Clamp(joystickInput.y / 100, -0.05f, 0f);
                        // stackFloatingValue.x = !isLookingLeft ? Mathf.Clamp(joystickInput.x*20 , 0, 1f) : Mathf.Clamp(joystickInput.x*20 , -1f, 0f);
                        //  newPoz.x = (collectedStacks[i - 1].transform.localPosition.x - stackFloatingValue.x) * i / 10;
                        // collectedStacks[i].transform.localPosition = newPoz;
                        //pos.x = (collectedStacks[i - 1].transform.localPosition.x - stackFloatingValue.y) * i / 10;
                        float distance = Vector3.Distance(collectedStacks[i].transform.position, collectedStacks[i-1].transform.position);
                        print(distance);
                        //rotate to look at the player
                        collectedStacks[i].transform.rotation = Quaternion.Slerp(collectedStacks[i].transform.rotation, Quaternion.LookRotation(collectedStacks[i-1].transform.position - collectedStacks[i].transform.position), 10 * Time.deltaTime);

                        if (distance > 0.4f)
                        {
                            //move towards the player
                            collectedStacks[i].transform.position += 8 * Time.deltaTime * collectedStacks[i].transform.forward;
                        }
                    }
                    else
                    {
                        float distance = Vector3.Distance(collectedStacks[i].transform.position, player.position);
                        print(distance);
                        //rotate to look at the player
                        collectedStacks[i].transform.rotation = Quaternion.Slerp(collectedStacks[i].transform.rotation, Quaternion.LookRotation(player.position - collectedStacks[i].transform.position), 10 * Time.deltaTime);

                        //move towards the player
                        if (distance > 0.5f)
                        {
                            collectedStacks[i].transform.position += 8 * Time.deltaTime * collectedStacks[i].transform.forward;
                        }
                        //Vector3 newPoz = player.position;
                        //newPoz.z -= 2*stackSpacing;
                        //collectedStacks[i].transform.position = Vector3.MoveTowards(collectedStacks[i].transform.position, player.position,stackSpacing);
                        //collectedStacks[i].transform.LookAt(player.parent.transform);
                        //collectedStacks[i].transform.localPosition =Vector3.Lerp(collectedStacks[i].transform.localPosition, newPoz, 10 * Time.deltaTime); ;

                    }
                }
                else
                {
                    //Vector3 pos = collectedStacks[i].transform.localPosition;
                    //pos.x = 0;
                    //collectedStacks[i].transform.localPosition = Vector3.Lerp(collectedStacks[i].transform.localPosition, pos, 1 * Time.deltaTime);
                }

            }
        }
    }
}
