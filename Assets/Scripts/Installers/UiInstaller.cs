namespace DefaultNamespace.Installers
{
    using Zenject;

    public class UiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // BuffsViewer
            Container
                .BindInterfacesTo<BuffsViewer>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}