using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Q2.Models.ApplicationDbContext _context;

        public DeleteModel(Q2.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id, int producerId)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Stars)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync();

            if (movie != null)
            {
                Movie = movie;

                foreach (var star in movie.Stars)
                {
                    movie.Stars.Remove(star);
                }

                foreach (var genre in movie.Genres)
                {
                    movie.Genres.Remove(genre);
                }

                _context.Movies.Remove(Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { producerId = producerId});
        }
    }
}
