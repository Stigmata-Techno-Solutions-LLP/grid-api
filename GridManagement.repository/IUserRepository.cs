using System.Collections.Generic;
using GridManagement.Model.Dto;

namespace GridManagement.repository
{
    public interface IUserRepository
    {
        List<UserDetails> getUser();
        UserDetails getUserById(int id);
        ResponseMessage AddUser(UserDetails userDetails);
        ResponseMessage UpdateUser(UserDetails userDetails, int id);
        ResponseMessage DeleteUser(int id);
        ResponseMessage ChangePassword(ChangePassword changePassword);
    }
}
