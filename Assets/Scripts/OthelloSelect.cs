using UnityEngine;
using System.Collections.Generic;

public class OthelloSelect : MonoBehaviour {
  
  private int WIDTH;
  private int DEPTH;
  
  public OthelloStone _player = null;
  
  [SerializeField]
  Material _selected = null;
  public Material SelectedMaterial
  {
    get {
      if (_selected != null) { return _selected; }
      _selected = Resources.Load<Material>("Cell/Selected");
      return _selected;
    }
  }

  [SerializeField]
  Material _normal = null;
  public Material NormalMaterial
  {
    get {
      if (_normal != null) { return _normal; }
      _normal = Resources.Load<Material>("Cell/Normal");
      return _normal;
    }
  }
  
  [SerializeField]
  Material _placeable = null;
  public Material PlaceableMaterial
  {
    get {
      if (_placeable != null) { return _placeable; }
      _placeable = Resources.Load<Material>("Cell/Placeable");
      return _placeable;
    }
  }
  
  private int _selectX = 0;
  public int SelectX {
    get { return _selectX; }
  }
  private int _selectZ = 0;
  public int SelectZ {
    get { return _selectZ; }
  }

  private void ChangeColor() {
    var cell = GameObject.Find("OthelloTable/Cell(" + _selectX + "," + _selectZ + ")");
    cell.GetComponent<Renderer>().material = SelectedMaterial;
  }
  
  private void BackColor() {
    var cell = GameObject.Find("OthelloTable/Cell(" + _selectX + "," + _selectZ + ")");
    cell.GetComponent<Renderer>().material = NormalMaterial;
  }
  
  private void ClearColor() {
    for (int x = 0; x < WIDTH; x++) {
      for (int z = 0; z < DEPTH; z++) {
        var cell = GameObject.Find("OthelloTable/Cell(" + x + "," + z + ")");
        cell.GetComponent<Renderer>().material = NormalMaterial;
      }
    }
  }

	void Start () {
    _player = GameObject.Find("OthelloStone").GetComponent<OthelloStone>();
		var table = GameObject.Find("OthelloTable");
    WIDTH = table.GetComponent<OthelloTableView>().GetWidth();
    DEPTH = table.GetComponent<OthelloTableView>().GetDepth();
    
    ChangeColor();
	}

	void Update () {
    Focus();
    Select();
    ChangeColor();
	}
  
  void Select() {
    bool keyW = Input.GetKeyDown(KeyCode.W);
    bool keyA = Input.GetKeyDown(KeyCode.A);
    bool keyS = Input.GetKeyDown(KeyCode.S);
    bool keyD = Input.GetKeyDown(KeyCode.D);
    
    if (!(keyW || keyA || keyS || keyD)) return;
    
    if (keyW && (_selectX < DEPTH - 1) && (_selectX >= 0)) {
      BackColor();
      _selectX++;
      ChangeColor();
    }
    if (keyA && (_selectZ <= WIDTH - 1) && (_selectZ > 0)) {
      BackColor();
      _selectZ--;
      ChangeColor();
    }
    if (keyS && (_selectX <= DEPTH - 1) && (_selectX > 0)) {
      BackColor();
      _selectX--;
      ChangeColor();
    }
    if (keyD && (_selectZ < WIDTH - 1) && (_selectZ >= 0)) {
      BackColor();
      _selectZ++;
      ChangeColor();
    }
  }
  
  void Focus() {
    ClearColor();
    for (int x = 0; x < WIDTH; x++) {
      for (int z = 0; z < DEPTH; z++) {
        // 石があれば処理を飛ばす
        if (GetStone(x, z)) continue;
        
        // 隣接しているCellを取得
        var adjacent = GetAdjacent(x, z);
        
        foreach (var index in adjacent) {
          // 配列外なら処理を飛ばす
          var xOver = index.Key < 0 || index.Key >= WIDTH;
          var zOver = index.Value < 0 || index.Value >= DEPTH;
          if (xOver || zOver) continue;
          
          // 石の情報を取得
          var findStone = GetStone(index.Key, index.Value);
          // 石がなければ処理を飛ばす
          if (findStone == null) continue;
          // playerの色と同じならば処理を飛ばす
          if (findStone.GetComponent<Stone>().getColor() == _player.GetCurrentPlayer()) continue;
          
          // 違う石を見つけたら方向を取得
          var directionX = index.Key - x;
          var directionZ = index.Value - z;
          
          // その方向のみ探索
          for (int count = 0; count < WIDTH; count++) {
            var dirX = index.Key + directionX * count;
            var dirZ = index.Value + directionZ * count;
            
            // 配列外なら処理を終了
            if (dirX < 0 || dirX >= WIDTH || dirZ < 0 || dirZ >= DEPTH) break;
            
            // 石が見つからなければ終了
            var stone = GetStone(dirX, dirZ);
            if (stone == null) break;
            
            // 違う石なら次へ
            if (stone.GetComponent<Stone>().getColor() != _player.GetCurrentPlayer()) continue;
            
            // 同じ石ならCellの色を変更
            var cell = GameObject.Find("OthelloTable/Cell(" + x + "," + z + ")");
            cell.GetComponent<Renderer>().material = PlaceableMaterial;
          }
        }
      }
    }
  }
  
  private List<KeyValuePair<int, int>> GetAdjacent(int x, int z) {
    var list = new List<KeyValuePair<int, int>>();
    int xp = x + 1;
    int xm = x - 1;
    int zp = z + 1;
    int zm = z - 1;
    list.Add(new KeyValuePair<int, int>(xp, zp));
    list.Add(new KeyValuePair<int, int>(xp, zm));
    list.Add(new KeyValuePair<int, int>(xm, zp));
    list.Add(new KeyValuePair<int, int>(xm, zm));
    list.Add(new KeyValuePair<int, int>(x, zp));
    list.Add(new KeyValuePair<int, int>(x, zm));
    list.Add(new KeyValuePair<int, int>(xp, z));
    list.Add(new KeyValuePair<int, int>(xm, z));
    return list;
  }
  
  private GameObject GetCell(int x, int z) {
    return GameObject.Find("OthelloTable/Cell(" + x + "," + z + ")");
  }
  
  private GameObject GetStone(int x, int z) {
    return GameObject.Find("OthelloTable/Cell(" + x + "," + z + ")/Stone(" + x + "," + z + ")");
  }
  
  public GameObject GetSelectedCell() {
    return GameObject.Find("OthelloTable/Cell(" + _selectX + "," + _selectZ + ")");
  }
}
