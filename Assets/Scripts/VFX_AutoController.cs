using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{


    [SerializeField] bool autoDestroy = true;
    [SerializeField] float destroyDealy = 1;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("random VFX offsets")]
    [SerializeField] private float minXoffset = -0.3f;
    [SerializeField] private float maxXoffset = 0.3f;
    [SerializeField] private float minYoffset = -0.3f;
    [SerializeField] private float maxYoffset = 0.3f;

    private void Start() {
        if (autoDestroy) Destroy(gameObject, destroyDealy);
        ApplyRandomRotation();
        ApplyRandomHitLocationEffect(); 
        }


    private void ApplyRandomHitLocationEffect()
    {
        if (randomOffset == false) return;
        float xOffest = Random.Range(minXoffset, maxXoffset);
        float yOffest = Random.Range(minYoffset, maxYoffset);
        transform.position = transform.position + new Vector3(xOffest, yOffest);

    }
    private void ApplyRandomRotation()
    {
        if (randomRotation == false) return;
        float Zrotation = Random.Range(0, 360);
        transform.Rotate(0, 0, Zrotation);
    }
        
  
}
