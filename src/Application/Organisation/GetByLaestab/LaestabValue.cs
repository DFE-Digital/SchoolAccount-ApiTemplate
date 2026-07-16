namespace Application.Organisation.GetByLaestab;

public class LaestabValue(string laestab)
{
    public string LocalAuthorityCode => laestab.Take(3).ToString();
    public string EstablishmentNumber => laestab.Skip(3).ToString();
}
