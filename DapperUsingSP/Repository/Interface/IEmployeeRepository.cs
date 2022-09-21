using DapperUsingSP.Models;

namespace DapperUsingSP.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> GetEmployeeById(int id);
        public Task<int> Insert(Employee employee);
        public Task<int> Update(Employee employee);
        public Task<int> Delete(int id);
    }
}
