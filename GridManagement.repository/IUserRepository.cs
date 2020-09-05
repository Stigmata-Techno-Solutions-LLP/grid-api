﻿using System.Collections.Generic;
using GridManagement.Model.Dto;

namespace GridManagement.repository
{
    public interface IUserRepository
    {
        List<UserDetails> getUser();
        ResponseMessage AddUser(UserDetails userDetails);
        ResponseMessage UpdateUser(UserDetails userDetails);
    }
}
