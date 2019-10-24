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

	public class GInputInterfaceExample: GInputInterfaceBase, GInputInterface {
		
		private string configString = "";
		private string source = "Fire1";
		private bool debug = false;

		new public bool implementsInputConfig(string inputName) {
			return true;
		}

		new public string InputConfig(string inputName, string newConfigString) {
			if (newConfigString != null) {
				source = GInputHelper.configGetValueString(newConfigString, "source", source);
				string debugString = GInputHelper.configGetValueString(newConfigString, "debug", debug ? "true" : "false");
				if (debugString.ToLower().Equals("true")) debug = true;
				if (debugString.ToLower().Equals("false")) debug = false;
				configString = newConfigString;
			}
			return configString;
		}

		new public bool implementsGetAxis(string inputName) {
			return true;
		}
		
		new public float GetAxis(string inputName) {
			float retv = UnityEngine.Input.GetAxis(source);
			if (debug) Debug.Log("GInputInterfaceAxis: " + inputName + ": received and bypass value " + retv);
			return retv;
		}

		new public bool implementsGetButton(string inputName) {
			return true;
		}
		
		new public bool GetButton(string inputName) {
			bool retv = UnityEngine.Input.GetButton(source);
			if (debug) Debug.Log("GInputInterfaceButton: " + inputName + ": received and bypass value " + retv);
			return retv;
		}
	}
}

