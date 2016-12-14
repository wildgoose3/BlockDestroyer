using UnityEngine;
using System.Collections;

using Parameter;
public class ShieldController : MonoBehaviour {
    private AudioSource ShieldAlert;

    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Collider>().enabled =false;
        ShieldAlert = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
            if (Param.ShieldTime > 0)
            {
                this.GetComponent<MeshRenderer>().enabled = true;
                this.GetComponent<Collider>().enabled = true;
            }
            else
            {
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }
            Param.ShieldTime -= Time.deltaTime;
            if (Param.ShieldTime > 3.0f)
            {
                this.GetComponent<Renderer>().material.color = new Color(0.235f, 0.5f, 0.235f, 0);
            }
            else if (Param.ShieldTime <= 3.02f&& Param.ShieldTime >= 2.98f)
            {
                ShieldAlert.PlayOneShot(ShieldAlert.clip);
            }
            else if (Param.ShieldTime > 2.0f)
            {
                this.GetComponent<Renderer>().material.color = new Color(Param.ShieldTime - 2, Param.ShieldTime - 2, 0, 0);
            }
            else if (Param.ShieldTime <= 2.02f && Param.ShieldTime >= 1.98f)
            {
                ShieldAlert.PlayOneShot(ShieldAlert.clip);
            }
            else if (Param.ShieldTime > 1.0f)
            {
                this.GetComponent<Renderer>().material.color = new Color(Param.ShieldTime - 1, Param.ShieldTime - 1, 0, 0);
            }
            else if (Param.ShieldTime <= 1.02f && Param.ShieldTime >= 0.98f)
            {
                ShieldAlert.PlayOneShot(ShieldAlert.clip);
            }
            else if (Param.ShieldTime > 0)
            {
                this.GetComponent<Renderer>().material.color = new Color(Param.ShieldTime, 0, 0, 0);
            }
        }
}
