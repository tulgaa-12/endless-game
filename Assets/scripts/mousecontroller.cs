using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // хөдөлгөөн хурд
    public float limitX = 3f;     // тал руу хэтрэхээс сэргийлэх

    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        // Mouse байрлал дэлгэц дээр → дэлхийн координат руу хөрвүүлэх
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // зөвхөн X тэнхлэгт дагуулж хөдөлнө
        targetPos = new Vector3(worldPos.x, transform.position.y, transform.position.z);

        // X байрлалыг хязгаарлах
        targetPos.x = Mathf.Clamp(targetPos.x, -limitX, limitX);

        // зөөлөн шилжилт
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
