using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    //public EnemyManager enemyManager;
    public override void InstallBindings()
    {
        Container.Bind<EnemyManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MissionManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KilledEnemyTypes>().FromComponentInHierarchy().AsSingle();
    }
}
