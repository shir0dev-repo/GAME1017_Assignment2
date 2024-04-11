using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] Vector2 _parallaxEffectMultiplier;

    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;

    float textureUnitSizeX;

    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * _parallaxEffectMultiplier.x, deltaMovement.y * _parallaxEffectMultiplier.y);
        _lastCameraPosition = _cameraTransform.position;

        if (Mathf.Abs(_lastCameraPosition.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
