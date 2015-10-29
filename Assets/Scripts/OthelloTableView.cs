using UnityEngine;
using System.Collections;

public class OthelloTableView : MonoBehaviour {
  
  [SerializeField]
  private GameObject _cell = null;
  public GameObject Cell{
    get
    {
      if (_cell != null) { return _cell; }
      _cell = Resources.Load<GameObject>("Cell/Cell");
      return _cell;
    }
  }
  
	void Start () {
    int width = 8;
    int depth = 8;
    float offset = 1.01f;
    var table = GameObject.Find("OthelloTable");
    for (int d = -depth / 2; d < depth; ++d) {
      for (int w = -width / 2; w < width; ++w) {
	      var cell = Instantiate(Cell);
        cell.name = Cell.name + "(" + d + "," + w + ")";
        cell.transform.parent = table.transform;
        cell.transform.localPosition = new Vector3(w * offset,
                                                   0.0f,
                                                   d * offset);
      }
    }
	}
}
