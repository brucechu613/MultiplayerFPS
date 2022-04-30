
using UnityEngine.UI;
using UnityEngine;

public class RobotName : MonoBehaviour
{

    [SerializeField]
    private Text usernameText;

    [SerializeField]
    private RectTransform healthBarFill;

    [SerializeField]
    private Robot enemy;

    // Update is called once per frame
    void Update()
    {
        usernameText.text = "�Ц�C�v�үS";
        healthBarFill.localScale = new Vector3(enemy.GetHealthPct(), 1f, 1f);
    }

}
