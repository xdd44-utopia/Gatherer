using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
	//爆炸特效
	public GameObject attacker;
	public float rangeMultiplier;
	public float damageMultiplier;

	public Light spotLight;
	private float lightIntensityTar = 0;
	private float lightIntensity;
	private float lightChangeSpeed = 5f;
	private float lightIntensityLimit = 5;

	private Vector3 mousePos;
	private LineRenderer lineRenderer;
	private GameObject[] gatherable;
	private int gatherableCnt;

	private const float maxDist = 1f;
	private const float attackRange = 2f;

	private float camHeight;
	private float camWidth;

	private bool doWeHaveFrozenHere=false;

	// Start is called before the first frame update
	void Start() {
		Camera cam = Camera.main;
		camHeight = cam.orthographicSize;
		camWidth = camHeight * cam.aspect;

		lineRenderer = gameObject.AddComponent<LineRenderer>() as LineRenderer;
		lineRenderer.startWidth = 0.05f;
		lineRenderer.endWidth = 0.05f;
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.alignment = LineAlignment.TransformZ;
		lineRenderer.startColor = new Color(1f, 1f, 1f, 0.25f);
		lineRenderer.endColor = new Color(1f, 1f, 1f, 0.25f);
		lineRenderer.numCapVertices = 90;
		lineRenderer.numCornerVertices = 90;
		lineRenderer.positionCount = 1;
	}

	// Update is called once per frame
	void Update() {
		mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
		transform.position = mousePos;
		hideMouse();
		checkGatherable();
		if (Input.GetMouseButtonDown(0)) {
			gather();
		}
	}

	private void hideMouse() {
		if (transform.position.x > -camWidth &&
			transform.position.x < camWidth &&
			transform.position.y > -camHeight &&
			transform.position.y < camHeight
		) {
			Cursor.visible = false;
		}
		else {
			Cursor.visible = true;
		}
	}

	private void checkGatherable() {
		List<Vector3> vertices = new List<Vector3>();
		vertices.Add(transform.position);

		gatherable = GameObject.FindGameObjectsWithTag("Unit");
		gatherableCnt = 1;
		float cnt = 0;
		foreach (GameObject unit in gatherable) {
			int result = unit.GetComponent<UnitController>().tryGather(transform.position);
			if (result > 0) {
				gatherableCnt += 2;
				vertices.Add(unit.transform.position);
				vertices.Add(transform.position);
				cnt++;
			}
		}

		cnt = Mathf.Sqrt(cnt);
		lightIntensityTar = cnt;
		lightIntensityTar = lightIntensityTar < lightIntensityLimit ? lightIntensityTar : lightIntensityLimit;
		lightIntensity = Mathf.Lerp(lightIntensity, lightIntensityTar, Time.deltaTime * lightChangeSpeed);
		spotLight.intensity = lightIntensity;
		spotLight.gameObject.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(spotLight.gameObject.transform.localPosition.z, - lightIntensityTar / 5, Time.deltaTime * lightChangeSpeed));

		lineRenderer.positionCount = gatherableCnt;
		Vector3[] pos = new Vector3[vertices.Count];
		for (int i=0;i<vertices.Count;i++){
			pos[i] = new Vector3(vertices[i].x, vertices[i].y, 0.005f);
		}
		lineRenderer.SetPositions(pos);
	}

	private void gather() {
		GameObject[] units;
		List<GameObject> units_avi = new List<GameObject>();
		int cnt = 0;
		units = GameObject.FindGameObjectsWithTag("Unit");
	
		foreach (GameObject unit in units) {
			if(unit.GetComponent<UnitController>().isFrozenUnit==true){
				doWeHaveFrozenHere=true;
			}
			int result = unit.GetComponent<UnitController>().tryGather(transform.position);
			if (result == 2) {
				units_avi.Add(unit);
				cnt++;
			}
		}

		if (cnt > 1) {
			//GatherExplostionEffect();
			foreach (GameObject unit_avi in units_avi)
			{
				unit_avi.GetComponent<UnitController>().startGather(transform.position);
			}

			attacker.transform.position = mousePos;
			StartCoroutine(attack(cnt * rangeMultiplier, cnt * damageMultiplier, units_avi[0].GetComponent<UnitController>().getGatherTime()));
		}
	}

	public int getGatherableCount() {
		return gatherableCnt / 2;
	}

	IEnumerator attack(float r, float d, float delay) {
		yield return new WaitForSeconds(delay);
		attacker.GetComponent<AttackController>().activate(r, d,doWeHaveFrozenHere);
    }


}
