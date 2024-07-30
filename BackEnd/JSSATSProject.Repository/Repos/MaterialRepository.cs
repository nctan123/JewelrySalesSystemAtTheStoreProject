using System.Runtime.InteropServices.JavaScript;
using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class MaterialRepository : GenericRepository<Material>
{
    public MaterialRepository(DBContext context) : base(context)
    {
    }

    public async Task<List<string>> GetIdsWithLastEffectiveDateMoreThanOneDayOldAsync()
    {
        var result = new List<string>();
        var timeStamp = DateTime.Now;
        var existingMaterials = await context.Materials.ToListAsync();
        foreach (var material in existingMaterials)
        {
            var lastEffectiveDate = (await context.MaterialPriceLists
                        .Where(m => m.Material.Id == material.Id)
                        .OrderByDescending(mpl => mpl.EffectiveDate)
                        .FirstAsync()
                    ).EffectiveDate
                ;
            if (timeStamp.Subtract(lastEffectiveDate).TotalHours >= 24) result.Add(material.Name);
        }
        return result;
    }
}