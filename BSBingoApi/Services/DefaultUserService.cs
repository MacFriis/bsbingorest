﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BSBingoApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BSBingoApi.Services
{
    public class DefaultUserService : IUsersService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultUserService(
            UserManager<UserEntity> userManager,
            IConfigurationProvider mappingConfiguration
        )
        {
            _userManager = userManager;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm form)
        {
            var entity = new UserEntity
            {
                Email = form.Email,
                UserName = form.Email,
                FirstName = form.FirstName,
                LastName = form.LastName,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var result = await _userManager.CreateAsync(entity, form.Password);
            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault()?.Description;
                return (false, firstError);
            }

            return (true, null);
        }

        public async Task<PagedResults<User>> GetUsersAsync(PagingOptions pagingOptions, SortOptions<User, UserEntity> sortOptions, SearchOptions<User, UserEntity> searchOptions)
        {
            IQueryable<UserEntity> query = _userManager.Users;
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);


            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<User>(_mappingConfiguration)
                .ToArrayAsync();


            return new PagedResults<User>
            {
                Items = items,
                TotalSize = size

            };
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal user)
        {
            var entity = await _userManager.GetUserAsync(user);
            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<User>(entity);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var entity = await _userManager.Users
                                           .SingleOrDefaultAsync(x => x.Id == userId);
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<User>(entity);
        }

        public async Task<Guid?> GetUserIdAsync(ClaimsPrincipal principal)
        {
            var entity = await _userManager.GetUserAsync(principal);
            if (entity == null) return null;

            return entity.Id;
        }
    }
}
