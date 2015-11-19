using UnityEngine;
using System.Collections;


public class Stone : MonoBehaviour {
  
  private Player _color;
  
  public bool isBlack() {
    return _color == Player.Black;
  }
  public bool isWhite() {
    return _color == Player.White;
  }
  
  public void setPlayerType(Player type) {
    _color = type;
  }
  
  public void reverse() {
    _color = isBlack() ? Player.White : Player.Black;
    transform.Rotate(180, 0, 0);
  }
  
  public Player getColor() {
    return _color;
  }
}
