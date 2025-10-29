using UnityEngine;
using UnityEngine.SceneManagement;

public  class  ButtonsInteract : MonoBehaviour
{
  public  void SwitchToPlayScene()
  {
    SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.Interface, "menuClick"), transform.position);
    SceneManager.LoadScene("Main");
  }

  public  void SwitchToMainMenu()
  {
    SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.Interface, "menuClick"), transform.position);
    SceneManager.LoadScene("TitleScreen");
  }

  public  void Quit()
  {
    SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.Interface, "menuClick"), transform.position);
    Application.Quit();
  }
}
