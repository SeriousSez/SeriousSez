using SeriousSez.Infrastructure.Interfaces;

namespace SeriousSez.Infrastructure.Repositories.Plan
{
    public class PlanRepository : BaseRepository<Domain.Entities.Plan.GroceryPlan>, IPlanRepository
    {
        protected internal SeriousContext _seriousContext { get { return _context as SeriousContext; } }

        public PlanRepository(SeriousContext db) : base(db) { }
    }
}
