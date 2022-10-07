using System.Threading.Tasks;
using LPC.Domain.Database;
using LPC.Domain.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LPC.Domain.Helpers;

public class ValidationHelper : IValidationHelper
{
    private readonly LpcDbContext _dbContext;

    public ValidationHelper(LpcDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ValidateRecordDuplication(int id)
    {
        var libraryRecordToValidate = await _dbContext.Libraries.FirstOrDefaultAsync(x => x.RecordOwned == id);
        var wishlistRecordToValidate = await _dbContext.Wishlists.FirstOrDefaultAsync(x => x.RecordWished == id);
        return libraryRecordToValidate == null && wishlistRecordToValidate == null;
    }
}