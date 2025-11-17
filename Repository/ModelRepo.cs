using Microsoft.EntityFrameworkCore;
using Repository.Basic;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ModelRepo : GenericRepository<BearProfile>
    {


        public ModelRepo() { }
        public ModelRepo(FA25BearDBContext context) : base(context)
        {
        }


        public async Task<List<BearProfile>> GetAllAsync()
        {
            return await _context.BearProfiles.Include(t => t.BearType).ToListAsync();
        }

        public async Task<BearProfile> GetByIdAsync(int modelId)
        {
            return await _context.BearProfiles.Include(t => t.BearType).FirstOrDefaultAsync(a => a.BearProfileId == modelId);
        }

        public async Task<bool> CreateAsync(BearProfile model)
        {
            try
            {
                await base.CreateAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(BearProfile model)
        {
            try
            {
                await base.UpdateAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeletetAsync(int id)
        {
            try
            {
                var model = await GetByIdAsync(id);
                if (model != null)
                {
                    await base.RemoveAsync(model);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
