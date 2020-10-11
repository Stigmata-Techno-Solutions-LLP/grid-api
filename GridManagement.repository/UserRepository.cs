using System;
using GridManagement.domain.Models;
using System.Collections.Generic;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using GridManagement.common;

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
            var users = _context.Users.Where(x => x.IsDelete == false).ToList();
            result = _mapper.Map<List<UserDetails>>(users);
            return result;
        }

        public UserDetails getUserById(int id)
        {
            UserDetails user = new UserDetails();
            var userDetail = _context.Users.Where(x => x.Id == id && x.IsDelete == false).FirstOrDefault();
            if(userDetail != null)
                user = _mapper.Map<UserDetails>(userDetail);
            else
                user = null;
            return user;
        }

        public ResponseMessage AddUser(UserDetails userDetails)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (_context.Users.Where(x => x.Email == userDetails.email && x.IsDelete == false).Count() > 0)
                {
                    throw new ValueNotFoundException("Email Id already exist.");                      
                }
                else if (_context.Users.Where(x => x.Username == userDetails.userName && x.IsDelete == false).Count() > 0)
                {
                  throw new ValueNotFoundException("UserName already exist.");                 
                }
                else
                {
                    _context.Users.Add(_mapper.Map<Users>(userDetails));
                    _context.SaveChanges();
                    return responseMessage = new ResponseMessage()
                    {
                        Message = "User added successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseMessage UpdateUser(UserDetails userDetails, int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                var userData = _context.Users.Where(x => x.Id == id  && x.IsDelete ==false).FirstOrDefault();
                if (userData != null)
                {
                    if (_context.Users.Where(x => x.Email == userDetails.email && x.Id != id && x.IsDelete == false).Count() > 0)
                    {
                         throw new ValueNotFoundException("Email Id already exist.");    
                       
                    }
                    else if (_context.Users.Where(x => x.Username == userDetails.userName && x.Id != id && x.IsDelete == false).Count() > 0)
                    {
                         throw new ValueNotFoundException("UserName already exist.");                        
                    }
                    else
                    {
                        userData.FirstName = userDetails.firstName;
                        userData.LastName = userDetails.lastName;
                        userData.Email = userDetails.email;
                        userData.Phoneno = userDetails.mobileNo;
                        userData.IsActive = userDetails.isActive;
                        userData.Username = userDetails.userName;
                   
                        userData.RoleId = userDetails.roleId;
                        userData.UpdatedBy = userDetails.updatedBy;
                        _context.SaveChanges();
                        return responseMessage = new ResponseMessage()
                        {
                            Message = "User updated successfully.",
                           
                        };
                    }
                }
                else
                {
                    throw new ValueNotFoundException("User not available.");  
                }
            }
            catch (Exception ex)
            {
             throw ex;
            }
        }

        public ResponseMessage DeleteUser(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                
                var userData = _context.Users.Where(x => x.Id == id  && x.IsDelete ==false).FirstOrDefault();
                if (userData == null) throw new ValueNotFoundException("User Id doesnt exist."); 
                userData.IsDelete = true;
                _context.SaveChanges();
                return responseMessage = new ResponseMessage()
                {
                    Message = "User deleted successfully."                  
                };
            }
            catch (Exception ex)
            {
              throw ex;
            }
        }

        public ResponseMessage ChangePassword(ChangePassword changePassword)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                var userData = _context.Users.Where(x => x.Id == changePassword.userId).FirstOrDefault();
                if (userData != null)
                {
                    if (_context.Users.Where(x => x.Password == changePassword.currentPassword && x.Id == changePassword.userId).Count() == 0)
                    {

                        throw new ValueNotFoundException("Current Password does not match.");    
                    }
                    else
                    {
                        userData.Password = changePassword.newPassword;
                        _context.SaveChanges();
                        return responseMessage = new ResponseMessage()
                        {
                            Message = "Password changed successfully."
                        };
                    }
                }
                else
                {
                     throw new ValueNotFoundException("User not available.");    
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
