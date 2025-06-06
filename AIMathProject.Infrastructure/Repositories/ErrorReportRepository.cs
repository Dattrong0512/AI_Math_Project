using AIMathProject.Application.Dto.ErrorReportDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class ErrorReportRepository : IErrorReportRepository<ErrorReportDto>
    {
        private readonly ApplicationDbContext _context;

        public ErrorReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorReportDto> CreateErrorReport(int userId, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentException("Error message cannot be null or empty");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} not found");
            }

            var errorReport = new ErrorReport
            {
                UserId = userId,
                ErrorMessage = errorMessage,
                ErrorType = "user",
                CreatedAt = DateTime.Now,
                ResolvedAt = null,
                Resolved = false
            };

            await _context.ErrorReports.AddAsync(errorReport);
            await _context.SaveChangesAsync();

            return new ErrorReportDto
            {
                ErrorId = errorReport.ErrorId,
                UserId = errorReport.UserId,
                ErrorMessage = errorReport.ErrorMessage,
                ErrorType = errorReport.ErrorType,
                CreatedAt = errorReport.CreatedAt,
                ResolvedAt = errorReport.ResolvedAt,
                Resolved = errorReport.Resolved
            };
        }

        public async Task<ErrorReportDto> GetErrorReportById(int errorId)
        {
            var errorReport = await _context.ErrorReports
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.ErrorId == errorId);

            if (errorReport == null)
                return null;

            return new ErrorReportDto
            {
                ErrorId = errorReport.ErrorId,
                UserId = errorReport.UserId,
                ErrorMessage = errorReport.ErrorMessage,
                ErrorType = errorReport.ErrorType,
                CreatedAt = errorReport.CreatedAt,
                ResolvedAt = errorReport.ResolvedAt,
                Resolved = errorReport.Resolved
            };
        }

        public async Task<List<ErrorReportDto>> GetErrorReportsByUserId(int userId)
        {
            var errorReports = await _context.ErrorReports
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return errorReports.Select(e => new ErrorReportDto
            {
                ErrorId = e.ErrorId,
                UserId = e.UserId,
                ResolvedAt = e.ResolvedAt,
                ErrorMessage = e.ErrorMessage,
                ErrorType = e.ErrorType,
                CreatedAt = e.CreatedAt,
                Resolved = e.Resolved
            }).ToList();
        }


        public async Task<List<ErrorReportDto>> GetErrorReports(
            string searchTerm = null,
            string errorType = null,
            bool? resolved = null,
            int pageNumber = 1,
            int pageSize = 10,
            bool newestFirst = true)
        {
            var query = _context.ErrorReports.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(e =>
                    e.ErrorMessage.ToLower().Contains(searchTerm) ||
                    e.ErrorId.ToString().Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(errorType))
            {
                query = query.Where(e => e.ErrorType == errorType);
            }

            if (resolved.HasValue)
            {
                query = query.Where(e => e.Resolved == resolved);
            }

            // Apply sorting
            query = newestFirst
                ? query.OrderByDescending(e => e.CreatedAt)
                : query.OrderBy(e => e.CreatedAt);

            // Apply pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            query = query.Include(e => e.User);

            var errorReports = await query.ToListAsync();

            return errorReports.Select(e => new ErrorReportDto
            {
                ErrorId = e.ErrorId,
                UserId = e.UserId,
                ErrorMessage = e.ErrorMessage,
                ErrorType = e.ErrorType,
                CreatedAt = e.CreatedAt,
                ResolvedAt = e.ResolvedAt,
                Resolved = e.Resolved
            }).ToList();
        }

        public async Task<List<ErrorReportDto>> GetAllErrorReports()
        {
            return await GetErrorReports(pageSize: int.MaxValue);
        }

        public async Task<int> GetTotalErrorReportsCount(
            string searchTerm = null,
            string errorType = null,
            bool? resolved = null)
        {
            var query = _context.ErrorReports.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(e =>
                    e.ErrorMessage.ToLower().Contains(searchTerm) ||
                    e.ErrorId.ToString().Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(errorType))
            {
                query = query.Where(e => e.ErrorType == errorType);
            }

            if (resolved.HasValue)
            {
                query = query.Where(e => e.Resolved == resolved);
            }

            return await query.CountAsync();
        }

        public async Task<ErrorReportDto> ResolveErrorReport(int errorId)
        {
            var errorReport = await _context.ErrorReports.FindAsync(errorId);

            if (errorReport == null)
            {
                throw new ArgumentException($"Error report with ID {errorId} not found");
            }

            // Cập nhật trạng thái thành đã giải quyết
            errorReport.Resolved = true;
            errorReport.ResolvedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ErrorReportDto
            {
                ErrorId = errorReport.ErrorId,
                UserId = errorReport.UserId,
                ErrorMessage = errorReport.ErrorMessage,
                ErrorType = errorReport.ErrorType,
                CreatedAt = errorReport.CreatedAt,
                ResolvedAt = errorReport.ResolvedAt,
                Resolved = errorReport.Resolved
            };
        }

        public async Task<bool> DeleteErrorReport(int errorId)
        {
            var errorReport = await _context.ErrorReports.FindAsync(errorId);

            if (errorReport == null)
            {
                return false;
            }

            _context.ErrorReports.Remove(errorReport);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}