using Microsoft.EntityFrameworkCore;
using TeacherTracker.Application.Interfaces;
using TeacherTracker.Domain.Entities;
using TeacherTracker.Infrastructure.Data;

namespace TeacherTracker.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository {
    private readonly TeacherTrackerDbContext _context;

    public TeacherRepository(TeacherTrackerDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<TeacherGlobal>> GetAllAsync() {
        return await _context.TeachersGlobales.AsNoTracking().ToListAsync();
    }

    public async Task<TeacherGlobal?> GetByIdAsync(int id) {
        return await _context.TeachersGlobales.FindAsync(id);
    }

    public async Task<TeacherGlobal> AddAsync(TeacherGlobal teacher) {
        _context.TeachersGlobales.Add(teacher);
        await _context.SaveChangesAsync();
        return teacher;
    }

    public async Task UpdateAsync(TeacherGlobal teacher) {
        _context.TeachersGlobales.Update(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id) {
        var teacher = await _context.TeachersGlobales.FindAsync(id);
        if (teacher is null) {
            return false;
        }

        _context.TeachersGlobales.Remove(teacher);
        await _context.SaveChangesAsync();
        return true;
    }
}
