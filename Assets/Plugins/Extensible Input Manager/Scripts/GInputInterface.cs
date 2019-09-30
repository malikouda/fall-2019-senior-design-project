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

	/// interface GInputInterface this is the heart of the library. Use this interface
	///                           to install your own input handlers in the GInput object: it will work
	///                           like another standard Unity input.

	public interface GInputInterface {
		
		/// section configuration values, those functions may return if this interface implements or not the specified functions, input handler will try to lead with it as specified //////
		
		/// method implementsInputUpdate should return > 0.0f if InputUpdate method is
		///                              implemented or <= 0.0f otherwise. If the
		///                              InputUpdate is implemented, the controller
		///                              will be called periodically so unless you really
		///                              don't need to be called periodically,
		///                              YOU SHOULD NOT IMPLEMENT InputUpdate.
		float implementsInputUpdate(string inputName);

		/// method implementsInputConfig should return true if InputConfig method is
		///                              implemented or false otherwise.
		bool implementsInputConfig(string inputName);

		/// method implementsGetAxis should return true if GetAxis method is
		///                          implemented or false otherwise.
		bool implementsGetAxis(string inputName);

		/// method implementsGetAxisRaw should return true if GetAxisRaw method is
		///                             implemented or false otherwise.
		bool implementsGetAxisRaw(string inputName);

		/// method implementsGetButton should return true if GetButton method is
		///                            implemented or false otherwise.
		bool implementsGetButton(string inputName);

		/// method implementsGetButtonDown should return true if GetButtonDown method is
		///                                implemented or false otherwise.
		bool implementsGetButtonDown(string inputName);

		/// method implementsGetButtonUp should return true if GetButtonUp method is
		///                              implemented or false otherwise.
		bool implementsGetButtonUp(string inputName);

		/// section implementations, if you do not implement every function on this table or want to keep it simple, you can inherit from GInputInterfaceBase //////

		/// method InputUpdate will be called with trying to respect the returned
		///                    frequency returned by implementsInputUpdate.
		void InputUpdate(string inputName);

		/// method InputConfig will be called if implemented (that is if
		///                    implementsInputConfig returned true and when the config
		///                    string is get or set thru the GInput API.
		string InputConfig(string inputName, string newConfigString);
		
		/// method GetAxis will be called if implemented and when GInput API GetAxis or
		///                similar is called.
		float GetAxis(string inputName);
		
		/// method GetAxisRaw will be called if implemented and when GInput API GetAxisRaw
		///                   or similar is called. If GetAxis is not implemented, for
		///                   example, and it is called thru the API, this could be called
		///                   several times.
		float GetAxisRaw(string inputName);
		
		/// method GetButton will be called if implemented and when GInput API GetButton
		///                  or similar is called (if not GetButtonDown or GetButtonUp is
		///                  implemented and if they are called thru the API, this could
		///                  be called several times.
		bool GetButton(string inputName);
		
		/// method GetButtonDown will be called if implemented and when GInput API
		///                      GetButtonDown or similar is called (for example if
		///                      GetButton is not implemented, this should be called).
		bool GetButtonDown(string inputName);
		
		/// method GetButtonUp will be called if implemented and when GInput API
		///                    GetButtonUp or similar is called (for example if
		///                    GetButton is not implemented, this should be called).
		bool GetButtonUp(string inputName);

		/// section registration handlers, if you do not implement them, you can inherit from GInputInterfaceBase //////
		
		/// method RegisterInstance will be called when this object is registered
		///                         as inputName.
		bool RegisterInstance(string inputName);
		
		/// method UnregisterInstance will be called when this object is unregistered
		///                           as inputName.
		bool UnregisterInstance(string inputName);
		
	}

}

