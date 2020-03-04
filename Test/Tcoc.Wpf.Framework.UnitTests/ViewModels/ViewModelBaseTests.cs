using NUnit.Framework;
using Shouldly;
using Tcoc.Wpf.Framework.ViewModels;

namespace Tcoc.Wpf.Framework.UnitTests.ViewModels
{
    public class ViewModelBaseTests
    {
        class TestViewModel : ViewModelBase
        {
        }

        [Test]
        public void RaisePropertyChanged_Raises_PropertChangedEvent()
        {
            // Arrange
            string raisedPropName = "";
            var sut = new TestViewModel();
            sut.PropertyChanged += (s, args) => raisedPropName = args.PropertyName;
            // Act
            sut.RaisePropertyChanged("Text");
            // Assert
            raisedPropName.ShouldBe("Text");
        }
    }
}
