
using SwapIt.Data.Entities;
using Repository.Pattern.Repositories;

namespace SwapIt.Repository.Repositories
{

    // Exmaple: How to add custom methods to a repository.
    // Here uses Interface and Class for more generic  

    public interface IUserRepository
    {
        User SignIn( string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        IRepositoryAsync<User> _userRepository;
        public UserRepository(IRepositoryAsync<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User SignIn(  string email, string password)
        {
            User? _user = _userRepository.Queryable()
                           .Where(x => x.Email.ToLower() == email.ToLower())
                           .FirstOrDefault();

            if (_user != null && _user.Password == password)
            {
                return _user;
            }
            else
            {
                return null;
            }
        }

    }



}