using UnityEngine;
using UnityEngine.SceneManagement;

public  class  ButtonsInteract : MonoBehaviour
{
  public static void SwitchToPlayScene()
  {
    SceneManager.LoadScene("Main");
  }

  public static void SwitchToMainMenu()
  {
    SceneManager.LoadScene("TitleScreen");
  }
}
