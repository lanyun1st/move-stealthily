using UnityEngine;

public class NextLevelPoint : MonoBehaviour
{
    public UIHintManager ui;

    public GameObject panelUI; // 选择界面
    public Transform player;
    public float distance = 3f;

    private bool inRange = false;

    void Start()
    {
        panelUI.SetActive(false);
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        inRange = dist <= distance;

        if (inRange)
        {
            ui.Show("Press E to enter next level");

            if (Input.GetKeyDown(KeyCode.E))
            {
                panelUI.SetActive(true); // 打开选择UI
            }
        }
    }
}