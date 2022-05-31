namespace CategoryManager.Service.Tests.Mock
{
    public class ServiceMock<TService, TFixture> : BaseMock where TService : class
    {
        private readonly TService _repository;
        protected readonly TFixture Fixture;

        public TService GetService() => _repository;
        protected ServiceMock(TFixture fixture)
        {
            _repository = AutoMocker.CreateInstance<TService>();
            Fixture = fixture;
        }
    }
}