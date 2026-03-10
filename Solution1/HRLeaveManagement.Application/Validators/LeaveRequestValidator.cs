using FluentValidation;
using HRLeaveManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Validators
{
    public class LeaveRequestValidator : AbstractValidator<LeaveRequestDto>
    {
        public LeaveRequestValidator() 
        {
            RuleFor(x => x.EmpId)
                .GreaterThan(0).WithMessage("Employee Id is required.");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0).WithMessage("Leave Type is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .Must(date => date.Date >= DateTime.Now.Date)
                .WithMessage("Start date cannot be in the past.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be after or equal to Start date.");

            RuleFor(x => x.BusinessDays)
                .GreaterThan(0)
                .WithMessage("Business days must be at least 1.");

            //RuleFor(x => x.Reason)
            //    .MaximumLength(300)
            //    .WithMessage("Reason cannot exceed 300 characters.");
        }
    }
}
