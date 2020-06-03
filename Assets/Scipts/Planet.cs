using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] Transform  planetShootSpot;
    [SerializeField] SpriteRenderer planetColorMark;
    private float   distanceFromSun;
    private Sprite  planetSprite;
    private float   rotateSpeed;
    private float   startAngle;
    private float   size;
    private bool    isSun;
    private bool isPlayer;

    private SpriteRenderer  spriteRenderer;
    private Combat          planetCombat;
    private GravityBody     gravityBody;
    private IPlanetInput    planetInput;

    public float    Size        => size;
    public bool     IsSun       => isSun;
    public bool     IsPlayer    => isPlayer;
    public float    RotateSpeed { get { return rotateSpeed; } set { rotateSpeed = value; } }

    private void Awake()
    {
        spriteRenderer  = GetComponent<SpriteRenderer>();
        gravityBody     = GetComponent<GravityBody>();
    }


    public void InitializePlanet(float size, float distanceFromSun, float rotateSpeed, float startAngle, Sprite planetSprite)
    {
        this.size = size;
        this.distanceFromSun    = distanceFromSun;
        this.planetSprite       = planetSprite;
        this.rotateSpeed        = rotateSpeed;
        this.startAngle         = startAngle;
        SetPlanetParametrs();
    }

    public void AddCombatController(int health, bool isPlayer, Rocket rocket)
    {
        this.isPlayer = isPlayer;
        planetCombat    = gameObject.AddComponent<Combat>();
        planetInput     = isPlayer ? new PlayerInput(transform) as IPlanetInput: new AIInput(planetCombat);
        planetCombat.InitializePlanetCombat(planetInput, health, rocket, planetShootSpot);
        //set planet mark
        planetColorMark.gameObject.SetActive(true);
        Color markColor = isPlayer ? Color.green : Color.red;
        markColor.a = 0.5f;
        planetColorMark.color = markColor;
    }

    private void SetPlanetParametrs()
    {
        isSun = distanceFromSun == 0;
        transform.localScale        = Vector3.one * size;
        transform.position          = Vector3.up * distanceFromSun;
        spriteRenderer.sprite       = planetSprite;
        gravityBody.RadiusGravityApply = size * 0.6f + size/3;
        gravityBody.IsSun = isSun;
        //start rotate
        SetAngle(startAngle);
    }

    private void Update()
    {
        if(GameManager.instance.GameInProgress && planetInput !=null)
            planetInput.UpdateInput();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        float angle = rotateSpeed * Time.deltaTime;
        SetAngle(angle);
    }

    private void SetAngle(float angle)
    {
        //rotate around sun
        if (!isSun)
            transform.RotateAround(Vector3.zero, Vector3.forward, angle);
        //rotate around self
        transform.Rotate(Vector3.forward, angle * -1);
    }


}
