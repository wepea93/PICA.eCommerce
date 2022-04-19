using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Validations.Products
{
    public  class ValidateSearch : ValidationAttribute
    {
        public ValidateSearch() { }

        public override bool IsValid(object? value)
        {
            try
            {
                if(value == null) return true;

                var search = value.ToString();

                if (string.IsNullOrEmpty(search)) return true;

                search = search.Trim();

                if (search.Length < 4)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
