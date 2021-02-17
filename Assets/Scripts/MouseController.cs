using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
	//爆炸特效
	public GameObject brustFX;
	public GameObject attacker;
	public float rangeMultiplier;
	public float damageMultiplier;

	private Vector3 mousePos;
	private LineRenderer lineRenderer;
	private GameObject[] gatherable;
	private int gatherableCnt;

	private const float maxDist = 1f;
	private const float attackRange = 2f;

	private float camHeight;
	private float camWidth;

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
		foreach (GameObject unit in gatherable) {
			bool result = unit.GetComponent<UnitController>().tryGather(transform.position);
			if (result) {
				gatherableCnt += 2;
				vertices.Add(unit.transform.position);
				vertices.Add(transform.position);
			}
		}
		lineRenderer.positionCount = gatherableCnt;
		Vector3[] pos = new Vector3[vertices.Count];
		for (int i=0;i<vertices.Count;i++){
			pos[i] = new Vector3(vertices[i].x, vertices[i].y, 1f);
		}
		lineRenderer.SetPositions(pos);
	}

	private void gather() {
		GameObject[] units;
		List<GameObject> units_avi = new List<GameObject>();
		int cnt = 0;
		units = GameObject.FindGameObjectsWithTag("Unit");
	
		foreach (GameObject unit in units) {
			bool result = unit.GetComponent<UnitController>().tryGather(transform.position);
			if (result) {
				units_avi.Add(unit);
				cnt++;
			}
		}
		if (cnt > 1) {
			GatherExplostionEffect();
			foreach (GameObject unit_avi in units_avi)
			{
				unit_avi.GetComponent<UnitController>().startGather(transform.position);
			}

			attacker.transform.position = mousePos;
			StartCoroutine(attack(cnt * rangeMultiplier, cnt * damageMultiplier, units_avi[0].GetComponent<UnitController>().getGatherTime()));
		}
	}

	IEnumerator attack(float r, float d, float delay) {
		yield return new WaitForSeconds(delay);
		attacker.GetComponent<AttackController>().activate(r, d);
    }

	//封装所有爆炸后的效果
	private void GatherExplostionEffect()
    {
		//爆炸特效
		Instantiate(brustFX, mousePos, Quaternion.identity);
		FindObjectOfType<CameraShake>().Shake();
		FindObjectOfType<RipplePostProcesser>().RippleEffect();
	}

}
