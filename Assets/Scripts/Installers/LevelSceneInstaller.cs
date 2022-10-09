namespace DefaultNamespace.Installers
{
    using UI;
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
            
            // UiView
            Container
                .BindInterfacesTo<UiView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}