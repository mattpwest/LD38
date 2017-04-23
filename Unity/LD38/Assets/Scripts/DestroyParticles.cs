using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    public Sprite sprite;
    public Color color;
    public int Value;
    public float LifeTime = 0.6f;

    private SpriteRenderer spriteRenderer;
    private float lifeTimeSpent = 0.0f;

	void Start ()
	{
	    this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = this.sprite;

        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.0f), 3.0f);
	}
	
	void Update ()
	{
	    this.lifeTimeSpent += Time.deltaTime;

	    if(this.lifeTimeSpent > this.LifeTime)
	    {
	        Destroy(this.gameObject);
	    }

	    this.spriteRenderer.color = new Color32(255, 255, 255,
            (byte)(255 - Mathf.RoundToInt(255 * this.lifeTimeSpent / this.LifeTime)));
	}
}
