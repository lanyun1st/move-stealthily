using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public Transform door;
    public Transform hinge;
    public Transform player;

    public Room room;

    public float interactDistance = 2f;
    public float openAngle = 90f;
    public float openSpeed = 90f;

    public GameObject interactHint;

    private bool isOpen = false;
    private float currentAngle = 0f;

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (!isOpen && dist <= interactDistance)
        {
            if (interactHint) interactHint.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                OpenDoor();
        }
        else
        {
            if (interactHint) interactHint.SetActive(false);
        }

        if (isOpen && currentAngle < openAngle)
        {
            float step = openSpeed * Time.deltaTime;
            door.RotateAround(hinge.position, Vector3.up, -step);
            currentAngle += step;
        }
    }

    void OpenDoor()
    {
        isOpen = true;

        if (room == null || room.isActivated) return;

        room.isActivated = true;

        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();

        foreach (var e in enemies)
        {
            e.Activate(true);
        }

        if (interactHint) interactHint.SetActive(false);
    }
}