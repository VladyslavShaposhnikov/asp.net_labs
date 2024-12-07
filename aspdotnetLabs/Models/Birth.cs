namespace aspdotnetLabs.Models;

public class Birth
{
    public string fullName { get; set; }
    public DateTime dateOfBirth { get; set; }
    private DateTime dateNow = DateTime.Now;

    public int HowOld()
    {
        int years = dateNow.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > dateNow.AddYears(-years).Date)
        {
            years--;
        }
        return years;
    }

    public bool isValid()
    {
        if (dateOfBirth > DateTime.Now || dateOfBirth < DateTime.Now.AddYears(-120) || fullName.Length < 2 ||
            fullName.Length > 70)
        {
            return false;
        }
        return true;
    }
}