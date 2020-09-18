using GridManagement.Model.Dto;
using System;
using System.Collections.Generic;

namespace GridManagement.service
{
    public interface IUserService
    {
        List<UserDetails> getUser();
        UserDetails getUserById(int id);
        ResponseMessage AddUser(UserDetails userDetails);
        ResponseMessage UpdateUser(UserDetails userDetails, int id);
        ResponseMessage DeleteUser(int id);
        ResponseMessage ChangePassword(ChangePassword changePassword);
    }
}