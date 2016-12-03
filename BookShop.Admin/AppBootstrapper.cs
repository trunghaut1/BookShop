namespace BookShop.Admin
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using ViewModels;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<MainViewModel>();
            container.PerRequest<UserViewModel>();
            container.PerRequest<CatViewModel>();
            container.PerRequest<SubCatViewModel>();
            container.PerRequest<BookViewModel>();
            container.PerRequest<OrderViewModel>();
            container.PerRequest<RecommendViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}