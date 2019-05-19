using BusinessLogic;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace UnitTests.BusinessLogic
{
    public class UserTests
    {
        readonly Mock<IUser> _userRepository;
        readonly IManageUser _manageUser;
        public UserTests()
        {
            _userRepository = new Mock<IUser>();
            _manageUser = new ManageUser(_userRepository.Object);
        }

        [Fact]
        public async Task VerifyGetAllUsers()
        {
            await _manageUser.GetAllUsers();

            _userRepository.Verify(r => r.GetAllUsers(), Times.Once);
        }

        [Fact]
        public async Task VerifyGetUser()
        {         
           await _manageUser.GetUser(12);

            _userRepository.Verify(r => r.GetUser(12), Times.Once);
        }

        [Fact]
        public async Task VerifyInsertUserFunction()
        {
            var userDetail = TestData.GetUsers().Where(x => x.UserId == 12).FirstOrDefault();

            var result = await _manageUser.InsertUser(userDetail);

            _userRepository.Verify(r => r.InsertUser(userDetail), Times.Once);
        }

        [Fact]
        public async Task VerifyEditUserFunction()
        {
            var userDetail = TestData.GetUsers().Where(x => x.UserId == 34456).FirstOrDefault();

            var result = await _manageUser.EditUser(userDetail);

            _userRepository.Verify(r => r.EditUser(userDetail), Times.Once);
        }

        [Fact]
        public async Task VerifyDeleteUserFunction()
        {
            var userDetail = TestData.GetUsers().Where(x => x.UserId == 33).FirstOrDefault();

            var result = await _manageUser.DeleteUser(userDetail);

            _userRepository.Verify(r => r.DeleteUser(userDetail), Times.Once);
        }       
    }
}
