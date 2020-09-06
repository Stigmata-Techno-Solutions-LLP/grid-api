using GridManagement.Model.Dto;
using System;
using System.Collections.Generic;

namespace GridManagement.service
{
    public interface IUserService
    {
        List<UserDetails> getUser();
        ResponseMessage AddUser(UserDetails userDetails);
        ResponseMessage UpdateUser(UserDetails userDetails, int id);
        ResponseMessage DeleteUser(int id);
    }
}