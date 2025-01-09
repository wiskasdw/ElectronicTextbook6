﻿using ElectronicTextbook.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicTextbook.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
        Task<User> GetByEmailAsync(string email);
    }
}