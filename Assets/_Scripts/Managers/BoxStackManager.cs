//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
namespace MeteTurkay{
	public class BoxStackManager : StackManager
    {
        List<GameObject> stacks;
        public override void MoveStackObjects(bool moving)
        {
            itemHolderTransform.transform.position = Vector3.Lerp(itemHolderTransform.transform.position, player.position, 1);
            for (int i = 1; i < collectedStacks.Count; i++)
            {
                if (moving)
                {
                    Vector3 pos = collectedStacks[i].transform.localPosition;
                    //stackFloatingValue.x = isLookingLeft ? 0.1f : -0.1f;
                    stackFloatingValue.x = isLookingFoward ? Mathf.Clamp(joystickInput.y / 100, 0, 0.05f) : Mathf.Clamp(joystickInput.y / 100, -0.05f, 0f);
                    stackFloatingValue.y = !isLookingLeft ? Mathf.Clamp(joystickInput.x / 100, 0, 0.05f) : Mathf.Clamp(joystickInput.x / 100, -0.05f, 0f);
                    pos.x = (collectedStacks[i - 1].transform.localPosition.x - stackFloatingValue.y) * i / 10;
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
        public override void AddNewItem(Transform stackUnit)
        {
            print("boxStack add item called");
            float nextPositionY = stackSpacing * numberOfItemsHolding;
            numberOfItemsHolding++;
            stackUnit.DOJump(itemHolderTransform.position + new Vector3(0, nextPositionY, 0), 2f, 1, 1f).OnComplete(
            () =>
            {
                stackUnit.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.FastBeyond360).SetLoops(2, LoopType.Incremental).OnComplete(() => {

                    stackUnit.SetParent(itemHolderTransform, true);
                    stackUnit.DOLocalMove(Vector3.zero + new Vector3(0, nextPositionY, 0), 1f).OnComplete(() => {
                        stackUnit.localRotation = Quaternion.identity;
                        collectedStacks.Add(stackUnit.gameObject);
                    });
                });
            }
             );
        }
        public override void SubStackUnity()
        {
            BoxStackUnit.PlayerContact += AddNewItem;
        }
        public void GetStacks()
        {
            stacks = ObjectPooler.SharedInstance.GetAllPooledObjects(0);
        }
        //called from red cube on scene 1 with ActionOnTrigger.cs
        public void StartStacks()
        {
            GetStacks();
            for (int i = 0; i < stacks.Count; i++)
            {
                stacks[i].SetActive(true);
            }
        }
    }
}
