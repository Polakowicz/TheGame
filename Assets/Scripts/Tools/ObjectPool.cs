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

		Stack<GameObject> stack = new Stack<GameObject>();

		private void Start()
		{
			for (int i = 0; i < defaultNumerOfObjects; i++)
			{
				var obj = Instantiate(objectPrefab);
				obj.SetActive(false);
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
				return Instantiate(objectPrefab);
			}
		}

		public void DisposeObject(GameObject obj)
		{
			obj.SetActive(false);
			stack.Push(obj);
		}
	}
}