using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPhase1 : MonoBehaviour
{
    public static BossAttackPhase1 instance;

    PlayerHandling player;
    public GameObject[] missile;
    public bool[] isMissileExploded /*= new bool[3]*/; //BOOLEANO CHE MI DICE SE MISSILE Iesimo E' ESPLOSO
    public ParticleSystem explosion;
    public Vector3[] offsetBetweenMissiles; //new Vector3(-10, 10, 0); // (L'OFFSET VIENE INSERITO DALL'EDITOR E DOVREBBE ESSERE DIVERSO TRA GLI ALTRI, VA MODIFICATO LA X E LA Z CON LA Y UGUALE PER TUTTI MINIMO 10(O COMUNQUE ABBASTANZA IN ALTO RISPETTO AL PLAYER)
    Vector3 direction;  //DIREZIONDE DEL PLAYER QUANDO INIZIALIZZO OP DI MISSILI

    public Boss data; //NECESSARIO PER POTER VEDERE QUANDO CADE
    public float currentTimeBeforeFalling = 0;
    public float calloutTime;
    public float speed = 2f;
    //private Vector3 positionToAttack;
    private bool isAttacking; //VERO SE STO FACENDO CADERE I MISSILI

    public SphereCollider bossExplosionCollider;
    public SpriteRenderer bossTargetRenderer;
    

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        for (int i = 0; i < missile.Length; i++)
        { //INIZIALIZZO TUTTI I MISSILI A ESPLOSI
            {
                Debug.Log("setting missiles");
                isMissileExploded[i] = true;

            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandling>(); //RECUPERO PLAYER A INIZIO PARTITA
        isAttacking = false;

        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {//SE I MISSILI NON STANNO CADENDO ALLORA ATTIVA TIMER
            if (currentTimeBeforeFalling >= data.maxTimeBeforeStomp)
            {//SE IL TIMER HA SUPERATO IL CAP ALLORA COMPI LE OPERAZIONI PER INIT LA CADUTA DEI MISSILI
                initMissileAttack();
            }
            else
            {
                currentTimeBeforeFalling += Time.deltaTime;
                if (currentTimeBeforeFalling + calloutTime >= data.maxTimeBeforeStomp)
                { //SE IL TIMER CHE AVVISA CHE LA BOMBA STA ARRIVANDO FINISCE ALLORA VAI QUA
                    //-------------->SE FUNZIONA, METTERE LE OPERAZIONI CHE DICONO CHE STA PER ARRIVARE LA BOMBA<------------------------ 
                    if (bossTargetRenderer.enabled) { return; } 
                    else {
                        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.4f, player.transform.position.z);
                        bossTargetRenderer.enabled = true; }
                    //Debug.Log("Se vedi questa scritta vuol dire che fra " + calloutTime + " arrivano i missili");
                }
            }
        }
        else
        {
            MissileAttack(); //SE I MISSILI STANNO CADENDO, CONTINUA LE OPERAZIONI A RIGUARDO
        }
    }
    private void initMissileAttack()
    {
        isAttacking = true;//AVVISO CHE STO ATTACCANDO CON I MISSILI
        direction = player.transform.position; //DO' LA DIREZIONE DEL PLAYER
        for (int i = 0; i < missile.Length; i++)//PER OGNI MISSILE
        {
            isMissileExploded[i] = false; //NON SONO ANCORA CADUTI/ESPLOSI
            missile[i].transform.position = direction + offsetBetweenMissiles[i]; //GLI DO' COME POSIZIONE INIZIALE DEL MISSILE LA POSIZIONE DEL PLAYER + OFFSET TRA' PLAYER E MISSILI)
            missile[i].SetActive(true); //ATTIVO IL MODELLO
            missile[i].transform.position = speed * Time.deltaTime * Vector3.MoveTowards(missile[i].transform.position, direction + offsetBetweenMissiles[i], 30f); //GLI DO' MOVIMENTO INIZIALE == A QUELLO PER UPDATE(AKA MISSILE UPDATE)

        }
    }

    private void MissileAttack()
    {
        Debug.Log("DroppingNukes");
        if (!allMissileUp() && !bossExplosionCollider.enabled)
        {// SE IL 1* MISSILE è CADUTO (VA' AGGIUNTO IL CONTROLLO CHE IL TRIGGER NON SIA GIA' ATTIVO)
            //--------------->QUI VA' MESSO L'ATTIVAZIONE DEL TRIGGER CHE AMMAZZA IL PLAYER<------------------
            bossExplosionCollider.enabled = true;
            Debug.Log("AAAAAAAAAA");
        }
        for (int i = 0; i < missile.Length; i++)
        {// PER OGNI MISSILE
            if (!isMissileExploded[i])
            { // SE MISSILE E' ANCORA ATTIVO
                if (missile[i].transform.position.y <= 0.5f)
                { // SE MISSILE TOCCA TERRA PROCEDI CON LE OPERAZIONI DI ESPLOSIONE
                    ParticleSystem ps = Instantiate(explosion, transform.position, Quaternion.identity);
                    ps.Emit(10);
                    Destroy(ps.gameObject, 2);
                    missile[i].SetActive(false);
                    //missile[i].transform.position = positionToAttack;
                    isMissileExploded[i] = true;// LA DISATTIVO DALL'ARRAY BOOLEANO
                }
                else
                {
                    missile[i].transform.position = speed * Time.deltaTime * Vector3.MoveTowards(missile[i].transform.position, direction + offsetBetweenMissiles[i], 30f); // ALTRIMENTI PROCEDI PER RAGGIUNGERE L'OBIETTIVO SALVATO
                }
            }
        }
        CheckMissileOnGround();//UNA VOLTA FATTO CONTROLLO SE SONO TUTTI A TERRA
    }



    private void CheckMissileOnGround() // SERVE PER FAR PARTIRE L'INSIEME DI OPERAZIONI NECESSARIE PER POTER CHIUDERE IL DROP DELLE BOMBE
    {
        if (allMissileDown())
        { //SE SONO ESPLOSI TUTTI I MISSILI, FERMO L'ATTACCO PRESENTE IN UPDATE E RESETTO I VALORI
            //------->QUI VA MESSO LA DISATTIVAZIONE DEL TRIGGER CHE AMMAZZA IL PLAYER<----------
            bossExplosionCollider.enabled = false;
            bossTargetRenderer.enabled = false;

            isAttacking = false;
            direction = Vector3.zero;
            currentTimeBeforeFalling = 0;
        }
    }

    private bool allMissileDown()
    { //Controlla se tutti i missili sono esplosi
        foreach (bool i in isMissileExploded)
        {
            if (!i)
            {
                return false;
            }
        }
        return true;
    }

    private bool allMissileUp()
    { //Controlla se tutti i missili non hanno ancora toccato terreno
        foreach (bool i in isMissileExploded)
        {
            if (i)
            {
                return false;
            }
        }
        return true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            player.GetComponent<PlayerHandling>().Squashed();
        }
    }
}
