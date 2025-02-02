using UnityEngine;

public class SlimeMenor : MonoBehaviour
{
    public float speed = 200.0f;
    public LayerMask camadaObstaculos;
    private Transform player;
    private Rigidbody2D rb;
    private float raioVerificacao = 0.5f;
    
    public Transform slimeBoss;
    private Rigidbody2D slimeBossRb; 

    public float suavizacaoVelocidade = 0.1f; 
    private Vector2 targetVelocity;

    private Collider2D slimeMenorCollider;
    private Collider2D slimeBossCollider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        slimeMenorCollider = GetComponent<Collider2D>();

        if (slimeBoss == null)
        {
            slimeBoss = GameObject.FindGameObjectWithTag("SlimeBoss")?.transform;
            if (slimeBoss == null)
            {
                Debug.LogWarning("SlimeBoss não encontrado na cena!");
            }
        }

        slimeBossRb = slimeBoss.GetComponent<Rigidbody2D>();
        slimeBossCollider = slimeBoss.GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (player == null || slimeBoss == null) return;

        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 direcaoParaSlimeBoss = (slimeBoss.position - transform.position).normalized;
            Vector2 direcaoComEvasao = EvitarObstaculos(direcaoParaSlimeBoss);

            targetVelocity = direcaoComEvasao * speed;
            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref targetVelocity, suavizacaoVelocidade);

            Physics2D.IgnoreCollision(slimeMenorCollider, slimeBossCollider, false);
        }
        else
        {
            Vector2 direcaoParaPlayer = (transform.position - player.position).normalized;
            Vector2 direcaoFuga = EvitarObstaculos(direcaoParaPlayer);

            targetVelocity = direcaoFuga * speed;
            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref targetVelocity, suavizacaoVelocidade);

            Physics2D.IgnoreCollision(slimeMenorCollider, slimeBossCollider, true);
        }
    }

    Vector2 EvitarObstaculos(Vector2 direcaoBase)
    {
        if (!Physics2D.Raycast(transform.position, direcaoBase, raioVerificacao, camadaObstaculos))
            return direcaoBase;

        Vector2[] direcoes = {
            Vector2.right, Vector2.left, Vector2.up, Vector2.down,
            new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized,
            new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized
        };

        foreach (Vector2 direcao in direcoes)
        {
            if (!Physics2D.Raycast(transform.position, direcao, raioVerificacao, camadaObstaculos))
                return direcao;
        }

        return direcoes[Random.Range(0, direcoes.Length)];
    }
}
