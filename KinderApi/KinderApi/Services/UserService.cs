using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.helper;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KinderApi.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public UserService(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PagedList<User>> GetAllUsers(PaginationParams userParams)
        {
            var allUsers = context.Users;

            return await PagedList<User>.CreateAsync(allUsers, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<List<User>> GetUsersForMatchByDistance(int currentUserId, int? distance)
        {
            if (distance == null)
                distance = 500000;

            List<User> allUsers = await context.Users.ToListAsync();

            List<User> userToReturn = new List<User>();

            User currentUser = await GetUserById(currentUserId);


            foreach (User user in allUsers)
            {
                if (user.Id == currentUser.Id)
                    continue;

                UsersCoordinates coords = UsersCoordinates.Parse(currentUser.Coordinate, user.Coordinate);
                if(coords == null)
                    continue;

                if (coords.Distance < distance &&
                    Math.Abs(currentUser.DateOfBith.GetYears() - user.DateOfBith.GetYears()) <= 5)
                    userToReturn.Add(user);

                if (userToReturn.Count >= 10)
                    break;
            }

            return userToReturn;
        }



        public async Task<List<User>> GetUsersForMathcByPreference(int currentUserId,PreferenceDto prefernces = null)
        {
            User currentUser = await GetUserById(currentUserId);
            if(DateTime.Now.SubstrHourse(currentUser.LastSeen) < 24)
                return new List<User>();
            
            List<User> allUsers = await context.Users.Include(u => u.Preferences).Include(p => p.Images).ToListAsync();

            List<User> userToReturn = new List<User>();


            PreferenceDto currentUserPrefs = null;
            if (prefernces == null)
                currentUserPrefs = mapper.Map<PreferenceDto>(currentUser.Preferences.First());
            else
                currentUserPrefs = prefernces;

            int notNullPrefs = currentUserPrefs.CountNotNullField();

            foreach (User user in allUsers)
            {
                if (user.Id == currentUser.Id)
                    continue;

                if(context.Likes.Any(l => l.SenderId == currentUserId && l.ReceiverId == user.Id))
                    continue;

                PreferenceDto userPrefs = mapper.Map<PreferenceDto>(user.Preferences.First());
                int difference = Math.Abs(userPrefs - currentUserPrefs);

                if (difference <= notNullPrefs &&
                    Math.Abs(currentUser.DateOfBith.GetYears() - user.DateOfBith.GetYears()) <= 5)
                    userToReturn.Add(user);

                if (userToReturn.Count >= 10)
                    break;
            }

            return userToReturn;
        }


        public async Task<Image> GetMainPhotoByUser(int userId)
        {
            var photoQuery = context.Image.AsQueryable();
            var photo = await photoQuery.FirstOrDefaultAsync(p => p.IsMain && p.Userid == userId);

            return photo;
        }

        public async Task<User> GetUserById(int userId)
        {
            var query = context.Users.Include(u => u.Images).Include(u => u.Preferences).AsQueryable();
            var user = await query.FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<Image> GetUserPhotoByPhotoId(int userId, int photoId)
        {
            var photoQuery = context.Image.AsQueryable();
            var photo = await photoQuery.FirstOrDefaultAsync(p => p.id == photoId && p.Userid == userId);

            return photo;
        }

        public async Task<List<User>> SortUsersByData(UserToFindDto sortUserData)
        {
            List<User> uesrToReturn = new List<User>();

            List<PropertyInfo> sortedParams = sortUserData.GetType().GetProperties().ToList();

            foreach (var prop in sortedParams)
            {
                string propName = prop.Name;

                if(prop.GetValue(sortUserData) == null)
                    continue;

                string propVal = prop.GetValue(sortUserData).ToString().ToLower();

                if (uesrToReturn.Count() == 0)
                {
                    await context.Users.ForEachAsync(u => 
                    {
                        if(u.GetType().GetProperty(propName).GetValue(u).ToString().ToLower().Contains(propVal)){
                            uesrToReturn.Add(u);
                        }
                    });
                }
                else
                {
                    List<User> tempUsers = new List<User>();
                    uesrToReturn.ForEach(u => 
                    {
                        if(u.GetType().GetProperty(propName).GetValue(u).ToString().ToLower().Contains(propVal)){
                            tempUsers.Add(u);
                        }
                    });

                    uesrToReturn = tempUsers;
                }
            }

            return uesrToReturn;
        }
    }
}