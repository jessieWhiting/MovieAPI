using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieListsController : ControllerBase
    {
        private readonly MovieListContext _context;

        public MovieListsController(MovieListContext context)
        {
            _context = context;
        }

        // GET: api/MovieLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieList>>> GetMovieLists()
        {
            return await _context.MovieLists.ToListAsync();
        }

        // GET: api/MovieLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieList>> GetMovieList(int id)
        {
            var movieList = await _context.MovieLists.FindAsync(id);

            if (movieList == null)
            {
                return NotFound();
            }

            return movieList;
        }

        [HttpGet("Genre/{genre}")]
        public async Task<ActionResult<IEnumerable<MovieList>>> SearchByGenre(string genre)
        {
            return await _context.MovieLists.Where(m => m.Genre.Contains(genre)).ToListAsync();
        }

        private string GetRandomMovie()
        {
            Random random = new Random();
            List<string> r = new List<string>();
            List<MovieList> movie = _context.MovieLists.ToList();

            foreach (MovieList m in movie)
            {
                r.Add("Title: " + m.Title + " " + "/ Year Released: " +  m.Year);
            }
            int i = random.Next(r.Count);
            return r[i];
        }

        [HttpGet("RandomMovie")]
        public async Task<ActionResult<String>> GetRandom()
        {
            return GetRandomMovie();
        }

        [HttpGet("RandomByGenre/{genre}")]
        public async Task<ActionResult<String>> RandomByGenre(string genre)
        {
            IEnumerable<MovieList> selected = await _context.MovieLists.Where(m => m.Genre.Contains(genre)).ToListAsync();
            List<string> gl = new List<string>();
            Random random = new Random();

            foreach (MovieList m in selected)
            {
                gl.Add("Title: " + m.Title + " " + "/ Year Released: " + m.Year);
            }
            int i = random.Next(gl.Count);
            return gl[i];
        }


        // PUT: api/MovieLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieList(int id, MovieList movieList)
        {
            if (id != movieList.Id)
            {
                return BadRequest();
            }

            _context.Entry(movieList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MovieLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieList>> PostMovieList(MovieList movieList)
        {
            _context.MovieLists.Add(movieList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieList", new { id = movieList.Id }, movieList);
        }
      

        // DELETE: api/MovieLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieList(int id)
        {
            var movieList = await _context.MovieLists.FindAsync(id);
            if (movieList == null)
            {
                return NotFound();
            }

            _context.MovieLists.Remove(movieList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Movies by GENRE:
        
        

        private bool MovieListExists(int id)
        {
            return _context.MovieLists.Any(e => e.Id == id);
        }

      
    }
}
