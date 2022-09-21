using Dapper;
using DapperUsingSP.Context;
using DapperUsingSP.Models;
using DapperUsingSP.Repository.Interface;

using System.Data;

namespace DapperUsingSP.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                result = connection.Execute("DeleteGroupEmployee", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee employee = new Employee();

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                employee = connection.Query<Employee>("GetGroupEmployeeByID", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee();
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                                    
                employees = connection.Query<Employee>("GetGroupEmployee", commandType: CommandType.StoredProcedure).ToList();
                foreach (var item in employees)
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@id", item.id);
                }
            }
            return employees;
        }

        public async Task<int> Insert(Employee employee)
        {
            int result = 0;
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ename", employee.ename);
                dynamicParameters.Add("@salary", employee.salary);
                result = await connection.ExecuteScalarAsync<int>("InsertGroupEmployee", dynamicParameters, commandType: CommandType.StoredProcedure);

            }
            return result;
        }

        

        public async Task<int> Update(Employee employee)
        {
            int result = 0;
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", employee.id);
                dynamicParameters.Add("@ename", employee.ename);
                dynamicParameters.Add("@salary", employee.salary);
                result = await connection.ExecuteAsync("Updateemployee", dynamicParameters, commandType: CommandType.StoredProcedure);
                result = employee.id;
            }
            return result;
        }

    }
}
