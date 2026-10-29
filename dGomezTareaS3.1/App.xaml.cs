using Microsoft.Extensions.DependencyInjection;

namespace dGomezTareaS3._1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Views.vLogin()));
        }
    }
}