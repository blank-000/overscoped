    // this was written by CodeWithKyrian in the unity forums. 
    // A great deal of thanks goes out to him. 
    // he wrote this august 17 2017.
    // are here we are april 18 2024. 
    // as far as I know unity still has no official support for this
    

    
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;
   
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemActions : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] UnityEvent _particleWasBorn = new UnityEvent();
        [SerializeField] UnityEvent _particleDead = new UnityEvent();
   
        private ParticleSystem _particleSystem;
   
        private int _uniqueID;
        private List<float> _currentParticlesIds = new List<float>();
        private List<Vector4> _customData = new List<Vector4>();
   
        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
   
            if (_particleSystem == null)
            {
                Debug.LogError("Missing particle system!", this);
            }
        }
   
        private void LateUpdate()
        {
            if (_particleSystem == null)
            {
                return;
            }
       
            UpdateLifeEvents();
        }
   
        void UpdateLifeEvents()
        {
            _particleSystem.GetCustomParticleData(_customData, ParticleSystemCustomData.Custom1);
   
            for (var i = 0; i < _particleSystem.particleCount; i++)
            {
                if (_customData[i].x != 0.0f)
                {
                    continue;
                }
           
                _customData[i] = new Vector4(++_uniqueID, 0, 0, 0);
           
                _particleWasBorn?.Invoke();
                _currentParticlesIds.Add(_customData[i].x);
   
                if (_uniqueID > _particleSystem.main.maxParticles)
                    _uniqueID = 0;
            }
   
            var ids = _customData.Select(d => d.x).ToList();
            var difference = _currentParticlesIds.Except(ids).Count();
   
            for (int i = 0; i < difference; i++)
            {
                _particleDead?.Invoke();
            }
   
            _currentParticlesIds = ids;
            _particleSystem.SetCustomParticleData(_customData, ParticleSystemCustomData.Custom1);
        }
    }
   
 