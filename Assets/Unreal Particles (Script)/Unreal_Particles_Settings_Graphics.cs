using UnityEngine;

public class Unreal_Particles_Settings_Graphics : MonoBehaviour
{
    public enum myEnum
    {
        Minimum,
        Optimal,
        Realistic
    }
    public myEnum Unreal_Particles_Settings = myEnum.Minimum;
    public bool Emission_Active = true;

    private float start_rateOverTime;
    private int start_max_particle;
    private void Start()
    {
        if (GetComponent<ParticleSystem>() == null)
        {
            Debug.Log("Скрипт: Unreal_Particles_Settings_Graphics - не обнаружил у себя форму ParticleSystem. Пожалуйста примените форму ParticleSystem к объекту с скриптом.");
            Destroy(GetComponent<Unreal_Particles_Settings_Graphics>());
        }
        if (Emission_Active == true)
        {
            start_rateOverTime = GetComponent<ParticleSystem>().emission.rateOverTime.constant;
        }
        start_max_particle = GetComponent<ParticleSystem>().main.maxParticles;
        OnValidate();
    }
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            if (Emission_Active == true)
            {
                if (start_rateOverTime == 0 || start_max_particle == 0)
                {
                    start_rateOverTime = GetComponent<ParticleSystem>().emission.rateOverTime.constant;
                    start_max_particle = GetComponent<ParticleSystem>().main.maxParticles;
                    //Debug.Log("Скрипт: Unreal_Particles_Settings_Graphics - обнаружил у себя пустые формы start_rateOverTime и-или start_max_particle. Формы start_rateOverTime и start_max_particle автоматически заменены на предполагаемо верные значения");
                }
                else
                {
                    Update_Setting();
                }
            }
            if (Emission_Active == false)
            {
                if (start_max_particle == 0)
                {
                    start_max_particle = GetComponent<ParticleSystem>().main.maxParticles;
                    //Debug.Log("Скрипт: Unreal_Particles_Settings_Graphics - обнаружил у себя пустую форму start_max_particle. Форма start_max_particle автоматически заменена на предполагаемо верное значение");
                }
                else
                {
                    Emission_Active_Update();
                }
            }
        }
    }

    private void Update_Setting()
    {
        if (start_rateOverTime != 0 & start_max_particle != 0)
        {
            if (Unreal_Particles_Settings == myEnum.Minimum)
            {
                if (Emission_Active == true)
                {
                    var ParticleSystem_Settings_Emission = GetComponent<ParticleSystem>().emission;
                    ParticleSystem_Settings_Emission.rateOverTime = start_rateOverTime / 5F;
                }

                var ParticleSystem_Max_Particl = GetComponent<ParticleSystem>();
                ParticleSystem_Max_Particl.maxParticles = (int)(start_max_particle / 5F);
            }
            if (Unreal_Particles_Settings == myEnum.Optimal)
            {
                if (Emission_Active == true)
                {
                    var ParticleSystem_Settings_Emission = GetComponent<ParticleSystem>().emission;
                    ParticleSystem_Settings_Emission.rateOverTime = start_rateOverTime / 2F;
                }

                var ParticleSystem_Max_Particl = GetComponent<ParticleSystem>();
                ParticleSystem_Max_Particl.maxParticles = (int)(start_max_particle / 2F);
            }
            if (Unreal_Particles_Settings == myEnum.Realistic)
            {
                if (Emission_Active == true)
                {
                    var ParticleSystem_Settings_Emission = GetComponent<ParticleSystem>().emission;
                    ParticleSystem_Settings_Emission.rateOverTime = start_rateOverTime;
                }

                var ParticleSystem_Max_Particl = GetComponent<ParticleSystem>();
                ParticleSystem_Max_Particl.maxParticles = start_max_particle;
            }
        }
        else
        {
            Debug.Log("Скрипт: Unreal_Particles_Settings_Graphics - настройки метода Update_Setting не применены. Причина: пустые формы start_rateOverTime и-или start_max_particle;");
        }
    }
    private void Emission_Active_Update()
    {
            if (Unreal_Particles_Settings == myEnum.Minimum)
            {
            var start_max_particle_int = start_max_particle;
            GetComponent<ParticleSystem>().maxParticles = (int)(start_max_particle_int / 5F);
        }
            if (Unreal_Particles_Settings == myEnum.Optimal)
            {
            var start_max_particle_int = start_max_particle;
            GetComponent<ParticleSystem>().maxParticles = (int)(start_max_particle_int / 2F);
            }
            if (Unreal_Particles_Settings == myEnum.Realistic)
            {
            var start_max_particle_int = start_max_particle;
            GetComponent<ParticleSystem>().maxParticles = start_max_particle_int;
            }       
    }
}
