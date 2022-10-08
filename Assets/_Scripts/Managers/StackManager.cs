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
        private void Start()
        {
            StackUnit.PlayerContact += StackUnit_PlayerContact;
            GetStacks();
        }

        private void StackUnit_PlayerContact(Transform obj)
        {
            AddNewItem(obj);
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
                        stackUnit.DOLocalMove(Vector3.zero + new Vector3(0, nextPositionY, 0), 1f);
                        stackUnit.localRotation = Quaternion.identity;
                    });
                }
             );
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
    }
}
