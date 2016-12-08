using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour {

	public AudioClip beep;
	private TangoPointCloud tengoPointCloud;
	private TangoDeltaPoseController tangoDeltaPoseController;

	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource>();
		tengoPointCloud = gameObject.GetComponent<TangoPointCloud>();
		tangoDeltaPoseController = GameObject.Find("Tango Delta Camera").GetComponent<TangoDeltaPoseController>();
	}
	
	// Update is called once per frame


	void Update (){
		
		int count = 0;
		for (int i = 0; i < tengoPointCloud.m_pointsCount; i++) {
			
			if (Distance(tengoPointCloud.m_points[i]) < 1.25) {
				count += 1;
			}
		}


		if (count > 30) {
			Vector3 closest = tengoPointCloud.m_points [0];
					for (int i = 1; i < tengoPointCloud.m_pointsCount; i++) {
						if (Distance (tengoPointCloud.m_points [i]) < Distance (closest)) {
							closest = tengoPointCloud.m_points [i];
						}

					}
			AudioSource.PlayClipAtPoint (beep, closest);
		}
	}

	Vector3 RelativePosition(Vector3 vec){

		Vector3 newVector3 = new Vector3();
		newVector3.Set (
			vec.x - tangoDeltaPoseController.m_tangoPosition.x,
			vec.y - tangoDeltaPoseController.m_tangoPosition.y,
			vec.z - tangoDeltaPoseController.m_tangoPosition.z);
		return newVector3;
	}

	float Distance(Vector3 vec){
		Vector3 temp = RelativePosition (vec);
		return Mathf.Pow( Mathf.Pow (temp.x, 2) + Mathf.Pow (temp.y, 2) + Mathf.Pow (temp.z, 2), 0.5f); 
	}


}
