using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerBody;
    public float inicialSpeed = 5f;
    public float speed = 5f;

    private bool atacando = false;
    private Vector2 direction;

    public LayerMask slimeLayer;
    public MenuController menuController;
    public Transform slimeBoss;

    private bool estaMorto = false;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        
        if (slimeBoss == null)
        {
            slimeBoss = GameObject.FindGameObjectWithTag("SlimeBoss")?.transform;
            if (slimeBoss == null)
            {
                Debug.LogWarning("SlimeBoss não encontrado na cena!");
            }
        }
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerBody.MovePosition(playerBody.position + direction.normalized * speed * Time.fixedDeltaTime);

        if(!estaMorto){
            playerAnimator.SetInteger("Movimento", direction.sqrMagnitude > 0 ? 1 : 0);
        }else{
            Morrer();
        }

        OrientacaoPlayer();

        PlayerAtacando();
        if (atacando)
        {
            playerAnimator.SetInteger("Movimento", 2);
        }
    }

    void OrientacaoPlayer()
    {
        if (direction.x != 0)
        {
            transform.eulerAngles = new Vector2(0f, direction.x > 0 ? 0f : 180f);
        }
    }

    void PlayerAtacando()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            atacando = true;
            speed = 3f;

            TryThrowSlime();
        }
        else
        {
            atacando = false;
            speed = inicialSpeed;
        }
    }

    void TryThrowSlime()
    {
        Collider2D[] slimes = Physics2D.OverlapCircleAll(transform.position, 3.5f, slimeLayer);

        if (slimes.Length > 0)
        {
            GameObject slimeParaArremessar = slimes[0].gameObject;
            SlimeMenor slimeScript = slimeParaArremessar.GetComponent<SlimeMenor>();

            if (slimeScript != null)
            {
                slimeScript.enabled = false;

                Vector2 direcaoArremesso = (slimeBoss.position - slimeParaArremessar.transform.position).normalized;
                
                Rigidbody2D slimeRb = slimeParaArremessar.GetComponent<Rigidbody2D>();
                if (slimeRb != null)
                {
                    slimeRb.AddForce(direcaoArremesso * 200f, ForceMode2D.Impulse);
                }

                StartCoroutine(ReactivateSlimeAI(slimeScript, slimeParaArremessar));
            }
        }
    }

    IEnumerator ReactivateSlimeAI(SlimeMenor slimeScript, GameObject slime)
    {
        yield return new WaitForSeconds(1f); 
        slimeScript.enabled = true; 
        slime.transform.SetParent(null); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!estaMorto){

    if (other.CompareTag("Cercado")||other.CompareTag("SlimeBoss")) 
    {
        Debug.Log("O Player foi atingido pelo SlimeMenor!"); 

        estaMorto = true; 
        Morrer();
    }
        }
    }  

    void Morrer()
{
    GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; 
    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 

    Animator anim = GetComponent<Animator>();
    if (anim != null)
    {
        anim.SetInteger("Movimento", 3); 
    }

    StartCoroutine(AguardeEAbraMenu());
    }

    IEnumerator AguardeEAbraMenu()
    {
        yield return new WaitForSeconds(3f); 

        SceneManager.LoadScene("Menu"); 
    } 
}
