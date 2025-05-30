using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AIMathProject.Application.Dto.EnrollmentUnlockExerciseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class EnrollmentUnlockExerciseRepository : IEnrollmentUnlockExerciseRepository<EnrollmentUnlockExerciseDto>
    {
        private readonly ApplicationDbContext _context;
        private const int UNLOCK_COST = 1; // 1 coin to unlock an exercise

        public EnrollmentUnlockExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetUnlockedExercisesByEnrollmentId(int enrollmentId)
        {
            return await _context.Set<EnrollmentUnlockExercise>()
                .Where(e => e.EnrollmentId == enrollmentId)
                .Select(e => e.ExerciseId.Value)
                .ToListAsync();
        }

        public async Task<(bool success, string message)> UnlockExercise(
            int exerciseId, int enrollmentId)
        {
            try
            {
                // 1. Check if exercise exists and is locked
                var exercise = await _context.Exercises
                    .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId && e.IsLocked == true);

                if (exercise == null)
                {
                    return (false, "Exercise not found or already unlocked globally");
                }

                var enrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.EnrollmentId == enrollmentId);

                if (enrollment == null)
                {
                    return (false, "Invalid enrollment");
                }

                // Get the userId from the enrollment
                int userId = (int)enrollment.UserId;

                // 3. Check if exercise is already unlocked for this enrollment
                var isAlreadyUnlocked = await _context.Set<EnrollmentUnlockExercise>()
                    .AnyAsync(e => e.ExerciseId == exerciseId && e.EnrollmentId == enrollmentId);

                if (isAlreadyUnlocked)
                {
                    return (false, "Exercise is already unlocked for this enrollment");
                }

                // 4. Get user wallet using userId from enrollment
                var wallet = await _context.Wallets
                    .FirstOrDefaultAsync(w => w.UserId == userId);

                if (wallet == null)
                {
                    return (false, "Wallet not found");
                }

                if (wallet.CoinRemains < UNLOCK_COST)
                {
                    return (false, "Not enough coins to unlock this exercise");
                }

                // 5. Update wallet balance
                wallet.CoinRemains -= UNLOCK_COST;

                // 6. Create coin transaction record
                var coinTransaction = new CoinTransaction
                {
                    WalletId = wallet.WalletId,
                    IsTokenPackage = false,
                    CoinRemains = wallet.CoinRemains,
                    Amount = -UNLOCK_COST, // Negative as coins are spent
                    Date = DateTime.UtcNow
                };

                // 7. Create unlock record
                var unlockRecord = new EnrollmentUnlockExercise
                {
                    ExerciseId = exerciseId,
                    EnrollmentId = enrollmentId,
                    UnlockDate = DateTime.UtcNow
                };

                await _context.CoinTransactions.AddAsync(coinTransaction);
                await _context.EnrollmentUnlockExercises.AddAsync(unlockRecord);
                await _context.SaveChangesAsync();

                return (true, "Exercise successfully unlocked");
            }
            catch (Exception ex)
            {
                return (false, $"Error unlocking exercise: {ex.Message}");
            }
        }
    }
}