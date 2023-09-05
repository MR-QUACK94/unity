using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetDummy : MonoBehaviour, IDamageable
{
    public GameObject damageNum;
    public GameObject canvas;
    public Vector2 canvasSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Damage(int damage)
    {
        Vector2 randomOff = new Vector2(Random.Range(-(canvasSize.x/2),canvasSize.x/2), Random.Range(-(canvasSize.y / 2), canvasSize.y / 2));
        Debug.Log(transform.root.localEulerAngles);
        GameObject inst = Instantiate(damageNum, canvas.transform.position, Quaternion.Euler(0, transform.parent.localEulerAngles.y, 0), canvas.transform);
        RectTransform rt = inst.GetComponent<RectTransform>();
        rt.localPosition = randomOff;
        inst.GetComponent<TMP_Text>().text = damage.ToString();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
