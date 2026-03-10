using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Services
{
    public class TeamCalendarService
    {
        private readonly ITeamCalendarRepository _repository;

        public TeamCalendarService(ITeamCalendarRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TeamCalendar>> GetCalendarAsync(int month, int year, int deptId)
        {
            try
            {
                return await _repository.GetTeamCalendarAsync(month, year, deptId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in TeamCalendarService GetCalendarAsync method");
                throw;
            }
        }
    }
}