using UnityEngine;
using System.Collections;

public class OthelloStone : MonoBehaviour {
  
  private int WIDTH;
  private int DEPTH;
  
  [SerializeField]
  private GameObject _stone = null;
  public GameObject Stone{
    get
    {
      if (_stone != null) { return _stone; }
      _stone = Resources.Load<GameObject>("Stone/Stone");
      return _stone;
    }
  }
  
  private OthelloSelect _othello = null;
  
  private bool _isBlack = true;
  
  void Start() {
    _othello = GameObject.Find("OthelloSelect").GetComponent<OthelloSelect>();
  }
  
  void Update() {
    if (!Input.GetKeyDown(KeyCode.Return)) return;
    
    if (_othello.getSelectedCell().GetComponentInChildren<Stone>()) return;
    
    CreateStone();
    
    _isBlack = !_isBlack;
  }
  
  void CreateStone() {
    var stone = Instantiate(Stone);
    stone.name = string.Format("Stone({0},{1})", _othello.SelectX, _othello.SelectZ);
    stone.transform.SetParent(_othello.getSelectedCell().transform);
    stone.transform.localPosition = Vector3.up;
    stone.transform.localRotation = Quaternion.AngleAxis(_isBlack ? 180f : 0f, Vector3.left);
  }
}