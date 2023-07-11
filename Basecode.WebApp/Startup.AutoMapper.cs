﻿using AutoMapper;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Basecode.WebApp
{
    public partial class Startup
    {
        private void ConfigureMapper(IServiceCollection services)
        {
            var Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Login, LoginViewModel>();
                cfg.CreateMap<SignUp, SignUpViewModel>();
                cfg.CreateMap<SignUpViewModel, SignUp>();
                cfg.CreateMap<JobOpening, JobOpeningViewModel>();
                cfg.CreateMap<PublicApplicationForm, PublicApplicationFormViewModel>();
                cfg.CreateMap<PublicApplicationFormViewModel, PublicApplicationForm>();
                cfg.CreateMap<CharacterReference, CharacterReferenceViewModel>();
                cfg.CreateMap<CharacterReferenceViewModel, CharacterReference>();
            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}