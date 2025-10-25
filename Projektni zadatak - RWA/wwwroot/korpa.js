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

