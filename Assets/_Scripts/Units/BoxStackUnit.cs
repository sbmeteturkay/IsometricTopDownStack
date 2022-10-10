//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using System;
namespace MeteTurkay{
	public class BoxStackUnit : StackUnit
	{
		public static event Action<Transform> PlayerContact;
        public override void StackCollected()
        {
            collision.enabled = false;
            isCollected = true;
            rb.isKinematic = true;
            trigger.enabled = false;
            InvokeContact(PlayerContact);
        }
    }
}
