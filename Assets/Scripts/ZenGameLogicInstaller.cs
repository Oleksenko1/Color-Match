using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenGameLogicInstaller : MonoInstaller
{
    [SerializeField] private UITimer uiTimer;
    [SerializeField] private UIScore uiScore;
    public override void InstallBindings()
    {
        Container.Bind<UITimer>().FromInstance(uiTimer);
        Container.Bind<UIScore>().FromInstance(uiScore);
    }
}
