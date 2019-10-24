//
// Gargore INPUT MANAGER (basic edition, version 0.10)
//

//
// IMPORTANT NOTICE: THIS FILE SHOULD NOT BE EDITED, IF YOU REALLY NEED TO
//                   MODIFY IT, WOULD BE BETTER CREATE A SUBCLASS WITH EXTEND
//                   (CHECK THE MANUAL)


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.gargore.InputManager {
	
	public class GInput {

		public static Vector3 acceleration {
			get {
				return Input.acceleration;
			}
		}
		
		public static AccelerationEvent[] accelerationEvents {
			get {
				return Input.accelerationEvents;
			}
		}
		
		public static int accelerationEventCount {
			get {
				return Input.accelerationEventCount;
			}
		}
		
		public static bool anyKey {
			get {
				return Input.anyKey;
			}
		}
		
		public static bool anyKeyDown {
			get {
				return Input.anyKeyDown;
			}
		}
		
		public static Compass compass {
			get {
				return Input.compass;
			}
		}
		
		public static string compositionString {
			get {
				return Input.compositionString;
			}
		}
		
		public static DeviceOrientation deviceOrientation {
			get {
				return Input.deviceOrientation;
			}
		}
		
		public static Gyroscope gyro {
			get {
				return Input.gyro;
			}
		}
		
		public static bool imeIsSelected {
			get {
				return Input.imeIsSelected;
			}
		}
		
		public static string inputString {
			get {
				return Input.inputString;
			}
		}
		
		public static LocationService location {
			get {
				return Input.location;
			}
		}
		
		public static Vector3 mousePosition {
			get {
				return Input.mousePosition;
			}
		}
		
		public static int touchCount {
			get {
				return Input.touchCount;
			}
		}
		
		public static Touch[] touches {
			get {
				return Input.touches;
			}
		}
		
		
		#if (UNITY_2_6 || UNITY_2_6_1 || UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
		#else
		public static bool compensateSensors {
			get {
				return Input.compensateSensors;
			}
			set {
				Input.compensateSensors = value;
			}
		}
		#endif

		public static Vector2 compositionCursorPos {
			get {
				return Input.compositionCursorPos;
			}
			set {
				Input.compositionCursorPos = value;
			}
		}
		
		public static IMECompositionMode imeCompositionMode {
			get {
				return Input.imeCompositionMode;
			}
			set {
				Input.imeCompositionMode = value;
			}
		}
		
		public static bool multiTouchEnabled {
			get {
				return Input.multiTouchEnabled;
			}
			set {
				Input.multiTouchEnabled = value;
			}
		}
		
		
		public static AccelerationEvent GetAccelerationEvent(int index) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetAccelerationEvent(index);
		}
		
		public static string[] GetJoystickNames() {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetJoystickNames();
		}
		
		public static bool GetKey(KeyCode key) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetKey(key);
		}
		
		public static bool GetKey(string name) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetKey(name);
		}
		
		public static bool GetKeyDown(KeyCode key) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetKeyDown(key);
		}
		
		public static bool GetKeyDown(string name) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetKeyDown(name);
		}
		
		public static bool GetKeyUp(KeyCode key) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetKeyUp(key);
		}
		
		public static bool GetKeyUp(string name) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetKeyUp(name);
		}
		
		public static bool GetMouseButton(int button) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetMouseButton(button);
		}
		
		public static bool GetMouseButtonDown(int button) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetMouseButtonDown(button);
		}
		
		public static bool GetMouseButtonUp(int button) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetMouseButtonUp(button);
		}
		
		public static Touch GetTouch(int index) {
			processUpdateTimers(Time.realtimeSinceStartup);
			return Input.GetTouch(index);
		}
		
		public static void ResetInputAxes() {
			processUpdateTimers(Time.realtimeSinceStartup);
			Input.ResetInputAxes();
		}

		
		
		private static GInputInterface[] interfaces = null;
		private static string[] interfaceNames = null;
		private static float[] interfaceUpdateTimers = null;
		private static float[] interfaceCurrentUpdateTimers = null;
		private static GInputInterface tmp;
		
		private static void Init() {
			interfaces = new GInputInterface[10];
			interfaceNames = new string[10];
			interfaceUpdateTimers = new float[10];
			interfaceCurrentUpdateTimers = new float[10];
			for (int i = 0; i < interfaces.Length; ++i) interfaces[i] = null;
			for (int i = 0; i < interfaces.Length; ++i) interfaceNames[i] = null;
			for (int i = 0; i < interfaces.Length; ++i) interfaceUpdateTimers[i] = 0f;
			for (int i = 0; i < interfaces.Length; ++i) interfaceCurrentUpdateTimers[i] = 0f;
		}
		
		private static GInputInterface getInterfaceInstance(string interfaceName) {
			processUpdateTimers(Time.realtimeSinceStartup);
			if (interfaces == null) Init();
			for (int i = 0; i < interfaces.Length; ++i) if ((interfaces[i] != null) && interfaceName.Equals(interfaceNames[i])) return interfaces[i];
			return null;
		}
		
		public static float GetAxis(string axisName) {
			processUpdateTimers(Time.realtimeSinceStartup);
			if ((tmp = getInterfaceInstance(axisName)) != null) {
				if (tmp.implementsGetAxis(axisName)) return tmp.GetAxis(axisName); else return 0.0f;
			} else return UnityEngine.Input.GetAxis(axisName);
		}
		
		public static float GetAxisRaw(string axisName) {
			processUpdateTimers(Time.realtimeSinceStartup);
			if ((tmp = getInterfaceInstance(axisName)) != null) {
				if (tmp.implementsGetAxisRaw(axisName)) return tmp.GetAxisRaw(axisName); else return 0.0f;
			} else return UnityEngine.Input.GetAxisRaw(axisName);
		}
		
		public static bool GetButton(string buttonName) {
			processUpdateTimers(Time.realtimeSinceStartup);
			if ((tmp = getInterfaceInstance(buttonName)) != null) {
				if (tmp.implementsGetButton(buttonName)) return tmp.GetButton(buttonName); else return false;
			} else return UnityEngine.Input.GetButton(buttonName);
		}
		
		public static bool GetButtonDown(string buttonName) {
			processUpdateTimers(Time.realtimeSinceStartup);
			if ((tmp = getInterfaceInstance(buttonName)) != null) {
				if (tmp.implementsGetButtonDown(buttonName)) return tmp.GetButtonDown(buttonName); else return false;
			} else return UnityEngine.Input.GetButtonDown(buttonName);
		}
		
		public static bool GetButtonUp(string buttonName) {
			processUpdateTimers(Time.realtimeSinceStartup);
			if ((tmp = getInterfaceInstance(buttonName)) != null) {
				if (tmp.implementsGetButtonUp(buttonName)) return tmp.GetButtonUp(buttonName); else return false;
			} else return UnityEngine.Input.GetButtonUp(buttonName);
		}
		
		public static bool registerInterface(string interfaceName, GInputInterface interfaceInstance) {
			if (interfaces == null) Init();
			if ((getInterfaceInstance(interfaceName) != null) || (interfaceInstance == null) || (!interfaceInstance.RegisterInstance(interfaceName))) return false;
			for (int i = 0; i < interfaces.Length; ++i) if (interfaces[i] == null) { interfaces[i] = interfaceInstance; interfaceNames[i] = interfaceName; interfaceUpdateTimers[i] = interfaceCurrentUpdateTimers[i] = interfaceInstance.implementsInputUpdate(interfaceName); return true; }
			return false;
		}
		
		public static bool unregisterInterface(string interfaceName) {
			if (interfaces == null) Init();
			if (((tmp = getInterfaceInstance(interfaceName)) == null) || (tmp.UnregisterInstance(interfaceName))) return false;
			for (int i = 0; i < interfaces.Length; ++i) if (interfaces[i] != null) if (interfaceName.Equals(interfaceNames[i])) { interfaces[i] = null; return true; }
			return false;
		}
		
		public static string configInterface(string interfaceName, string newConfigString) {
			if (((tmp = getInterfaceInstance(interfaceName)) == null) || (!tmp.implementsInputConfig(interfaceName))) return null;
			return tmp.InputConfig(interfaceName, newConfigString);
		}
		
		public static float minTime = 1f / 50f;
		private static float lastTime = -1f;
		public static void processUpdateTimers(float currentTime) {
			if (currentTime - lastTime > minTime) {
				if (lastTime < 0f) { lastTime = currentTime; return; }
		
				for (int i = 0; i < interfaces.Length; ++i) if ((interfaces[i] != null) && (interfaceUpdateTimers[i] > 0f)) {
					if ((interfaceCurrentUpdateTimers[i] -= (currentTime - lastTime)) <= 0f) {
						interfaceCurrentUpdateTimers[i] += interfaceUpdateTimers[i];
						interfaces[i].InputUpdate(null);
					}
				}
			}
		}
	}
}


