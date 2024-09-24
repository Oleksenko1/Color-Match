using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenPlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerBehaviour playerBehaviour;
    public override void InstallBindings()
    {
        Container.Bind<PlayerBehaviour>().FromInstance(playerBehaviour);
    }
}
