using UnityEngine.SceneManagement;

public class Teleport : CheckInteractive
{
    public override void Interact()
    {
        base.Interact();

        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}
