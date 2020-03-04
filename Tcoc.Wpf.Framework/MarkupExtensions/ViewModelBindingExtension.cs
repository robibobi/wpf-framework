using System;
using System.Reflection;
using System.Windows.Markup;
using Tcoc.Wpf.Framework.Util;

namespace Tcoc.Wpf.Framework.MarkupExtensions
{
    public class ViewModelBindingExtension : MarkupExtension
    {
        private Func<Type, object> _serviceProdiver;

        public Type ViewModelType { get; set; }

        public ViewModelBindingExtension()
        {
            _serviceProdiver = GetInstanceUsingCommonServiceLocator;
        }

        public override object ProvideValue(IServiceProvider _)
        {
            return ProvideValue(DesignTimeUtils.IsRunningInDesigner);
        }

        // Test seam
        public object ProvideValue(bool isRunningInDesigner)
        {
            if (isRunningInDesigner)
                return CreateDesignTimeInstance();
            else
                return CreateRuntimeInstance();
        }

        // Test seam
        public void SetServiceProvider(Func<Type, object> provider)
        {
            _serviceProdiver = provider;
        }

        private object CreateRuntimeInstance()
        {
            return _serviceProdiver.Invoke(ViewModelType);
        }

        private object CreateDesignTimeInstance()
        {
            return GetDefaultCtor(ViewModelType)?.Invoke(null);
        }

        private static ConstructorInfo GetDefaultCtor(Type viewModelType)
        {
            return viewModelType.GetConstructor(new Type[0]);
        }

        private static object GetInstanceUsingCommonServiceLocator(Type vmType)
        {
            return CommonServiceLocator.ServiceLocator.Current.GetInstance(vmType);
        }
    }
}
