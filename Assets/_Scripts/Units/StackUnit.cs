//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using System;
namespace MeteTurkay{
    public abstract class StackUnit : MonoBehaviour
    {
        public bool isCollected = false;
        public Rigidbody rb;
        public Collider trigger;
        public Collider collision;
        private void OnTriggerEnter(Collider other)
        {
            if (isCollected)
                return;
            if (other.CompareTag("Player"))
            {
                StackCollected();
            }
        }

        abstract public void StackCollected();
        virtual public void InvokeContact(Action<Transform> action)
        {
            print("action called");
            action?.Invoke(transform);
        }
    }
}
