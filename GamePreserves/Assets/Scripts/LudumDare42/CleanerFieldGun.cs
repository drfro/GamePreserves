using UnityEngine;

public class CleanerFieldGun : MonoBehaviour, IGun
{

    public GameObject energyField;
    public float maxScale = 3;
    public float startScale = 1;
    public float growRatePerSecond = .02f;
    private Vector3 baseScale;

    private GameObject currentFieldInstance;
    private float currentScale = 1;

    private bool fieldActive;

    public void Fire()
    {
        fieldActive = true;
        currentFieldInstance.SetActive(true);

    }

    public void StoppedFire()
    {
        fieldActive = false;
        currentFieldInstance.SetActive(false);
        currentFieldInstance.transform.localScale = baseScale;
        currentScale = startScale;
    }

    public void Start()
    {
        currentFieldInstance = Instantiate(energyField, this.transform.position, Quaternion.identity, this.transform);
        currentFieldInstance.transform.localScale = Vector3.one * startScale;
        currentFieldInstance.SetActive(false);

        currentScale = startScale;
        baseScale = currentFieldInstance.transform.localScale;
    }

    public void Update()
    {
        if (fieldActive)
        {
            if (currentScale < maxScale)
            {
                currentScale += growRatePerSecond * Time.deltaTime;
                currentFieldInstance.transform.localScale = baseScale * currentScale;
            }
        }
    }
}