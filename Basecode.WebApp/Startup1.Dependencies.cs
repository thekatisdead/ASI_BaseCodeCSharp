using Basecode.WebApp.Authentication;
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
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<IApplicantListService, ApplicantListService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IJobOpeningService, JobOpeningService>();
            services.AddScoped<IUserViewService, UserViewService>();
            services.AddScoped<IPublicApplicationFormService, PublicApplicationFormService>();
            services.AddScoped<ICharacterReferenceService, CharacterReferenceService>();
            services.AddScoped<IInterviewerServices, InterviewerServices>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISignUpRepository, SignUpRepository>();
            services.AddScoped<IApplicantListRepository, ApplicantListRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IJobOpeningRepository, JobOpeningRepository>();
            services.AddScoped<IUserViewRepository, UserViewRepository>();
            services.AddScoped<IPublicApplicationFormRepository, PublicApplicationFormRepository>();
            services.AddScoped<ICharacterReferenceRepository, CharacterReferenceRepository>();
            services.AddScoped<IInterviewerRepository, InterviewerRepository>();
        }
    }
}