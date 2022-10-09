using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Tools
{
	public class ObjectPool : MonoBehaviour
	{
		// How many objects create at start
		[SerializeField] private int defaultNumerOfObjects;
		[SerializeField] private GameObject objectPrefab;
		[SerializeField] private Transform objectsParent;

		Stack<GameObject> stack = new Stack<GameObject>();

		private void Start()
		{
			for (int i = 0; i < defaultNumerOfObjects; i++)
			{
				var obj = Instantiate(objectPrefab, objectsParent);
				obj.SetActive(false);
				stack.Push(obj);
			}
		}

		public GameObject GetObject()
		{
			if (stack.Count > 0)
			{
				var obj = stack.Pop();
				obj.SetActive(true);
				return obj;
			} else
			{
				return Instantiate(objectPrefab, objectsParent);
			}
		}

		public void DisposeObject(GameObject obj)
		{
			obj.SetActive(false);
			stack.Push(obj);
		}
	}
}