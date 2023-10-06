using Microsoft.EntityFrameworkCore;
using student_records.Shared.Roles;
using students_records.Data.Models;
using students_records.Data.Repositories.Interfaces;

namespace students_records.Data.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly StudentRecordContext _studentRecordContext;
       
        public UserRepository(StudentRecordContext studentRecordContext)
        {
            _studentRecordContext = studentRecordContext;
        }

        public async Task<List<User>> GetAllProfessors()
        {
            return await _studentRecordContext.Users.Where(p => p.Role == (int)Roles.Professor).ToListAsync();
        }

        public async Task<User> GetProfessorById(int id)
        {
            return await _studentRecordContext.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> AddProffesor(User user)
        {
            user.Role = ((int)Roles.Professor);
            _studentRecordContext.Users.Add(user);
            await _studentRecordContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> Update(User user)
        {
             _studentRecordContext.Users.Update(user);
            await _studentRecordContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Delete(int id)
        {
            User user = _studentRecordContext.Users.FirstOrDefault(p => p.Id == id);      
            if (user == null) { return false; }
            
            _studentRecordContext.Users.Remove(user);
            return await _studentRecordContext.SaveChangesAsync() > 0;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            return await Task.Run(() => _studentRecordContext.Users
            .FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password)));
        }
    }
}
