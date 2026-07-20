using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    //  ვალიდაციის დამხმარე მეთოდები.
    public class ValidationHelper
    {
            public static bool IsPositive(decimal amount)
            {
                return amount > 0;
            }

            public static bool IsValidEmail(string email)
            {
            return !string.IsNullOrWhiteSpace(email)
             && email.Contains("@")
             && email.Contains(".");
        }

            public static bool IsValidPassword(string password)
            {
                return !string.IsNullOrWhiteSpace(password) && password.Length >= 4;
            }

            public static bool IsNotEmpty(string value)
            {
                return !string.IsNullOrWhiteSpace(value);
            }
        }
    }


