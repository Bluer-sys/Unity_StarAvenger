namespace DefaultNamespace.Installers
{
    using Zenject;

    public class LevelSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // EnemySpawner
            Container
                .BindInterfacesTo<EnemySpawner>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            
        }
    }
}