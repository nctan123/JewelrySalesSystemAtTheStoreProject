using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class PointRepository : GenericRepository<Point>
{
    public PointRepository(DBContext context) : base(context)
    {
    }

    public async Task<Point?> GetByCustomerPhoneNumber(string phoneNumber)
    {
        var entity = await context.Points.Where(p => p.Customers.First().Phone == phoneNumber).FirstOrDefaultAsync();
        return entity;
    }

}