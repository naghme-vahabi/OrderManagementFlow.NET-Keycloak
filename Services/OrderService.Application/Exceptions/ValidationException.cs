﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Exceptions
{
    public class ValidationException : Exception
    {

        public IDictionary<string, string[]> Errors { get; }
        public ValidationException()
            : base("یک یا چند خطای اعتبارسنجی رخ داده است.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

    }
}
