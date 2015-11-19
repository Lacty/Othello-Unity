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
  
  private Player _player = Player.Black;
  
  void Start() {
    _othello = GameObject.Find("OthelloSelect").GetComponent<OthelloSelect>();
    CreateStone(4, 4, Player.Black);
    CreateStone(3, 3, Player.Black);
    CreateStone(3, 4, Player.White);
    CreateStone(4, 3, Player.White);
  }
  
  void Update() {
    if (!Input.GetKeyDown(KeyCode.Return)) return;
    
    if (_othello.getSelectedCell().GetComponentInChildren<Stone>()) return;
    
    CreateStone(_othello.SelectX, _othello.SelectZ, _player);
    
    _player = _player == Player.Black
              ? Player.White
              : Player.Black;
  }
  
  void CreateStone(int x, int z, Player player) {
    var stone = Instantiate(Stone);
    stone.GetComponent<Stone>().setPlayerType(player);
    stone.name = string.Format("Stone({0},{1})", x, z);
    stone.transform.SetParent(GameObject.Find("OthelloTable/Cell(" + x + "," + z + ")").transform);
    stone.transform.localPosition = Vector3.up;
    stone.transform.localRotation = Quaternion.AngleAxis(player == Player.Black ? 180f : 0f, Vector3.left);
  }
}