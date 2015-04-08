using UnityEngine;
using System.Collections;

namespace Manager {
	public static class InterfaceManager {
		
		private static InputInterpreter _inputInterp;
		public static void SetInputInterpreter(InputInterpreter inputInterpreter) {
			_inputInterp = inputInterpreter;
		}

		public static InputInterpreter GetInputInterpreter() {
			return _inputInterp;
		}

		private static HUD _hud;
		public static void SetHUD(HUD hud) {
			_hud = hud;
		}
		
		public static HUD GetHUD() {
			return _hud;
		}
		
	}
}