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
                if (i != 0)
                {
                    float distance = Vector3.Distance(collectedStacks[i].transform.position, collectedStacks[i - 1].transform.position);
                    //rotate to look at the player
                    collectedStacks[i].transform.rotation = Quaternion.Slerp(collectedStacks[i].transform.rotation, Quaternion.LookRotation(collectedStacks[i - 1].transform.position - collectedStacks[i].transform.position), 10 * Time.deltaTime);

                    if (distance > 0.4f)
                    {
                        //move towards the player
                        collectedStacks[i].transform.position += 8 * Time.deltaTime * collectedStacks[i].transform.forward;
                    }
                }
                else
                {
                    float distance = Vector3.Distance(collectedStacks[i].transform.position, player.position);
                    //rotate to look at the player
                    collectedStacks[i].transform.rotation = Quaternion.Slerp(collectedStacks[i].transform.rotation, Quaternion.LookRotation(player.parent.localPosition - collectedStacks[i].transform.position), 10 * Time.deltaTime);

                    //move towards the player
                    if (distance > 0.5f)
                    {
                        collectedStacks[i].transform.position += 8 * Time.deltaTime * collectedStacks[i].transform.forward;
                    }

                }
            }
        }
    }
}
