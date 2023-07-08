using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //our enum for the box type/index
    private enum BoxType
    {
        BoxOne,
        BoxTwo,
        BoxThree
    
    }

    [SerializeField] BoxType boxType;

    [SerializeField] float ParticleLifeTime = 10;

    //our VFX prefab
    [SerializeField] GameObject boxParticle;
    [SerializeField] GameObject boxCanvasObj;

    private void Start()
    {
        if (boxParticle)
            boxParticle.SetActive(false);

        if (boxCanvasObj)
            boxCanvasObj.SetActive(false);


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " hit our box trigger of type : " + boxType);

        if (other.CompareTag("GameSphere"))
        {
            OnGameSphereHit(other.gameObject);
        }
    }

    private void OnGameSphereHit(GameObject gameSphere)
    {
        //show our canvas and play our vfx.
        if (boxCanvasObj)
        boxCanvasObj.SetActive(true);

        if (boxParticle)
            boxParticle.SetActive(true);

        if (AudioManager.AM_instance != null)
        {
            AudioManager.AM_instance.Play("BoxHit", 1);
        }

        StartCoroutine(ToggleParticleEffects());

        GameSphere tempSphere = gameSphere.transform.GetComponent<GameSphere>();

        switch (boxType)
        {
            case BoxType.BoxOne:

                if ((int)tempSphere.sphereType == (int)boxType + 1) //are the colours matching ?
                    GameManager.Instance.AddScore(10);
                else
                    GameManager.Instance.AddScore(5);
            break;

            case BoxType.BoxTwo:

                if ((int)tempSphere.sphereType == (int)boxType + 1) //are the colours matching ?
                    GameManager.Instance.AddScore(10);
                else
                    GameManager.Instance.AddScore(5);

            break;

            case BoxType.BoxThree:

                if ((int)tempSphere.sphereType == (int)boxType + 1) //are the colours matching ?
                    GameManager.Instance.AddScore(10);
                else
                    GameManager.Instance.AddScore(5);

                GameManager.Instance.ResetLevel();
                //Destroy(gameSphere);
                break;
        }
    }


    IEnumerator ToggleParticleEffects()
    {
        yield return new WaitForSeconds(ParticleLifeTime);

        boxParticle.SetActive(false);
    }
}
