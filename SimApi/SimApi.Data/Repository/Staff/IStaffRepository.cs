using SimApi.Data.Domain;

namespace SimApi.Data.Repository;

public interface IStaffRepository : IGenericRepository<Staff>
{
    List<Staff> FindByEmail(string email);
    List<Staff> FindByName(string name);
    List<Staff> FindByLocation(string location);
  
}
