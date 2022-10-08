//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
namespace MeteTurkay{
	public class StackManager : MonoBehaviour
	{
        [SerializeField] Transform itemHolderTransform;
        [SerializeField] float stackSpacing=0.25f;
        int numberOfItemsHolding = 0;
        List<GameObject> stacks;
        List<GameObject> collectedStacks=new List<GameObject>();
        [SerializeField] Transform player;
        [SerializeField] bool moving = false;
        bool firstMoveEnter = true;
        private void Start()
        {
            StackUnit.PlayerContact += StackUnit_PlayerContact;
            GetStacks();
        }

        private void StackUnit_PlayerContact(Transform obj)
        {
            AddNewItem(obj);
        }
        private void Update()
        {
            MoveStackObjects(moving);
            itemHolderTransform.transform.position = Vector3.Lerp(itemHolderTransform.transform.position, player.position, 1);
            if (moving)
            {
                //if (firstMoveEnter)
                //{
                //    firstMoveEnter = false;
                //    itemHolderTransform.rotation = player.rotation;
                //}
                //itemHolderTransform.transform.SetParent(transform.parent.parent,false);
            }
            else
            {
                //firstMoveEnter = true;
                //itemHolderTransform.transform.SetParent(player,true);
            }
        }
        public void AddNewItem(Transform stackUnit)
        {
            float nextPositionY= stackSpacing * numberOfItemsHolding;
            numberOfItemsHolding++;
            stackUnit.DOJump(itemHolderTransform.position+new Vector3(0,nextPositionY,0),2f,1,1f).OnComplete(
            () =>
                {
                    stackUnit.DORotate(new Vector3(0, 180, 0), 0.2f,RotateMode.FastBeyond360).SetLoops(2,LoopType.Incremental).OnComplete(()=> {

                        stackUnit.SetParent(itemHolderTransform, true);
                        stackUnit.DOLocalMove(Vector3.zero + new Vector3(0, nextPositionY, 0), 1f).OnComplete(()=> {
                            stackUnit.localRotation = Quaternion.identity;
                            collectedStacks.Add(stackUnit.gameObject);
                        });
                    });
                }
             );
        }
        Vector2 stackFloatingValue =new Vector2(0.1f,-0.1f);
        void MoveStackObjects(bool moving)
        {
            for(int i = 1; i < collectedStacks.Count; i++)
            {
                if (moving) {
                    Vector3 pos = collectedStacks[i].transform.localPosition;
                    //stackFloatingValue.x = isLookingLeft ? 0.1f : -0.1f;
                    stackFloatingValue.x = isLookingFoward ? Mathf.Clamp(joystickInput.y/100,0,0.05f) : Mathf.Clamp(joystickInput.y/100, -0.05f, 0f);
                    stackFloatingValue.y= !isLookingLeft ? Mathf.Clamp(joystickInput.x / 100, 0, 0.05f) : Mathf.Clamp(joystickInput.x / 100, -0.05f, 0f);
                    print(stackFloatingValue.x);
                    pos.x = (collectedStacks[i - 1].transform.localPosition.x - stackFloatingValue.y)*i/10;
                    pos.z = (collectedStacks[i - 1].transform.localPosition.z - stackFloatingValue.x)*i/10;
                    //collectedStacks[i - 1].transform.localPosition.z+stackFloatingValue.x* (Mathf.Abs(joystickInput.y)%10);
                    collectedStacks[i].transform.localPosition = pos;
                }
                else
                {
                    Vector3 pos = collectedStacks[i].transform.localPosition;
                    pos.x = 0;
                    pos.z = 0;
                    collectedStacks[i].transform.localPosition = Vector3.Lerp(collectedStacks[i].transform.localPosition,pos, 1*Time.deltaTime);
                }

            }
        }
        public void GetStacks()
        {
            stacks=ObjectPooler.SharedInstance.GetAllPooledObjects(0);
        }
        public void StartStacks()
        {
            for(int i = 0; i < stacks.Count; i++)
            {
                stacks[i].SetActive(true);
            }
        }
        public void ResetStacks()
        {

        }
        Vector2 joystickInput;
        [SerializeField]bool isLookingFoward = false;
        [SerializeField]bool isLookingLeft = false;
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
