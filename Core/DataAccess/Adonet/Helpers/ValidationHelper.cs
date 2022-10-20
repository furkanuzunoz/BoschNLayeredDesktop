using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Adonet.Helpers
{
    public static class ValidationHelper<T,C> where T : class, new() where C : class, new()
    {
        /*private readonly IValidator _validator;
        private T _request;

        

        public ValidationHelper(IValidator validator,T request)
        {
            _validator = validator;
            _request = request;
            runit();
        }*/
        public static void HelperValidation(IValidator _validator, T _request)
        {
            var context = new ValidationContext<T>(_request);
            var result = _validator.Validate(context);

            if (!result.IsValid) // Validasyon hatası mevcut!!
            {
                foreach (var item in result.Errors)
                {
                    Console.WriteLine(item.ErrorMessage);
                }

                //TO DO: Throw exception and handle globally.

                return;
            }
        }
        //public void runit()
        //{
        //    var context = new ValidationContext<T>(_request) ;
        //    var result = _validator.Validate(context);

        //    if (!result.IsValid) // Validasyon hatası mevcut!!
        //    {
        //        foreach (var item in result.Errors)
        //        {
        //            Console.WriteLine(item.ErrorMessage);
        //        }

        //        //TO DO: Throw exception and handle globally.

        //        return;
        //    }
        //}
    }
}
