let sviProizvodi = [];

function prikaziPrviProizvod() {
    if (sviProizvodi.length === 0) return;

    const prvaTri = sviProizvodi.slice(0, 3);
    const prviDiv = document.getElementById("prviProizvod");

    prvaTri.forEach(proizvod => {
        prviDiv.innerHTML += `
                            <div class="offer-container">
                                <div class="offer-content">
                                    <img src="${proizvod.slikaUrl}" alt="${proizvod.naziv}" style="width:250px"/><br/>
                                    <p class="description"><a href="#">${proizvod.naziv}</a></p>
                                    <p class="price">${proizvod.cijena} €</p><br>
                                    <button class="btn" onclick="dodajUKorpu(${proizvod.id})">Dodaj u korpu</button>
                                </div>
                            </div>
        `;
    })
}

function prikaziOstale() {
    const ostaliDiv = document.getElementById("ostaliProizvodi");
    ostaliDiv.innerHTML = ""; 

    for (let i = 3; i < sviProizvodi.length; i++) {
        const proizvod = sviProizvodi[i];
        ostaliDiv.innerHTML += `
                                    <div class="offer-container">
                                        <div class="offer-content">
                                            <img src="${proizvod.slikaUrl}" alt="${proizvod.naziv}" style="width:250px"/><br/>
                                            <p class="description"><a href="#">${proizvod.naziv}</a></p>
                                            <p class="price">${proizvod.cijena} €</p><br>
                                            <button class="btn" onclick="dodajUKorpu(${proizvod.id})">Dodaj u korpu</button>
                                        </div>
                                    </div>
        `;
    }
    document.getElementById("prikaziOstaleBtn").style.display = "none";
}

function ucitajProizvode() {
    fetch('/api/Proizvodi/prikazi_sve_proizvode')
        .then(res => res.json())
        .then(data => {
            sviProizvodi = data;
            prikaziPrviProizvod();
        })
        .catch(() => {
            alert('Greška pri učitavanju proizvoda.');
        });
}
document.getElementById("prikaziOstaleBtn").addEventListener("click", prikaziOstale);
//učitavanje proizvoda odmah pri učitavanju stranice
window.onload = ucitajProizvode;

function dodajUKorpu(proizvodId) {
    fetch('/api/Korpa/dodaj_u_korpu', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ proizvodId: proizvodId, kolicina: 1 })
    })
        .then(res => {
            if (res.ok) {
                alert("Proizvod dodat u korpu!");
                prikaziKorpu();
            } else {
                alert("Greška pri dodavanju u korpu.");
            }
        });
}
function prikaziKorpu() {
    fetch('/api/Korpa/PrikaziKorpu')
        .then(res => res.json())
        .then(data => {
            const korpaDiv = document.getElementById("korpaPrikaz");
            const korpaLista = document.getElementById("korpaLista");

            korpaLista.innerHTML = "";

            if (data.length === 0) {
                korpaLista.innerHTML = "<li>Korpa je prazna.</li>";
            } else {
                data.forEach(item => {
                    const ukupnaCijena = (item.kolicina * item.proizvod.cijena).toFixed(2);
                    korpaLista.innerHTML +=
                        `<li class=stavka-korpe>
                                    <img src="${item.proizvod.slikaUrl}" class="korpa-slika" style="width: 150px; border-radius: 10px;" />
                                    <div class="korpa-detalji">
                                        <strong>${item.proizvod.naziv}</strong><br/>
                                        Cijena: ${item.proizvod.cijena} €<br/>
                                        Količina:
                                        <button class="btn btn-sm" onclick="smanji(${item.id})">-</button>
                                        ${item.kolicina}
                                        <button class="btn btn-sm" onclick="povecaj(${item.id})">+</button></br>
                                        <strong>Ukupno: ${ukupnaCijena} €</strong>
                                    </div>
                        </li>`;
                });
            }

            korpaDiv.style.display = "block";
        })
        .catch(() => {
            alert("Greška pri dohvaćanju korpe.");
        });
}
function povecaj(id) {
    fetch(`/api/Korpa/PovecajKolicinu/${id}`, {
        method: 'POST'
    })
        .then(res => {
            if (res.ok) {
                prikaziKorpu();
            }
        });
}
function smanji(id) {
    fetch(`/api/Korpa/SmanjiKolicinu/${id}`, {
        method: 'POST'
    })
        .then(res => {
            if (res.ok) {
                prikaziKorpu();
            }
        });
}
