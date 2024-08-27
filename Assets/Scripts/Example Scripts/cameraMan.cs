
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraMan : MonoBehaviour
{
    public Camera cameraMansCam;
    public Image[] imageDisplays;
    public float[] timeToTakePic = new float[3]; // Times to take pictures
    public float zOffsetToPlayer;
    public float xOffsetToPlayer;
    public float yOffsetToPlayer;
    private GameObject smallCar;
    private GameObject mediumCar;
    private GameObject largeCar;
    public Transform playersPosition;
    private Texture2D[] imageTextures;
    private int currentImageIndex = 0;
    private bool pictureTaken = false;
    public GameObject cameraLightGreen;
    public GameObject cameraLightRed;


    private void Awake()
    {
        smallCar = GameObject.FindWithTag("Small Car");
        mediumCar = GameObject.FindWithTag("Medium Car");
        largeCar = GameObject.FindWithTag("Large Car");
    }



    private void Start()
    {
     
        cameraLightGreen.SetActive(false);
        cameraLightRed.SetActive(true);

        zOffsetToPlayer = Random.Range(-10, 20);
        xOffsetToPlayer = Random.Range(-10, -20);
        yOffsetToPlayer = Random.Range(10, 20);

        

        if (smallCar != null)
        {
            playersPosition = smallCar.transform;
        }

        if (mediumCar != null)
        {
            playersPosition = mediumCar.transform;
        }

        if (largeCar != null)
        {
            playersPosition = largeCar.transform;
        }

        // Disable cameraman's camera
        cameraMansCam.enabled = false;

        imageTextures = new Texture2D[timeToTakePic.Length];
        StartCoroutine(SnapshotTiming());
        
    }



    private void Update()//update for cameraman movement
    {
        this.gameObject.transform.position = new Vector3(playersPosition.position.x + xOffsetToPlayer, playersPosition.position.y + yOffsetToPlayer, playersPosition.position.z + zOffsetToPlayer);
        this.gameObject.transform.LookAt(playersPosition.transform.position);//always faces the players position

        if (pictureTaken)
        {
            
            MoveCameraMan();//if picture taken move the cameraman to new location
            
        }

    }


    void MoveCameraMan()
    {
        
        //Debug.Log("function called for movement");
      
        zOffsetToPlayer = Random.Range(-10, 20);
        xOffsetToPlayer = Random.Range(-10, -20);
        yOffsetToPlayer = Random.Range(10, 20);
        pictureTaken = false;
        //Debug.Log("stop moving camera");
        
    }


    IEnumerator SnapshotTiming()
    {
        while (currentImageIndex < timeToTakePic.Length)
        {
            

            yield return new WaitForSeconds(timeToTakePic[currentImageIndex]);
            
            // Take a picture
            TakeSnapShot(currentImageIndex);
            
            DisplayImage(currentImageIndex);

            currentImageIndex++;

        }
        this.gameObject.SetActive(false);
        //Debug.Log("cameraman false now");
    }
 
    IEnumerator LightTimer()
    {
        cameraLightRed.SetActive(false);
        cameraLightGreen.SetActive(true);
        yield return new WaitForSeconds(1);
        cameraLightGreen.SetActive(false);
        cameraLightRed.SetActive(true);
    }

    void TakeSnapShot(int index)
    {
        // Calculate camera position
        Vector3 cameraPosition = playersPosition.position + new Vector3(xOffsetToPlayer, yOffsetToPlayer, zOffsetToPlayer);
        cameraMansCam.transform.position = cameraPosition;
        cameraMansCam.transform.LookAt(playersPosition.position);
        StartCoroutine(LightTimer());

        cameraMansCam.enabled = true;

        // Capture the image
        RenderTexture rt = new RenderTexture(720, 720, 24);
        cameraMansCam.targetTexture = rt;
        cameraMansCam.Render();
        RenderTexture.active = rt;

        // Create the texture
        imageTextures[index] = new Texture2D(720, 720, TextureFormat.RGB24, false);
        imageTextures[index].ReadPixels(new Rect(0, 0, 720, 720), 0, 0);
        imageTextures[index].Apply();

       
        cameraMansCam.enabled = false;
        cameraMansCam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        pictureTaken = true;
    }




    void DisplayImage(int index)
    {
        // Display the captured image on the canvas
        Sprite sprite = Sprite.Create(imageTextures[index], new Rect(0, 0, 720, 720), Vector2.zero);
        imageDisplays[index].sprite = sprite;
    }
}
