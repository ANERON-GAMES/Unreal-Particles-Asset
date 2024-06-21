using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Unreal_Particles_Light_URP_Animation : MonoBehaviour
{
    private Light2D Light2D_Controle;

    public bool Active_anim_Light2D = true;
    public bool Loop_anim_Light2D = true;
    public float Speed_anim_Light2D = 1;
    public float Light2D_intensity_MIN = 0;
    public float Light2D_intensity_MAX = 1;

    private float intensity_Light2D;
    private bool controle_loop;

    public bool Active_Random_Speed_anim_Light2D = false;
    public bool Auto_Radius_anim = false;
    private float Start_Radius;
    private bool controle_Start_Radius;
    public float Smoothing_Auto_Radius_anim = 2F;
    public Vector2 Random_Speed_anim_Light2D = new Vector2(0F, 1F);
    public int update_tik_Random_anim_Light2D = 5;
    private float random_controle;
    private float controle_start_Speed_anim_Light2D;
    private bool controle_update_const_controle_start_Speed_anim_Light2D;
    private int start_update_tik_Random_anim_Light2D;


    private void Start()
    {
        if (GetComponent<Light2D>() == null)
        {
            Debug.Log("Скрипт: Unreal_Particles_Light_URP_Animation - не обнаружил у себя форму Light2D. Пожалуйста примените форму Light2D к объекту пред использованием скрипта.");
            Destroy(GetComponent<Unreal_Particles_Light_URP_Animation>());
        }
        else
        {
            Light2D_Controle = GetComponent<Light2D>();
            controle_start_Speed_anim_Light2D = Speed_anim_Light2D;
            start_update_tik_Random_anim_Light2D = update_tik_Random_anim_Light2D;
            Start_Radius = Light2D_Controle.pointLightOuterRadius;
        }
    }

    private void Update()
    {
        if (Active_anim_Light2D == true)
        {
            if (Active_Random_Speed_anim_Light2D == true)
            {
                if (update_tik_Random_anim_Light2D > 0)
                {
                    update_tik_Random_anim_Light2D -= 1;
                }
                else
                {
                    random_controle = Random.Range(Random_Speed_anim_Light2D.x, Random_Speed_anim_Light2D.y);
                    Speed_anim_Light2D = random_controle;
                    update_tik_Random_anim_Light2D = start_update_tik_Random_anim_Light2D;
                }
                if (controle_update_const_controle_start_Speed_anim_Light2D == false)
                {
                    controle_update_const_controle_start_Speed_anim_Light2D = true;
                }
            }
            if (Active_Random_Speed_anim_Light2D == false)
            {
                if (controle_update_const_controle_start_Speed_anim_Light2D == true)
                {
                    Speed_anim_Light2D = controle_start_Speed_anim_Light2D;
                    update_tik_Random_anim_Light2D = start_update_tik_Random_anim_Light2D;
                    controle_update_const_controle_start_Speed_anim_Light2D = false;
                }
            }
            if (Auto_Radius_anim == true)
            {
                Light2D_Controle.pointLightOuterRadius = (intensity_Light2D * 10F) / Smoothing_Auto_Radius_anim;
                if (controle_Start_Radius == false)
                {
                    controle_Start_Radius = true;
                }
            }
            if (Auto_Radius_anim == false)
            {
                if (controle_Start_Radius == true)
                {
                    Light2D_Controle.pointLightOuterRadius = Start_Radius;
                    controle_Start_Radius = false;
                }
            }
            if (Loop_anim_Light2D == true)
            {
                if (intensity_Light2D > Light2D_intensity_MAX)
                {
                    controle_loop = false;
                }
                if (intensity_Light2D < Light2D_intensity_MIN)
                {
                    controle_loop = true;
                }
                if (controle_loop == false)
                {
                    intensity_Light2D -= Speed_anim_Light2D * Time.deltaTime;
                }
                else
                {
                    intensity_Light2D += Speed_anim_Light2D * Time.deltaTime;
                }
            }
            if (Loop_anim_Light2D == false)
            {
                if (intensity_Light2D > Light2D_intensity_MAX)
                {
                    intensity_Light2D = Light2D_intensity_MIN;
                }
                if(intensity_Light2D < Light2D_intensity_MIN)
                {
                    intensity_Light2D = Light2D_intensity_MAX;
                }
                intensity_Light2D += Speed_anim_Light2D * Time.deltaTime;
            }
            Light2D_Controle.intensity = intensity_Light2D;
        }
    }
}
