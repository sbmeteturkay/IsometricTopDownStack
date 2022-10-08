//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using System;
namespace MeteTurkay{
    public class StackUnit : MonoBehaviour
    {
        bool isCollected = false;
        [SerializeField] Rigidbody rb;
        [SerializeField] Collider trigger;
        [SerializeField] Collider collision;
        public static event Action<Transform> PlayerContact;
        private void OnTriggerEnter(Collider other)
        {
            if (isCollected)
                return;
            if (other.CompareTag("Player"))
            {
                isCollected = true;
                rb.isKinematic = true;
                trigger.enabled = false;
                collision.enabled = false;
                PlayerContact?.Invoke(transform);
                //if(other.TryGetComponent(out StackManager stackManager)){
                //    stackManager.AddNewItem(transform);
                //    isCollected = true;
                //}
            }
        }
    }
}
