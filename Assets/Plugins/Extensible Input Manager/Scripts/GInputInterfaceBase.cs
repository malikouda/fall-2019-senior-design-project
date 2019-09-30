//
// Gargore INPUT MANAGER (basic edition, version 0.10)
//

//
// IMPORTANT NOTICE: THIS FILE SHOULD NOT BE EDITED, IF YOU REALLY NEED TO
//                   MODIFY IT, WOULD BE BETTER CREATE A SUBCLASS WITH EXTEND
//                   (CHECK THE MANUAL)

using UnityEngine;
using System.Collections;

namespace com.gargore.InputManager {

	public class GInputInterfaceBase: GInputInterface {
		
		public float implementsInputUpdate(string inputName) {
			return 0.0f;
		}
		
		public bool implementsInputConfig(string inputName) {
			return false;
		}
		
		public bool implementsGetAxis(string inputName) {
			return false;
		}

		public bool implementsGetAxisRaw(string inputName) {
			return false;
		}

		public bool implementsGetButton(string inputName) {
			return false;
		}

		public bool implementsGetButtonDown(string inputName) {
			return false;
		}

		public bool implementsGetButtonUp(string inputName) {
			return false;
		}


		public void InputUpdate(string inputName) {
			return;
		}

		public string InputConfig(string inputName, string newConfigString) {
			return null;
		}

		public float GetAxis(string inputName) {
			return 0.0f;
		}
		
		public float GetAxisRaw(string inputName) {
			return 0.0f;
		}

		public bool GetButton(string inputName) {
			return false;
		}
		
		public bool GetButtonDown(string inputName) {
			return false;
		}
		
		public bool GetButtonUp(string inputName) {
			return false;
		}
		
		public bool RegisterInstance(string inputName) {
			return (inputName != null);
		}

		public bool UnregisterInstance(string inputName) {
			return true;
		}
	}
}

