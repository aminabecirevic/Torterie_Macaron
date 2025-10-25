using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Projektni_zadatak___RWA.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projektni_zadatak___RWA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProizvodiController : ControllerBase
    {
        TorterieMacaronContext db = new TorterieMacaronContext();

        [HttpGet]
        public IActionResult prikazi_sve_proizvode() //za select svega
        {
            List<Proizvodi> podaci = db.Proizvodis.OrderBy(r => r.Id).ToList();
            return Ok(podaci);
        }

        [HttpPost]
        public IActionResult unesi_novi_proizvod([FromBody] Proizvodi podaci)     //za unos novog podatka
        {

            db.Add(podaci);
            db.SaveChanges();
            return Ok(podaci);
        }

        [HttpDelete("{parametar:int}")]
        public IActionResult Obrisi(int parametar)  //za brisanje po ID
        {
            Proizvodi rezultat = db.Proizvodis.Where(r => r.Id == parametar).FirstOrDefault();
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

        [HttpGet]
        public IActionResult azuriraj(int IDpodatka, string novaNaziv)     //za azuriranje po ID
        {
            var rezultat = db.Proizvodis.Where(r => r.Id == IDpodatka).FirstOrDefault();
            //select * from  where....
            if (rezultat == null)
            { return Ok("ne postoji podatak sa tim ID"); }
            else
            {
                rezultat.Naziv = novaNaziv;
                db.SaveChanges();
            }
            return Ok("azurirano");
        }

    }
}
