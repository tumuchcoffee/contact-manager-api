using ContactManager.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ContactManagerAPI.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddContactService(this IServiceCollection services)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			// Register IContactService with its implementation ContactService
			services.AddScoped<IContactService, ContactService>();

			return services;
		}
	}
}
