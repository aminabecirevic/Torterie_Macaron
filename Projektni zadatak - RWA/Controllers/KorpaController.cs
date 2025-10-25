using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projektni_zadatak___RWA.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projektni_zadatak___RWA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KorpaController : ControllerBase
    {
        TorterieMacaronContext db = new TorterieMacaronContext();

        [HttpGet]
        public IActionResult PrikaziKorpu()
        {
            var proizvodiUKorpi = db.Korpas //dohvaćamo podatke iz baze
                .Include(k => k.Proizvod) //uključuje i podatke o povezanom proizvodu za svaku stavku u korpi
                .ToList();
            return Ok(proizvodiUKorpi);
        }

        [HttpPost]
        public IActionResult dodaj_u_korpu([FromBody] Korpa novaStavka)
        {
            if (novaStavka == null || novaStavka.ProizvodId == null)
                return BadRequest("Neispravan proizvod.");

            //Provjera postoji li već proizvod u korpi
            var postojecaStavka = db.Korpas.FirstOrDefault(k => k.ProizvodId == novaStavka.ProizvodId);

            if (postojecaStavka != null)
            {
                postojecaStavka.Kolicina += novaStavka.Kolicina;
            }
            else
            {
                db.Korpas.Add(novaStavka);
            }

            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{parametar:int}")]
        public IActionResult Obrisi(int parametar)  //za brisanje po ID
        {
            Korpa rezultat = db.Korpas.Where(r => r.Id == parametar).FirstOrDefault();
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
        
        [HttpPost("{id}")]
        public IActionResult PovecajKolicinu(int id)
        {
            var stavka = db.Korpas.FirstOrDefault(k => k.Id == id);
            if (stavka == null) return NotFound();

            stavka.Kolicina++;
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult SmanjiKolicinu(int id)
        {
            var stavka = db.Korpas.FirstOrDefault(k => k.Id == id);
            if (stavka == null) return NotFound();

            stavka.Kolicina--;

            if (stavka.Kolicina <= 0)
                db.Korpas.Remove(stavka);

            db.SaveChanges();
            return Ok();
        }

    }
}
