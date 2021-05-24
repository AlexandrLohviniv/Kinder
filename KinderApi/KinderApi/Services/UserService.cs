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
                if (coords.Distance < distance &&
                    Math.Abs(currentUser.DateOfBith.GetYears() - user.DateOfBith.GetYears()) <= 5)
                    userToReturn.Add(user);

                if (userToReturn.Count >= 10)
                    break;
            }

            return userToReturn;
        }

        

        public async Task<List<User>> GetUsersForMathcByPreference(int currentUserId, PreferenceDto prefernces = null)
        {
            List<User> allUsers = await context.Users.Include(u => u.Preferences).ToListAsync();

            List<User> userToReturn = new List<User>();

            User currentUser = await GetUserById(currentUserId);

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
    }
}