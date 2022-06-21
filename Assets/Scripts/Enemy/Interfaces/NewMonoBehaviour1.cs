using System.Collections;
using UnityEngine;

namespace Interfaces
{
	public interface IKick
	{
		public void Kick(Vector2 direction);
		public void Kick(Vector2 direction, float force);
	}
}