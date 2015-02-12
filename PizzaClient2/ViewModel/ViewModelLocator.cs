/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:PizzaClient.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace PizzaClient2.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // design time viewmodels
            }
            else
            {
                
            }

            SimpleIoc.Default.Register<OrderedPizzaViewModel>();
            SimpleIoc.Default.Register<SizeAndQuantityViewModel>();
            SimpleIoc.Default.Register<OrderSummaryViewModel>();
            SimpleIoc.Default.Register<PizzaViewModel>();
            SimpleIoc.Default.Register<PizzaSelectorViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel LoginView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public PizzaSelectorViewModel PizzaSelectorView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PizzaSelectorViewModel>();
            }
        }

        public PizzaViewModel PizzaView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PizzaViewModel>();
            }
        }

        public OrderSummaryViewModel OrderSummaryView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderSummaryViewModel>();
            }
        }

        public SizeAndQuantityViewModel SizeAndQuantityView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SizeAndQuantityViewModel>();
            }
        }

        public OrderedPizzaViewModel OrderedPizzaView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderedPizzaViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}