using UnityEngine;
using System.Collections;

namespace Manager {
	public static class OrderManager {
		
		private static int nextOrderID = 0;
		
		public static int GetNextOrderID() {
			return (nextOrderID++);
		}
		
	}
}