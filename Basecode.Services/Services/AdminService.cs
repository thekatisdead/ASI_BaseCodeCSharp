﻿using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class AdminService : IAdminService
    {
        public readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IdentityResult> CreateRole(String roleName)
        {
            return await _adminRepository.CreateRole(roleName);
        }
    }
}
