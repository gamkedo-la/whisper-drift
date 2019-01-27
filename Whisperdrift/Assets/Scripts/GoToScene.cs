using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
	Main,
}

public class GoToScene : MonoBehaviour
{
	[SerializeField] private Scenes goToThisScene = Scenes.Main;

	public void ChangeScene( )
	{
		SceneManager.LoadScene( goToThisScene.ToString( ), LoadSceneMode.Single );
	}
}
