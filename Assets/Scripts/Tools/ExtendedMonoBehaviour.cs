using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Tools
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
       protected IEnumerator WaitAndDo(float time, Action func)
		{
            yield return new WaitForSeconds(time);
            func?.Invoke();
		}
    }
}
