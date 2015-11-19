using UnityEngine;
using System.Collections;


public class Stone : MonoBehaviour {
  
  private Player _player;
  
  public bool isBlack() {
    return _player == Player.Black;
  }
  public bool isWhite() {
    return _player == Player.White;
  }
  
  public void setPlayerType(Player type) {
    _player = type;
  }
  
  public void reverse() {
    _player = isBlack() ? Player.White : Player.Black;
    transform.Rotate(180, 0, 0);
  }
}
