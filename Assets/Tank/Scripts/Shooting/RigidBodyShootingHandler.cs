using System;
using UnityEngine;

namespace Tank.Scripts
{
	public class RigidBodyShootingHandler : MonoBehaviour, IShootingHandler
	{
		[SerializeField] private Transform muzzle;
		[SerializeField] private Transform tower;


		[Header("Bullet Info")] [Tooltip("In seconds")] [SerializeField]
		private float reloadTime = 1f;

		[SerializeField] private float shellStartVelocity = 100f;

		private new Rigidbody rigidbody;
		
		private void Awake()
		{
			rigidbody = this.GetComponent<Rigidbody>();
		}

		public void Shoot()
		{
			var shell = CreateShell();
			PrepareShell(shell);
			FireShell(shell);
		}

		private void FireShell(GameObject shell)
		{
			var shellRigidbody = shell.GetComponent<Rigidbody>();
			shellRigidbody.AddForce(shell.transform.forward * shellStartVelocity + rigidbody.velocity);
		}

		private static GameObject CreateShell()
		{
			var shell = ShellContainer.Instance.GetShell();
			return shell;
		}

		private void PrepareShell(GameObject shell)
		{
			shell.SetActive(true);
			shell.transform.position = muzzle.position;
			shell.transform.rotation = tower.rotation;
		}

		private void Reload()
		{
			throw new NotImplementedException();
		}
	}
}