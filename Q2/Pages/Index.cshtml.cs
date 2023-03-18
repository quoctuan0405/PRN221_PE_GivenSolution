using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Q2.Models.ApplicationDbContext _context;

        public int ProducerId { get; set; }

        public IndexModel(Q2.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;
        public IList<Producer> Producers { get; set; } = default!;

        public async Task OnGetAsync(int producerId)
        {
            ProducerId = producerId;

            if (_context.Movies != null)
            {
                if (producerId == 0)
                {
                    Movie = await _context.Movies
                        .Include(m => m.Director)
                        .Include(m => m.Producer)
                        .Include(m => m.Stars)
                        .ToListAsync();
                } 
                else
                {
                    Movie = await _context.Movies
                        .Include(m => m.Director)
                        .Include(m => m.Producer)
                        .Where(p => p.Id == producerId)
                        .Include(m => m.Stars)
                        .ToListAsync();
                }
            }

            if (_context.Producers != null) 
            {
                Producers = await _context.Producers.ToListAsync();
            }
        }
    }
}
