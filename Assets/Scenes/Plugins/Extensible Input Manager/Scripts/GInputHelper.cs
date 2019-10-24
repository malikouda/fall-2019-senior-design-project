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

	public class GInputHelper {
		
		public static string configGetValueString(string configString, string valueName, string defaultValue) {
			if (configString == null) return defaultValue;
			if (valueName == null) return defaultValue;
			configString = ";" + configString + ";";
			int spos = configString.IndexOf(";" + valueName + "=");
			if (spos < 0) return defaultValue;
			spos = spos + valueName.Length + 2;
			int epos = configString.IndexOf(";", spos);
			if (epos < 0) return defaultValue;
			return configString.Substring(spos, epos - spos);
		}
	}
}


