using Application.UseCases;
using Moq;
using System;

namespace Test.UseCaseMocks
{
    /// <summary>
    /// This class is used to mock the behavior of our UseCase
    /// We can implement methods to mock different behaviors
    /// </summary>
    /// <returns></returns>
    public class GetAccountInfoMock
    {
        protected readonly Mock<IGetAccountInfo> mock = new Mock<IGetAccountInfo>();

        public static GetAccountInfoMock Instance()
        {
            return new GetAccountInfoMock();
        }

        public Mock<IGetAccountInfo> Mock()
        {
            return mock;
        }
                
        public GetAccountInfoMock GetSuccessImplementation(double balanceToReturn = 500)
        {
            mock.Setup(x => x.Execute(It.IsAny<int>())).Returns(new AccountDto
            {
                Balance = balanceToReturn
            });

            return this;
        }

        public GetAccountInfoMock GetNotFoundImplementation()
        {
            mock.Setup(x => x.Execute(It.IsAny<int>())).Returns(new AccountDto
            {
                Balance = null
            });

            return this;
        }

        public GetAccountInfoMock GetBadRequestImplementation()
        {
            mock.Setup(x => x.Execute(It.IsAny<int>())).Throws(new Exception("Mock message"));

            return this;
        }
    }
}
