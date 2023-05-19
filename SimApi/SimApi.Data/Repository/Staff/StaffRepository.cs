using Microsoft.EntityFrameworkCore;
using SimApi.Data.Context;
using SimApi.Data.Domain;
using System.Security.Cryptography.X509Certificates;
using static Dapper.SqlMapper;

namespace SimApi.Data.Repository;

public class StaffRepository : GenericRepository<Staff>, IStaffRepository
{
    public StaffRepository(SimDbContext context) : base(context)
    {

    }
    public List<Staff> FindByEmail(string email)
    {
        var list = dbContext.Set<Staff>().Where(c => c.Email.Contains(email)).ToList();
        return list;
    }
    public List<Staff> FindByName(string name)
    {
        return dbContext.Set<Staff>().Where(n => n.FirstName.Contains(name)
        || n.LastName.Contains(name)
        ).ToList();
    }
    public List<Staff> FindByLocation(string location)
    {
        return dbContext.Set<Staff>()
        .Where(n => n.AddressLine1.Contains(location)
        || n.Country.Contains(location)
        || n.City.Contains(location)
        || n.Province.Contains(location)).ToList();

    }


}
