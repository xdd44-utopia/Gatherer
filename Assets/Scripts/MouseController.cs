using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
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
		lineRenderer.SetWidth(0.025f, 0.025f);
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
		Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
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
		int cnt = 0;
		units = GameObject.FindGameObjectsWithTag("Unit");
		foreach (GameObject unit in units) {
			bool result = unit.GetComponent<UnitController>().startGather(transform.position);
			if (result) {
				cnt++;
			}
		}
		if (cnt > 0) {
			attack();
		}
		Debug.Log(cnt + " units gathered");
	}

	private void attack() {
		GameObject[] enemies;
		int cnt = 0;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			if (Vector3.Distance(transform.position, enemy.transform.position) < attackRange) {
				Destroy(enemy, 0.3f);
			}
		}
		Debug.Log(cnt + " units gathered");
	}
}
