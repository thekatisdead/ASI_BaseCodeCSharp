﻿using Basecode.WebApp.Authentication;
using Basecode.Data;
using Basecode.Data.Interfaces;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;

namespace Basecode.WebApp
{
    public partial class Startup1
    {
        private void ConfigureDependencies(IServiceCollection services)
        {            
            // Common
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ClaimsProvider, ClaimsProvider>();

            // Services 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicantListService, ApplicantListService>();
            services.AddScoped<IJobOpeningService, JobOpeningService>();
            services.AddScoped<IPublicApplicationFormService, PublicApplicationFormService>();
            services.AddScoped<ICharacterReferenceService, CharacterReferenceService>();
            services.AddScoped<IInterviewerServices, InterviewerServices>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IAdminService, AdminService>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicantListRepository, ApplicantListRepository>();
            services.AddScoped<IJobOpeningRepository, JobOpeningRepository>();
            services.AddScoped<IPublicApplicationFormRepository, PublicApplicationFormRepository>();
            services.AddScoped<ICharacterReferenceRepository, CharacterReferenceRepository>();
            services.AddScoped<IInterviewerRepository, InterviewerRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IApplicantsScheduleRepo, ApplicantsScheduleRepo>();
        }
    }
}