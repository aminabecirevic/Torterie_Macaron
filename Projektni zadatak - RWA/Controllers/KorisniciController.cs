using Microsoft.AspNetCore.Mvc;
using Projektni_zadatak___RWA.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projektni_zadatak___RWA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KorisniciController : ControllerBase
    {
        TorterieMacaronContext db = new TorterieMacaronContext();

        [HttpGet]
        public IActionResult prikazi_sve_korisnike() //za select svega
        {
            List<Korisnici> podaci = db.Korisnicis.OrderByDescending(r => r.Id).ToList();
            return Ok(podaci);
        }

        [HttpPost]
        public IActionResult Registracija([FromBody] Korisnici korisnik)
        {
            if (db.Korisnicis.Any(k => k.Email == korisnik.Email))
                return BadRequest("Email već postoji.");

            db.Korisnicis.Add(korisnik);
            db.SaveChanges();
            return Ok("Registracija uspješna.");
        }

        [HttpPost]
        public IActionResult Login([FromBody] Korisnici korisnik)
        {
            var user = db.Korisnicis
                .FirstOrDefault(k => k.Email == korisnik.Email && k.Lozinka == korisnik.Lozinka);

            if (user == null)
                return Unauthorized("Pogrešan email ili lozinka.");

            return Ok("Uspješna prijava.");
        }

        [HttpDelete("{parametar:int}")]
        public IActionResult Obrisi(int parametar)  //za brisanje po ID
        {
            Korisnici rezultat = db.Korisnicis.Where(r => r.Id == parametar).FirstOrDefault();
            //select * from  where....
            if (rezultat == null)
            { return NotFound($"Podatak sa Id = {parametar} nije pronadjen"); }
            else
            {
                db.Remove(rezultat);
                db.SaveChanges();
            }
            return Ok(parametar);
        }
    }
}
