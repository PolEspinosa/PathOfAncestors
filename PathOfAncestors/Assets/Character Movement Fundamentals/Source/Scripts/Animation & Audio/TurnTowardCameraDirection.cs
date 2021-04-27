using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	//This script turns a gameobject toward the look direction of a chosen 'CameraController' component;
	public class TurnTowardCameraDirection : MonoBehaviour {

		public CameraController cameraController;
		Transform tr;

        [SerializeField]
        private SpiritsPassiveAbilities1 passiveScript;

		//Setup;
		void Start () {
			tr = transform;

			if(cameraController == null)
				Debug.LogWarning("No camera controller reference has been assigned to this script.", this);
		}
		
		//Update;
		void LateUpdate () {

			if(!cameraController)
				return;

			//Calculate up and forwward direction;
			Vector3 _forwardDirection = cameraController.GetFacingDirection();
			Vector3 _upDirection = cameraController.GetUpDirection();

            //Set rotation;
            if (!passiveScript.pushing)
            {
                tr.rotation = Quaternion.LookRotation(_forwardDirection, _upDirection);
            }
            else
            {
                gameObject.transform.LookAt(new Vector3(passiveScript.movingObject.transform.position.x, gameObject.transform.position.y, passiveScript.movingObject.transform.position.z));
            }
		}
	}
}
