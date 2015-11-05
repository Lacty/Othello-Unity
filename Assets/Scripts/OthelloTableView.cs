using UnityEngine;
using System.Collections;

public class OthelloTableView : MonoBehaviour {
  
  private int WIDTH = 8;
  private int DEPTH = 8;
  
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
    float offset = 1.01f;
    var table = GameObject.Find("OthelloTable");
    for (int d = 0; d < DEPTH; ++d) {
      for (int w = 0; w < WIDTH; ++w) {
	      var cell = Instantiate(Cell);
        cell.name = Cell.name + "(" + d + "," + w + ")";
        cell.transform.parent = table.transform;
        cell.transform.localPosition = new Vector3(w * offset,
                                                   0.0f,
                                                   d * offset);
      }
    }
  }
  
  public int GetWidth() {
    return WIDTH;
  }
  
  public int GetDepth() {
    return DEPTH;
  }
}
