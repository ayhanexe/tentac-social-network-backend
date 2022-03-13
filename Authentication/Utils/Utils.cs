using Authentication.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Authentication.Utils
{
    public static class Utils
    {
        public static IResponse isValidEmail(this string _string)
        {

            if (!string.IsNullOrEmpty(_string))
            {
                try
                {
                    MailAddress mail = new MailAddress(_string);

                    return new Response()
                    {
                        HasError = false,
                        Message = null
                    };
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        HasError = true,
                        Message = "Invalid email!"
                    };
                }
            }
            else
            {
                return new Response()
                {
                    HasError = true,
                    Message = "Invalid email!"
                };
            }
        }

        public static IResponse isValidUsername(this string _string)
        {
            if (!string.IsNullOrEmpty(_string))
            {
                if (_string.Length >= Options.MinimumUsernameLength)
                {
                    if (_string.Length <= Options.MaximumUsernameLength)
                    {
                        return new Response()
                        {
                            HasError = false,
                            Message = null
                        };
                    }
                    else
                    {
                        return new Response()
                        {
                            HasError = true,
                            Message = $"Username's length must be lower than {Options.MaximumUsernameLength} characters."
                        };
                    }
                }
                else
                {
                    return new Response()
                    {
                        HasError = true,
                        Message = $"Username's length must be greater than {Options.MinimumUsernameLength} characters."
                    };
                }
            }
            else
            {
                return new Response()
                {
                    HasError = true,
                    Message = "Invalid username!"
                };
            }
        }

        public static IResponse isValidPassword(this string _string)
        {
            if (!string.IsNullOrEmpty(_string))
            {
                if (_string.Length >= Options.MinimumPasswordLength)
                {
                    if (_string.Length <= Options.MaximumPasswordLength)
                    {
                        return new Response()
                        {
                            HasError = false,
                            Message = null
                        };
                    }
                    else
                    {
                        return new Response()
                        {
                            HasError = true,
                            Message = $"Password's length must be lower than {Options.MaximumPasswordLength} characters."
                        };
                    }
                }
                else
                {
                    return new Response()
                    {
                        HasError = true,
                        Message = $"Password's length must be greater than {Options.MinimumPasswordLength} characters."
                    };
                }
            }
            else
            {
                return new Response()
                {
                    HasError = true,
                    Message = "Invalid password!"
                }; ;
            }
        }
    }
}
