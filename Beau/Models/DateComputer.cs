using Beau.Models;
namespace Beau.Models
{
    public class DateComputer
    {

        public static int CalculateAge(DateTime birthdate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthdate.Year;

            if (birthdate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
