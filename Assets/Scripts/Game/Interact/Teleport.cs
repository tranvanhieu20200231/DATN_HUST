using UnityEngine.SceneManagement;

public class Teleport : CheckInteractive
{
    public override void InteractWithNPC()
    {
        base.InteractWithNPC();

        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}
