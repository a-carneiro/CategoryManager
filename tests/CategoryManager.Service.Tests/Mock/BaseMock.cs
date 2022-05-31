using Moq.AutoMock;

namespace CategoryManager.Service.Tests.Mock
{
    public abstract class BaseMock
    {
        protected readonly AutoMocker AutoMocker;

        protected BaseMock()
        {
            AutoMocker = new AutoMocker();

        }
    }
}
