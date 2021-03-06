using System;
using JetBrains.Annotations;
using Tank.Scripts.Input;
using UnityEngine;

namespace Tank.Scripts
{
	public class AimHandler
	{
		[NotNull] private readonly IInputHandler input;
		private readonly Transform tankTransform;
		private readonly float towerRotationSpeed;
		private readonly Transform towerTransform;

		public AimHandler(TankMovementController tankMovementController)
		{
			input = tankMovementController.InputHandler;
			towerTransform = tankMovementController.TowerTransform;
			tankTransform = tankMovementController.transform;
			towerRotationSpeed = tankMovementController.TowerRotationSpeed;
		}

		public void HandleAim()
		{
			if (input.AimVector == Vector2.zero) return;

			var angle = Vector2ToAngle(input.AimVector);

			var newRotation = towerTransform.localEulerAngles;
			newRotation.y = Mathf.LerpAngle(newRotation.y, angle - tankTransform.eulerAngles.y + 90,
				towerRotationSpeed * Time.deltaTime);

			towerTransform.localEulerAngles = newRotation;
		}

		private static float Vector2ToAngle(Vector2 vector2)
		{
			float angle;
			if (vector2.y >= 0)
				angle = (float) (-Mathf.Rad2Deg * Math.Acos(Vector2.Dot(vector2, Vector2.right) / vector2.magnitude));
			else
				angle = (float) (Mathf.Rad2Deg * Math.Acos(Vector2.Dot(vector2, Vector2.right) / vector2.magnitude));
			return angle;
		}
	}
}