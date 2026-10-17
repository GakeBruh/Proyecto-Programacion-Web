using TeacherTracker.Domain.Entities;

namespace TeacherTracker.Application.Interfaces;

public interface ITeacherRepository
{
    Task<IEnumerable<TeacherGlobal>> GetAllAsync();
    Task<TeacherGlobal?> GetByIdAsync(int id);
    Task<TeacherGlobal> AddAsync(TeacherGlobal teacher);
    Task UpdateAsync(TeacherGlobal teacher);
    Task<bool> DeleteAsync(int id);
}