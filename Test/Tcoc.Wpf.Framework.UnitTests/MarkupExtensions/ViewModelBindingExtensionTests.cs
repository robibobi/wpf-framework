using NUnit.Framework;
using Shouldly;
using Tcoc.Wpf.Framework.MarkupExtensions;

namespace Tcoc.Wpf.Framework.UnitTests.MarkupExtensions
{
    class SomeViewModel
    {
        public string UsedCtor { get; }
        
        public SomeViewModel()
        {
            UsedCtor = "Default";
        }

        public SomeViewModel(string usedCtor)
        {
            UsedCtor = usedCtor;
        }
    }

    class VmWithoutDefaultCtor
    {
        public VmWithoutDefaultCtor(object service)
        {
        }
    }

    public class ViewModelBindingExtensionTests
    {
        [Test]
        public void ProvideValue_InDesigner_UsesDefaultCtor()
        {
            // Arrange
            var sut = CreateSut<SomeViewModel>();
            // Act
            var instance = sut.ProvideValue(isRunningInDesigner: true) as SomeViewModel;
            // Assert
            instance.UsedCtor.ShouldBe("Default");
        }

        [Test]
        public void ProvideValue_InDesigner_NoDefaultCtor_ReturnsNull()
        {
            // Arrange
            var sut = CreateSut<VmWithoutDefaultCtor>();
            // Act
            var instance = sut.ProvideValue(isRunningInDesigner: true);
            // Assert
            instance.ShouldBeNull();
        }

        [Test]
        public void ProvideValue_AtRuntime_ServiceProviderIsUsed()
        {
            // Arrange
            var sut = CreateSut<SomeViewModel>();
            sut.SetServiceProvider(type => new SomeViewModel("parametrized"));
            // Act
            var instance = sut.ProvideValue(isRunningInDesigner: false) as SomeViewModel;
            // Assert
            instance.UsedCtor.ShouldBe("parametrized");
        }

        private ViewModelBindingExtension CreateSut<T>()
        {
            var sut = new ViewModelBindingExtension();
            sut.ViewModelType = typeof(T);
            return sut;
        }
    }
}
