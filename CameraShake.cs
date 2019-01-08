//Camera shake, Attcach to camera, increase shakeScale variable for shake
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Transform shakingCamera;
    
    [HideInInspector]
    public float  shakeScale = 0; 
    [Range(0, 2)]
    public float  maxOffset;
    [Range(0, 10)]
    public float  maxAngle;
    [Range(0, 10)]
    public string damageButton;

    void Start () {
        shakingCamera = this.transform;
        Mathf.Clamp(shakeScale, 0f, 1f);
	  }

    void DecreaseShakeScale()
    {
        if (shakeScale > 0)
        {
            shakeScale -= Time.deltaTime;
        }
    }
    
    void Update()
    {
        DecreaseShakeScale();
            
        if (Input.GetButtonDown(damageButton))
        {
            Damage();
        }
        TransformCamera();
        RotateCamera();
    }

    void RotateCamera()
    {  
        shakingCamera.rotation = transform.rotation * Quaternion.Euler(0,0, Angle());
        transform.rotation = shakingCamera.rotation;
    }

    void TransformCamera()
    {
        shakingCamera.position = transform.position + new Vector3(Offset(), Offset(),0);
        transform.position = shakingCamera.position;
    }
    
    float Shake(){
        return shakeScale * shakeScale;
    }  

    float GetRandomFloatNegOneToOne(){
        return Random.Range(-1f, 1f);        
    }

    float Offset(){
        return maxOffset * Shake() * GetRandomFloatNegOneToOne();
    }

    float Angle() {
        return maxAngle * Shake() * GetRandomFloatNegOneToOne();
    }

    //TODO Create Perlin noise that returns -1, 1 with seed 
    float GetPerlinNoise(float seed) {
        return Mathf.PerlinNoise(-1, 1);
    }

    //Damage increses the shakeScale by a random amount. 
    public void Damage()
    {
        if (shakeScale < 1)
        {
            shakeScale += Random.Range(0.1f, 1f);
        }
    }
}
