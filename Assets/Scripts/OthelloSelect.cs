using UnityEngine;
using System.Collections;

public class OthelloSelect : MonoBehaviour {
  
  private int WIDTH;
  private int DEPTH;
  
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
  
  private void ChangeColor() {
    var cell = GameObject.Find("OthelloTable/Cell(" + _selectX + "," + _selectZ + ")");
    cell.GetComponent<Renderer>().material = SelectedMaterial;
  }
  
  private void backColor() {
    var cell = GameObject.Find("OthelloTable/Cell(" + _selectX + "," + _selectZ + ")");
    cell.GetComponent<Renderer>().material = NormalMaterial;
  }
  
  private int _selectX = 0;
  private int _selectZ = 0;

	void Start () {
		var table = GameObject.Find("OthelloTable");
    WIDTH = table.GetComponent<OthelloTableView>().GetWidth();
    DEPTH = table.GetComponent<OthelloTableView>().GetDepth();
    
    ChangeColor();
	}

	void Update () {
    bool keyW = Input.GetKeyDown(KeyCode.W);
    bool keyA = Input.GetKeyDown(KeyCode.A);
    bool keyS = Input.GetKeyDown(KeyCode.S);
    bool keyD = Input.GetKeyDown(KeyCode.D);
    
    if (!(keyW || keyA || keyS || keyD)) return;
    
    if (keyW && (_selectX < DEPTH - 1) && (_selectX >= 0)) {
      backColor();
      _selectX++;
      ChangeColor();
    }
    if (keyA && (_selectZ <= WIDTH - 1) && (_selectZ > 0)) {
      backColor();
      _selectZ--;
      ChangeColor();
    }
    if (keyS && (_selectX <= DEPTH - 1) && (_selectX > 0)) {
      backColor();
      _selectX--;
      ChangeColor();
    }
    if (keyD && (_selectZ < WIDTH - 1) && (_selectZ >= 0)) {
      backColor();
      _selectZ++;
      ChangeColor();
    }
	}
}
