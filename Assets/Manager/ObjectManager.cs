using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Manager {
	public static class ObjectManager {

		private static GameObjectList gameObjectList;
		public static void SetGameObjectList(GameObjectList objectList) {
			gameObjectList = objectList;
		}
		
		public static Formicant GetFormicantPrefab(string name) {
			return gameObjectList.GetFormicantPrefab(name);
		}
		
		public static WorldObject GetWorldObjectPrefab(string name) {
			return gameObjectList.GetWorldObjectPrefab(name);
		}

		public static Delegation GetDelegationPrefab() {
			return gameObjectList.GetDelegationPrefab();
		}

		public static Formicant CreateFormicant(string formicantName, Vector2 location)
		{
			return gameObjectList.CreateFormicant(formicantName, location);
		}

		public static Delegation CreateDelegation()
		{
			return gameObjectList.CreateDelegation();
		}
	}
}