using System;
using GridManagement.domain.Models;
using System.Collections.Generic;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;

namespace GridManagement.repository
{

    public class UserRepository : IUserRepository
    {
        private readonly gridManagementContext _context;
        private readonly IMapper _mapper;

        public UserRepository(gridManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UserDetails> getUser()
        {
            List<UserDetails> result = new List<UserDetails>();
            var users = _context.Users.Where(x => x.IsActive == true).ToList();
            result = _mapper.Map<List<UserDetails>>(users);
            return result;
        }

        public ResponseMessage AddUser(UserDetails userDetails)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (_context.Users.Where(x => x.Email == userDetails.email).Count() > 0)
                {
                    return responseMessage = new ResponseMessage()
                    {
                        Message = "Email Id already exist.",
                        IsValid = false
                    };
                }
                else if (_context.Users.Where(x => x.Username == userDetails.userName).Count() > 0)
                {
                    return responseMessage = new ResponseMessage()
                    {
                        Message = "UserName already exist.",
                        IsValid = false
                    };
                }
                else
                {
                    _context.Users.Add(_mapper.Map<Users>(userDetails));
                    _context.SaveChanges();
                    return responseMessage = new ResponseMessage()
                    {
                        Message = "User added successfully.",
                        IsValid = true
                    };
                }
            }
            catch (Exception ex)
            {
                return responseMessage = new ResponseMessage()
                {
                    Message = "Error in adding the user. Please contact administrator. Error : " + ex.Message,
                    IsValid = false
                };
            }
        }

        public ResponseMessage UpdateUser(UserDetails userDetails, int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                var userData = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                if (userData != null)
                {
                    if (_context.Users.Where(x => x.Email == userDetails.email && x.Id != id).Count() > 0)
                    {
                        return responseMessage = new ResponseMessage()
                        {
                            Message = "Email Id already exist.",
                            IsValid = false
                        };
                    }
                    else if (_context.Users.Where(x => x.Username == userDetails.userName && x.Id != id).Count() > 0)
                    {
                        return responseMessage = new ResponseMessage()
                        {
                            Message = "UserName already exist.",
                            IsValid = false
                        };
                    }
                    else
                    {
                        userData.FirstName = userDetails.firstName;
                        userData.LastName = userDetails.lastName;
                        userData.Email = userDetails.email;
                        userData.Phoneno = userDetails.mobileNo;
                        userData.IsActive = userDetails.isActive;
                        userData.Username = userDetails.userName;
                        userData.Password = userDetails.password;
                        userData.RoleId = userDetails.roleId;
                        userData.UpdatedBy = userDetails.updatedBy;
                        _context.SaveChanges();
                        return responseMessage = new ResponseMessage()
                        {
                            Message = "User updated successfully.",
                            IsValid = true
                        };
                    }
                }
                else
                {
                    return responseMessage = new ResponseMessage()
                    {
                        Message = "User not available.",
                        IsValid = false
                    };
                }
            }
            catch (Exception ex)
            {
                return responseMessage = new ResponseMessage()
                {
                    Message = "Error in updating the user. Please contact administrator. Error : " + ex.Message,
                    IsValid = false
                };
            }
        }

        public ResponseMessage DeleteUser(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                var user = new Users { Id = id };
                _context.Entry(user).State = EntityState.Deleted;
                _context.SaveChanges();
                return responseMessage = new ResponseMessage()
                {
                    Message = "User deleted successfully.",
                    IsValid = true
                };
            }
            catch (Exception ex)
            {
                return responseMessage = new ResponseMessage()
                {
                    Message = "Error in deleting the user. Please contact administrator. Error : " + ex.Message,
                    IsValid = false
                };
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
