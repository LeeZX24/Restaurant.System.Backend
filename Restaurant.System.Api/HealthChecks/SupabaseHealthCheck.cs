using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Restaurant.System.Api.HealthChecks
{
    public class SupabaseHealthCheck : IHealthCheck
    {
        private readonly Supabase.Client _supabase;

        public SupabaseHealthCheck(Supabase.Client supabase)
        {
            // If 'supabase' is null here, DI failed to find the service registered in Program.cs
            _supabase = supabase ?? throw new ArgumentNullException(nameof(supabase),
                "Supabase.Client was not found in the service collection.");
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _supabase.InitializeAsync();

                return HealthCheckResult.Healthy("Supabase API is Reachable");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Supabase connection failed.", ex);
            }
        }
    }
}