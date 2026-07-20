namespace Application.Organisations.GetByLaestab;

public class LaestabValue(string laestab)
{
    public string LocalAuthorityCode => laestab[..3];
    public string EstablishmentNumber => laestab[3..];
}
