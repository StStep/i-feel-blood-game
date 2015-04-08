using UnityEngine;
using System.Collections;

namespace Manager {
	public static class NetworkManager {

		private static int nextLocID = 0;

		public static int GetNextLocID() {
			return (nextLocID++);
		}

	}
}