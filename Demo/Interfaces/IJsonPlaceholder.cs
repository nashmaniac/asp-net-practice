using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Dto;

namespace Demo.Interfaces
{
    public interface IJsonPlaceholder
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
